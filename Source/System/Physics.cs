using System;
using Microsoft.Xna.Framework;

namespace Evergreen.System
{
    public abstract class Physics
    {
        const float ACCELERATION_SPEED = 1.02f;
        const float ACCELERATION_MINIMUM = 1f;
        const float ACCELERATION_MAXIMUM = 5f;

        const float GRAVITY_SCALE = 50f;

        private static float[] CalculateGravity(Vector2 acceleration, float delta)
        {
            return [
                Math.Clamp(acceleration.Y * ACCELERATION_SPEED, ACCELERATION_MINIMUM, ACCELERATION_MAXIMUM),
                acceleration.Y * GRAVITY_SCALE * delta,
            ];
        }

        public static void ApplyGravity(Item item, float delta)
        {
            float[] result = CalculateGravity(item.Acceleration, delta);

            item.Acceleration.Y = result[0];
            item.Position.Y += result[1];
        }

        public static void ApplyGravity(Player player, float delta)
        {
            float[] result = CalculateGravity(player.Acceleration, delta);

            player.Acceleration.Y = result[0];
            player.Position.Y += result[1];
        }
    }
}
