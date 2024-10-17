using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen
{
    public class Item : DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 Acceleration = Vector2.Zero;
        internal Texture2D texture;

        public Item(Vector2 position) : base(Evergreen.Instance) {
            Position = position;

            LoadContent(Evergreen.Instance.Content);
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Physics.ApplyGravity(this, delta);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Draw(texture, Position);

            base.Draw(gameTime);
        }
    }
}
