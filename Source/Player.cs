using System;
using System.Linq;
using Evergreen.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keyboard = Evergreen.System.Keyboard;

namespace Evergreen
{
    enum Direction {
        Left,
        Right,
    }

    public class Player : DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 Acceleration = Vector2.Zero;
        private Texture2D texture;
        private Direction direction = Direction.Right;
        private bool isOnFloor = false;
        private bool isBlockedRight = false;
        private bool isBlockedLeft = false;
        private bool isBlockedUp = false;
        private float speed;

        public Player(Game game) : base(game)
        {
            LoadContent(game.Content);
        }

        public override void Initialize()
        {
            Position = new Vector2(Evergreen.GraphicsManager.PreferredBackBufferWidth / 2, Evergreen.GraphicsManager.PreferredBackBufferHeight / 2);
            speed = 100f;

            base.Initialize();
        }

        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updatedPlayerSpeed = speed * delta;

            CheckCollisions();

            if (!isOnFloor)
            {
                Physics.ApplyGravity(this, delta);
            }

            if (!isBlockedLeft && Keyboard.IsPressed(Keys.A))
            {
                direction = Direction.Left;
                Position.X -= updatedPlayerSpeed;
            }

            if (!isBlockedRight && Keyboard.IsPressed(Keys.D))
            {
                direction = Direction.Right;
                Position.X += updatedPlayerSpeed;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteEffects spriteEffects = direction == Direction.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Graphics.Draw(texture, Position, new Vector2(0, texture.Bounds.Height), spriteEffects);

            base.Draw(gameTime);
        }

        private Vector2 TileCoords()
        {
            return Tile.WorldToTileCoords(new Vector2(((Position.X / 16f) + 1f) * 16, Position.Y));
        }

        private void CheckCollisions()
        {
            Vector2 pos = TileCoords();

            Vector2[] stopLeftPositions = [
                new(pos.X - 1, pos.Y - 3),
                new(pos.X - 1, pos.Y - 2),
                new(pos.X - 1, pos.Y - 1),
            ];

            Vector2[] stopRightPositions = [
                new(pos.X + 1, pos.Y - 3),
                new(pos.X + 1, pos.Y - 2),
                new(pos.X + 1, pos.Y - 1),
            ];

            Vector2[] stopUpPositions = [
                new(pos.X, pos.Y - 3),
                new(pos.X + 1, pos.Y - 1),
            ];

            Vector2[] stopDownPositions = [
                new(pos.X, pos.Y),
                new(pos.X + 1, pos.Y),
            ];

            isBlockedLeft = stopLeftPositions.Any(position => World.Tiles.TryGetValue(position, out Tile _));
            isBlockedRight = stopRightPositions.Any(position => World.Tiles.TryGetValue(position, out Tile _));
            isBlockedUp = stopUpPositions.Any(position => World.Tiles.TryGetValue(position, out Tile _));
            isOnFloor = stopDownPositions.Any(position => World.Tiles.TryGetValue(position, out Tile _));
        }
    }
}
