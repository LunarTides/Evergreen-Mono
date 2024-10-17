using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Evergreen
{
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }
        public Matrix InverseViewMatrix { get; protected set; }

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Position = Vector2.Zero;
        }

        private void UpdateVisibleArea()
        {
            InverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, InverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), InverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), InverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), InverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = Position + movePosition;
            Position = newPosition;
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom = Math.Clamp(Zoom + zoomAmount, .35f, 2f);
        }

        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();

            Position = Evergreen.Player.Position;

            if (Keyboard.IsJustPressed(Keys.PageUp))
            {
                AdjustZoom(.1f);
            }

            if (Keyboard.IsJustPressed(Keys.PageDown))
            {
                AdjustZoom(-.1f);
            }
        }
        public Vector2 DeprojectScreenPosition(Vector2 position)
        {
            return Vector2.Transform(position, InverseViewMatrix);
        }
        public Vector2 DeprojectScreenPosition(Point position) // For MouseState.Position
        {
            return Vector2.Transform(new Vector2(position.X, position.Y), InverseViewMatrix);
        }
    }
}
