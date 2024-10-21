using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen.System {
    public abstract class Graphics {
        public static void Draw(Texture2D texture, Vector2 position) {
            Evergreen.SpriteBatch.Begin(transformMatrix: Evergreen.Camera.Transform);
            Evergreen.SpriteBatch.Draw(texture, position, Color.White);
            Evergreen.SpriteBatch.End();
        }

        public static void Draw(Texture2D texture, Vector2 position, Rectangle sourceRectangle) {
            Evergreen.SpriteBatch.Begin(transformMatrix: Evergreen.Camera.Transform);
            Evergreen.SpriteBatch.Draw(texture, position, sourceRectangle, Color.White);
            Evergreen.SpriteBatch.End();
        }

        public static void Draw(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Vector2 origin, SpriteEffects spriteEffects) {
            Evergreen.SpriteBatch.Begin(transformMatrix: Evergreen.Camera.Transform);
            Evergreen.SpriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, origin, Vector2.One, spriteEffects, 0f);
            Evergreen.SpriteBatch.End();
        }

        public static void DrawUI(Texture2D texture, Vector2 position) {
            Evergreen.SpriteBatch.Begin();
            Evergreen.SpriteBatch.Draw(texture, position, Color.White);
            Evergreen.SpriteBatch.End();
        }
    }
}
