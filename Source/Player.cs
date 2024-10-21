using System;
using System.Linq;
using Evergreen.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keyboard = Evergreen.System.Keyboard;

namespace Evergreen {
    enum Direction {
        Left,
        Right,
    }

    public class Player : DrawableGameComponent {
        public Vector2 Position;
        public Vector2 Acceleration = Vector2.Zero;
        private Texture2D texture;
        private Direction direction = Direction.Right;
        private bool isOnFloor = false;
        private bool isBlockedRight = false;
        private bool isBlockedLeft = false;
        private bool isBlockedUp = false;
        private const float SPEED = 100;
        private const float JUMPING_FORCE = 200;

        public Player(Game game) : base(game) {
            LoadContent();
        }

        public override void Initialize() {
            Position = new Vector2(Evergreen.GraphicsManager.PreferredBackBufferWidth / 2, Evergreen.GraphicsManager.PreferredBackBufferHeight / 2);

            base.Initialize();
        }

        protected override void LoadContent() {
            ContentManager content = Evergreen.Instance.Content;
            texture = content.Load<Texture2D>("Images/NPC_3");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updatedPlayerSpeed = SPEED * delta;

            CheckCollisions();

            if (!isOnFloor && Acceleration.Y >= 0) {
                Physics.ApplyGravity(this);
            }
            
            if (isOnFloor) {
                Acceleration.Y = Math.Min(Acceleration.Y, 0);

                if (!isBlockedUp && Keyboard.IsJustPressed(Keys.Space)) {
                    Jump();
                }
            }

            if (!isBlockedLeft && Keyboard.IsPressed(Keys.A)) {
                direction = Direction.Left;
                // TODO: Use Acceleration
                Position.X -= updatedPlayerSpeed;
            }

            if (!isBlockedRight && Keyboard.IsPressed(Keys.D)) {
                direction = Direction.Right;
                Position.X += updatedPlayerSpeed;
            }

            Physics.DoAcceleration(this, delta);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            SpriteEffects spriteEffects = direction == Direction.Left ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Graphics.Draw(texture, Position, new Rectangle(0, 0, 37, 45), new Vector2(0, 45), spriteEffects);

            base.Draw(gameTime);
        }

        private Vector2 TileCoords() {
            return Tile.WorldToTileCoords(new Vector2(((Position.X / 16f) + 1f) * 16, Position.Y));
        }

        private void CheckCollisions() {
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

            isBlockedLeft = stopLeftPositions.Any(World.Tiles.ContainsKey);
            isBlockedRight = stopRightPositions.Any(World.Tiles.ContainsKey);
            isBlockedUp = stopUpPositions.Any(World.Tiles.ContainsKey);
            isOnFloor = stopDownPositions.Any(World.Tiles.ContainsKey);
        }

        private void Jump() {
            Acceleration.Y = -JUMPING_FORCE;
        }
    }
}
