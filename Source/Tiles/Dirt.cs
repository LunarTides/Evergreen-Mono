using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen.Tiles
{
    public class Dirt : Tile
    {
        public Dirt(Vector2 position) : base(position)
        {
            Item = new Items.Dirt(position);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("tile/dirt");
            base.LoadContent(content);
        }
    }
}
