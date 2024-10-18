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
    public class Player : DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 Acceleration = Vector2.Zero;
        private Texture2D texture;
        private Rectangle collisionBox;
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
            
            collisionBox = new Rectangle(((int)Position.X), ((int)Position.X), 32, 32);

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
                Position.X -= updatedPlayerSpeed;
            }

            if (!isBlockedRight && Keyboard.IsPressed(Keys.D))
            {
                Position.X += updatedPlayerSpeed;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Draw(texture, Position, new Vector2(0, texture.Bounds.Height));

            base.Draw(gameTime);
        }

        private Vector2 TileCoords()
        {
            return Tile.WorldToTileCoords(Position);
        }

        private void CheckCollisions()
        {
            Vector2 pos = TileCoords();

            Vector2[] stopLeftPositions = [
                new Vector2(pos.X - 1, pos.Y - 2),
                new Vector2(pos.X - 1, pos.Y - 1),
            ];

            Vector2[] stopRightPositions = [
                new Vector2(pos.X + 1, pos.Y - 2),
                new Vector2(pos.X + 1, pos.Y - 1),
            ];

            Vector2 stopUpPosition = new(pos.X, pos.Y - 3);
            Vector2 stopDownPosition = pos;

            isBlockedLeft = stopLeftPositions.Any(position => World.Tiles.TryGetValue(position, out Tile _));
            isBlockedRight = stopRightPositions.Any(position => World.Tiles.TryGetValue(position, out Tile _));

            isOnFloor = World.Tiles.TryGetValue(stopDownPosition, out Tile _);
            isBlockedUp = World.Tiles.TryGetValue(stopUpPosition, out Tile _);
        }

        public bool CollidesWithTile(Tile tile)
		{
			return collisionBox.Intersects(tile.collisionBox);
		}
    }
}
