using System;
using Microsoft.Xna.Framework;

namespace Evergreen.System {
    public abstract class Physics {
        const float ACCELERATION_SPEED = 1.02f;
        const float ACCELERATION_MINIMUM = 1f;
        const float ACCELERATION_MAXIMUM = 5f;

        public const float GRAVITY_SCALE = 50f;
        private const double AIR_RESISTANCE = 200f;

        private static float CalculateGravity(Vector2 acceleration) {
            return Math.Clamp(acceleration.Y * ACCELERATION_SPEED, ACCELERATION_MINIMUM, ACCELERATION_MAXIMUM);
        }

        // TODO: Try to use generics instead of overloading to avoid duplicate methods.
        public static void ApplyGravity(Item item) {
            item.Acceleration.Y = CalculateGravity(item.Acceleration);
        }

        public static void ApplyGravity(Player player) {
            player.Acceleration.Y = CalculateGravity(player.Acceleration);
        }

        public static void DoAcceleration(Item item, float delta) {
            float yVelocity = item.Acceleration.Y * delta;
            if (item.Acceleration.Y > 0) {
                yVelocity *= GRAVITY_SCALE;
            }

            item.Position.Y += yVelocity;
            item.Position.X += item.Acceleration.X * delta;

            if (item.Acceleration.X != 0f) {
                double xSign = Math.Sign(item.Acceleration.X);
                item.Acceleration.X -= (float)(AIR_RESISTANCE * Math.Sign(item.Acceleration.X) * delta);
                if (Math.Sign(item.Acceleration.X) != xSign) {
                    item.Acceleration.X = 0f;
                }
            }

            if (item.Acceleration.Y != 0f) {
                double ySign = Math.Sign(item.Acceleration.Y);
                item.Acceleration.Y -= (float)(AIR_RESISTANCE * Math.Sign(item.Acceleration.Y) * delta);
                if (Math.Sign(item.Acceleration.Y) != ySign) {
                    item.Acceleration.Y = 0f;
                }
            }
        }

        public static void DoAcceleration(Player player, float delta) {
            float yVelocity = player.Acceleration.Y * delta;
            if (player.Acceleration.Y > 0) {
                yVelocity *= GRAVITY_SCALE;
            }

            player.Position.Y += yVelocity;
            player.Position.X += player.Acceleration.X * delta;

            if (player.Acceleration.X != 0f) {
                double xSign = Math.Sign(player.Acceleration.X);
                player.Acceleration.X -= (float)(AIR_RESISTANCE * Math.Sign(player.Acceleration.X) * delta);
                if (Math.Sign(player.Acceleration.X) != xSign) {
                    player.Acceleration.X = 0f;
                }
            }

            if (player.Acceleration.Y != 0f) {
                double ySign = Math.Sign(player.Acceleration.Y);
                player.Acceleration.Y -= (float)(AIR_RESISTANCE * Math.Sign(player.Acceleration.Y) * delta);
                if (Math.Sign(player.Acceleration.Y) != ySign) {
                    player.Acceleration.Y = 0f;
                }
            }
        }
    }
}
