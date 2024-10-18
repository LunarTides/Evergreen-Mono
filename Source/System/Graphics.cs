using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen.System
{
    public abstract class Graphics
    {
        public static void Draw(Texture2D texture, Vector2 position)
        {
            Evergreen.SpriteBatch.Begin(transformMatrix: Evergreen.Camera.Transform);
            Evergreen.SpriteBatch.Draw(texture, position, Color.White);
            Evergreen.SpriteBatch.End();
        }

        public static void Draw(Texture2D texture, Vector2 position, Vector2 origin)
        {
            Evergreen.SpriteBatch.Begin(transformMatrix: Evergreen.Camera.Transform);
            Evergreen.SpriteBatch.Draw(texture, position, null, Color.White, 0f, origin, Vector2.One, SpriteEffects.None, 0f);
            Evergreen.SpriteBatch.End();
        }

        public static void DrawUI(Texture2D texture, Vector2 position)
        {
            Evergreen.SpriteBatch.Begin();
            Evergreen.SpriteBatch.Draw(texture, position, Color.White);
            Evergreen.SpriteBatch.End();
        }
    }
}
