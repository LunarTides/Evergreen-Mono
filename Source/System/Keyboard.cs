using Microsoft.Xna.Framework.Input;

namespace Evergreen.System {
    public abstract class Keyboard {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static KeyboardState GetState() {
            previousKeyState = currentKeyState;
            currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            return currentKeyState;
        }

        public static bool IsHolding(Keys key) {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool IsPressed(Keys key) {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static int GetAxis(Keys positiveKey, Keys negativeKey) {
            return IsHolding(positiveKey) ? 1 : IsHolding(negativeKey) ? -1 : 0;
        }
    }
}
