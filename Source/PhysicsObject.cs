using Microsoft.Xna.Framework;
using System;

namespace Evergreen {
    public class PhysicsObject : DrawableGameComponent {
        private const float GRAVITY = 9.81f;
        private const float TERMINAL_VELOCITY = 50f;

        private const float AIR_RESISTANCE = 10f;

        public Vector2 Position;
        public Vector2 Velocity = Vector2.Zero;
        internal bool hasGravity = true;
        internal bool isOnFloor = false;
        internal bool shouldSlowDown = true;

        public PhysicsObject() : base(Evergreen.Instance) { }

        public override void Update(GameTime gameTime) {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            CheckCollisions();

            if (hasGravity && !isOnFloor) {
                Velocity.Y += GRAVITY * delta;
                Velocity.Y = Math.Min(Velocity.Y, TERMINAL_VELOCITY);
            } else {
                Velocity.Y = Math.Min(Velocity.Y, 0f);
            }

            Position.Y += Velocity.Y;
            Position.X += Velocity.X;

            if (shouldSlowDown) {
                Velocity.X = Vector2.Lerp(Velocity, Vector2.Zero, AIR_RESISTANCE * delta).X;
            }

            base.Update(gameTime);
        }

        internal virtual void CheckCollisions() { }
    }
}
