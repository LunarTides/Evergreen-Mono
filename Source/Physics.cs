using System;

namespace Evergreen
{
    public abstract class Physics
    {
        const float ACCELERATION_SPEED = 1.02f;
        const float ACCELERATION_MINIMUM = 1f;
        const float ACCELERATION_MAXIMUM = 5f;

        const float GRAVITY_SCALE = 50f;

        public static void ApplyGravity(Item component, float delta)
        {
            component.Acceleration.Y = Math.Clamp(component.Acceleration.Y * ACCELERATION_SPEED, ACCELERATION_MINIMUM, ACCELERATION_MAXIMUM);
            component.Position.Y += component.Acceleration.Y * GRAVITY_SCALE * delta;
        }
    }
}
