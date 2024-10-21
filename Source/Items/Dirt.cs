using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen.Items {
    internal class Dirt : Item {
        public Dirt(Vector2 position) : base(position) {}

        public override void LoadContent(ContentManager content) {
            texture = content.Load<Texture2D>("Images/Item_2");
            base.LoadContent(content);
        }
    }
}
