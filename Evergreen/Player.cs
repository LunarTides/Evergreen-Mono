using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Evergreen
{
    public class Player : DrawableGameComponent
    {
        public Vector2 Position;
        Texture2D texture;
        float speed;

        public Player(Game game) : base(game)
        {
            LoadContent(game.Content);
        }

        public override void Initialize()
        {
            Position = new Vector2(Evergreen.GraphicsManager.PreferredBackBufferWidth / 2, Evergreen.GraphicsManager.PreferredBackBufferHeight / 2);
            speed = 100f;

            base.Initialize();
        }

        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            float updatedPlayerSpeed = speed * (float)delta;

            if (Keyboard.IsPressed(Keys.A))
            {
                Position.X -= updatedPlayerSpeed;
            }

            if (Keyboard.IsPressed(Keys.D))
            {
                Position.X += updatedPlayerSpeed;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Draw(texture, Position);

            base.Draw(gameTime);
        }
    }
}
