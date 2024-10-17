using Evergreen.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Evergreen
{
    public class Evergreen : Game
    {
        public static Player Player;
        public static Camera Camera;

        public static GraphicsDeviceManager GraphicsManager;
        public static SpriteBatch SpriteBatch;

        // TODO: Remove
        Dirt dirt;

        public Evergreen()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Camera = new(GraphicsDevice.Viewport);

            Player = new(this);
            Components.Add(Player);

            // TODO: Remove
            dirt = new(this, Vector2.Zero);
            Components.Add(dirt);

            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.IsPressed(Keys.Escape))
            {
                Exit();
            }

            // TODO: Remove
            if (Keyboard.IsJustPressed(Keys.F1))
            {
                dirt.Destroy(this);
            }

            Camera.UpdateCamera(GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
