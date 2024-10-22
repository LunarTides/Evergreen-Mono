using Evergreen.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Evergreen {
    public abstract class Item : PhysicsObject {
        internal Texture2D texture;

        public Item(Vector2 position) : base() {
            Position = position;

            LoadContent(Evergreen.Instance.Content);
        }

        public virtual void LoadContent(ContentManager content) {}

        public override void Update(GameTime gameTime) {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float playerDistance = Vector2.Distance(Position, Evergreen.Player.Position);

            if (playerDistance < 100f) {
                if (playerDistance < 10f) {
                    PickUp();
                }

                FloatTowardsPlayer(delta);
                hasGravity = false;
            } else {
                hasGravity = true;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            Graphics.Draw(texture, Position);

            base.Draw(gameTime);
        }

        private Vector2 TileCoords() {
            return Tile.WorldToTileCoords(Position);
        }

        internal override void CheckCollisions() {
            Vector2 coords = TileCoords();
            isOnFloor = World.Tiles.ContainsKey(new Vector2(coords.X, coords.Y + 1));
        }

        private void FloatTowardsPlayer(float delta) {
            Velocity.Y = 0;
            Position += Vector2.Normalize(Evergreen.Player.Position - Position) * 100 * delta;
        }

        private void PickUp() {
            Sound.Play("Grab");
            Evergreen.Instance.Components.Remove(this);
            Inventory.Add(this);
        }
    }
}
