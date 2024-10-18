using Evergreen.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Evergreen
{
    public class Tile : DrawableGameComponent
    {
        const uint TILE_SIZE = 16;

        public Vector2 Position;
        internal Texture2D texture;
        public Rectangle collisionBox;
        public Item Item;

        public Tile(Vector2 position) : base(Evergreen.Instance)
        {
            Position = position;
            collisionBox = new Rectangle(((int)Position.X), ((int)Position.X), ((int)TILE_SIZE), ((int)TILE_SIZE));

            LoadContent(Evergreen.Instance.Content);
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Draw(texture, Position * TILE_SIZE);

            base.Draw(gameTime);
        }

        public static T Create<T>(Vector2 position) where T : Tile
        {
            T tile = (T)Activator.CreateInstance(typeof(T), [position]);
            Evergreen.Instance.Components.Add(tile);
            return tile;
        }

        public void Destroy()
        {
            Evergreen.Instance.Components.Remove(this);
            Item.Position = Position * TILE_SIZE;
            Evergreen.Instance.Components.Add(Item);
        }

        public static Vector2 WorldToTileCoords(Vector2 worldCoords)
        {
            return Vector2.Floor(worldCoords / TILE_SIZE);
        }

        public static Vector2 MouseToTileCoords(Point mouseCoords)
        {
            return WorldToTileCoords(Evergreen.Camera.DeprojectScreenPosition(mouseCoords));
        }
    }
}
