using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen
{
    public class Graphics
    {
        public static void Draw(Texture2D texture, Vector2 position)
        {
            Evergreen.SpriteBatch.Begin(transformMatrix: Evergreen.Camera.Transform);
            Evergreen.SpriteBatch.Draw(texture, position, Color.White);
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
