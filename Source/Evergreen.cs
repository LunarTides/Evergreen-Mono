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
        public static Evergreen Instance;

        public static GraphicsDeviceManager GraphicsManager;
        public static SpriteBatch SpriteBatch;

        public Evergreen()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Instance = this;
            Camera = new(GraphicsDevice.Viewport);

            Player = new(this);
            Components.Add(Player);

            // TODO: Remove
            Dirt dirt = new(Vector2.Zero);
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
