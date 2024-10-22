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

    public class Player : PhysicsObject {

        private Texture2D texture;
        private Direction direction = Direction.Right;
        private bool isBlockedRight = false;
        private bool isBlockedLeft = false;
        private bool isBlockedUp = false;
        public float AccelerationSpeed = 3f;
        public float MaxSpeed = 3f;
        public float JumpForce = 5f;

        public Player() : base() {
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

            // TODO: Remove.
            if (Keyboard.IsPressed(Keys.R)) {
                Position = new Vector2(Evergreen.GraphicsManager.PreferredBackBufferWidth / 2, Evergreen.GraphicsManager.PreferredBackBufferHeight / 2);
            }

            if (isOnFloor && !isBlockedUp && Keyboard.IsPressed(Keys.Space)) {
                Jump();
            }

            int axis = Keyboard.GetAxis(Keys.D, Keys.A);

            Velocity.X += AccelerationSpeed * axis * delta;
            Velocity.X = Math.Clamp(Velocity.X, -MaxSpeed, MaxSpeed);

            shouldSlowDown = axis == 0;

            if (axis != 0) {
                direction = axis == 1 ? Direction.Right : Direction.Left;

                if ((axis == 1 && isBlockedRight) || (axis == -1 && isBlockedLeft)) {
                    Velocity.X = 0;
                }
            }

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

        internal override void CheckCollisions() {
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
            Velocity.Y = -JumpForce;
        }
    }
}
