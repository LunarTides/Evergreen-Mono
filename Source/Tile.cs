using Evergreen.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Evergreen
{
    public abstract class Tile : DrawableGameComponent
    {
        const uint TILE_SIZE = 16;

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

        public override void Draw(GameTime gameTime)
        {
            Rectangle source = new(16, 16, 16, 16);
            Graphics.Draw(texture, new Vector2(Position.X, Position.Y + 1) * TILE_SIZE, source, new Vector2(0, TILE_SIZE), SpriteEffects.None);

            base.Draw(gameTime);
        }

        public static T Create<T>(Vector2 position) where T : Tile
        {
            T tile = (T)Activator.CreateInstance(typeof(T), [position]);
            Evergreen.Instance.Components.Add(tile);
            World.Tiles.Add(position, tile);
            return tile;
        }

        public void Destroy()
        {
            Sound.Play("Dig");
            Evergreen.Instance.Components.Remove(this);
            World.Tiles.Remove(Position);
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
