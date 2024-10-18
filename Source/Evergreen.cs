using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

            WorldGen = new WorldGen();

            // TODO: Remove
            WorldGen.GenerateWorld(Random.Shared.Next());

            Inventory = new();

            Player = new(this);
            Components.Add(Player);

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

            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 tile_coords = Tile.MouseToTileCoords(mouseState.Position);

                foreach (IGameComponent component in Evergreen.Instance.Components)
                {
                    if (component is Tile tile && tile.Position == tile_coords)
                    {
                        tile.Destroy();
                        break;
                    }
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
