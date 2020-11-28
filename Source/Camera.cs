using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GraphFileManager
{
    public class Camera
    {
        public float Zoom { get; set; }
        public float Rotation { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Rotation = 0f;
            Position = Vector2.Zero;
        }


        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

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
            Transform = 
                    Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = new Vector2(Position.X + (float)(movePosition.Y * Math.Sin(Rotation)) + (float)(movePosition.X * Math.Cos(Rotation)),
                                              Position.Y + (float)(movePosition.Y * Math.Cos(Rotation)) - (float)(movePosition.X * Math.Sin(Rotation)));
            Position = newPosition;
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            //if (Zoom < .35f)
                //Zoom = .35f;
            //if (Zoom > 4f)
                //Zoom = 4f;
            if (Zoom < 0.001f)
                Zoom = 0.001f;
        }

        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();

            Vector2 cameraMovement = Vector2.Zero;
            int moveSpeed;

            if (Zoom > .8f) {
                moveSpeed = 15;
            } else if (Zoom < .8f && Zoom >= .6f) {
                moveSpeed = 25;
            } else if (Zoom < .6f && Zoom > .35f) {
                moveSpeed = 25;
            } else if (Zoom < .35f && Zoom > .20f) {
                moveSpeed = 30;
            } else if (Zoom <= .20f) {
                moveSpeed = 50;
            } else {
                moveSpeed = 10;
            }


            //if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                //cameraMovement.X = -(float)(moveSpeed * Math.Sin(Rotation));
                //cameraMovement.Y = -(float)(moveSpeed * Math.Cos(Rotation));
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                //cameraMovement.X = +(float)(moveSpeed * Math.Sin(Rotation));
                //cameraMovement.Y = +(float)(moveSpeed * Math.Cos(Rotation));
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                //cameraMovement.X = -(float)(moveSpeed * Math.Cos(Rotation));
                //cameraMovement.Y = +(float)(moveSpeed * Math.Sin(Rotation));
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                //cameraMovement.X = +(float)(moveSpeed * Math.Cos(Rotation));
                //cameraMovement.Y = -(float)(moveSpeed * Math.Sin(Rotation));
            //}
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                cameraMovement.Y = -moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                cameraMovement.Y = moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                cameraMovement.X = -moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                cameraMovement.X = moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E)) {
                Rotation += 0.05f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
                Rotation -= 0.05f;
            }

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue) {
                AdjustZoom(.1f);
            }
            if (currentMouseWheelValue < previousMouseWheelValue) {
                AdjustZoom(-.1f);
            }

            previousZoom = zoom;
            zoom = Zoom;

            MoveCamera(cameraMovement);
        }
    }
}

