﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen
{
    public class Tile : DrawableGameComponent
    {
        public Vector2 Position;
        internal Texture2D texture;
        public Item Item;

        public Tile(Game game, Vector2 position) : base(game)
        {
            Position = position;

            LoadContent(game.Content);
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Draw(texture, Position);

            base.Draw(gameTime);
        }

        public void Destroy(Game game)
        {
            game.Components.Remove(this);
            Item.Position = Position;
            game.Components.Add(Item);
        }
    }
}
