using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Evergreen
{
    public class Tile : DrawableGameComponent
    {
        public Vector2 Position;
        internal Texture2D texture;
        public Item Item;

        public Tile(Vector2 position) : base(Evergreen.Instance)
        {
            Position = position;

            LoadContent(Evergreen.Instance.Content);
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 tile_coords = Tile.MouseToTileCoords(mouseState.Position);

                if (tile_coords == TileCoords())
                {
                    Destroy();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Draw(texture, Position);

            base.Draw(gameTime);
        }

        public void Destroy()
        {
            Evergreen.Instance.Components.Remove(this);
            Item.Position = Position;
            Evergreen.Instance.Components.Add(Item);
        }

        public Vector2 TileCoords()
        {
            Vector2 tileCoords = Position / 16f;
            tileCoords.Floor();
            return tileCoords;
        }

        public static Vector2 WorldToTileCoords(Vector2 worldCoords)
        {
            Vector2 tileCoords = worldCoords / 16f;
            tileCoords.Floor();
            return tileCoords;
        }

        public static Vector2 MouseToTileCoords(Point mouseCoords)
        {
            return WorldToTileCoords(Evergreen.Camera.DeprojectScreenPosition(mouseCoords));
        }
    }
}
