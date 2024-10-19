using Evergreen.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keyboard = Evergreen.System.Keyboard;

namespace Evergreen
{
    public class Evergreen : Game
    {
        public static Player Player;
        public static Inventory Inventory;
        public static Camera Camera;
        public static Evergreen Instance;
        public static WorldGen WorldGen;
        public static World World;

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

            World = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Sound.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.IsPressed(Keys.Escape))
            {
                Exit();
            }

            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 tile_coords = Tile.MouseToTileCoords(mouseState.Position);
                if (World.Tiles.TryGetValue(tile_coords, out Tile tile))
                {
                    tile.Destroy();
                }
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
