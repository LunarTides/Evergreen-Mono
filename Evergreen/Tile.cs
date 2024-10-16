using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen
{
    public class Tile : DrawableGameComponent
    {
        public Vector2 Position;
        internal Texture2D texture;

        public Tile(Game game, Vector2 position) : base(game)
        {
            Position = position;

            LoadContent(game.Content);
        }

        //public override void Initialize()
        //{
        //    base.Initialize();
        //}

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
    }
}
