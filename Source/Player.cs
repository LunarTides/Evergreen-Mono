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
        private Rectangle collisionBox;
        float speed;

        public Player(Game game) : base(game)
        {
            LoadContent(game.Content);
        }

        public override void Initialize()
        {
            Position = new Vector2(Evergreen.GraphicsManager.PreferredBackBufferWidth / 2, Evergreen.GraphicsManager.PreferredBackBufferHeight / 2);
            speed = 100f;
            
            collisionBox = new Rectangle(((int)Position.X), ((int)Position.X), 32, 32);

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
            CheckCollisions();

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

        private Vector2 TileCoords()
        {
            return Tile.WorldToTileCoords(Position);
        }

        private void CheckCollisions()
        {
            Vector2 pos = TileCoords();

            Vector2[] positions = [
                new Vector2(pos.X, pos.Y - 3),
                new Vector2(pos.X - 1, pos.Y - 2),
                new Vector2(pos.X + 1, pos.Y - 2),
                new Vector2(pos.X - 1, pos.Y - 1),
                new Vector2(pos.X + 1, pos.Y - 1),
                new Vector2(pos.X - 1, pos.Y),
                new Vector2(pos.X + 1, pos.Y),
                new Vector2(pos.X, pos.Y + 1),
            ];

            foreach (Vector2 position in positions)
            {
                // TODO
            }
        }

        public bool CollidesWithTile(Tile tile)
		{
			return collisionBox.Intersects(tile.collisionBox);
		}
    }
}
