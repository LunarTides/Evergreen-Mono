using Evergreen.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keyboard = Evergreen.System.Keyboard;

namespace Evergreen
{
    public class Player : DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 Acceleration = Vector2.Zero;
        private Texture2D texture;
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
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updatedPlayerSpeed = speed * delta;

            Physics.ApplyGravity(this, delta);

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
