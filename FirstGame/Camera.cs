using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame
{
    public class Camera
    {
        // We need this to calculate the aspectRatio
        // in the ProjectionMatrix property.
        GraphicsDevice graphicsDevice;
        private MouseState oldState;
        private float currentTime = 0;
        private float value = 0;
        private float duration = 1000;
        private bool isAnimating = false;
        private float cametaHeight = 20f;

        Vector3 position = new Vector3(0, 20, 10);
        float angle;

        public Matrix ViewMatrix
        {
            get
            {
                var lookAtVector = new Vector3(0, -0.5f, -.5f);
                // We'll create a rotation matrix using our angle
                var rotationMatrix = Matrix.CreateRotationZ(angle);
                // Then we'll modify the vector using this matrix:
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                lookAtVector += position;

                var upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(
                    position, lookAtVector, upVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 200;
                float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;

                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public Camera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {                
            HandleKeyboardInput(gameTime);
            HandleMouseInput(gameTime);
        }

        private void HandleMouseInput(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            var scrollDelta = this.oldState.ScrollWheelValue - mouseState.ScrollWheelValue;

            if (scrollDelta != 0f)
            {
                Vector3 forwardVector;
                if (scrollDelta > 0)
                {
                    forwardVector = new Vector3(0, 0, 1);
                }
                else
                {
                    forwardVector = new Vector3(0, 0, -1);
                }

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                float unitsPerSecond = Math.Abs((float)scrollDelta);

                this.position += forwardVector * unitsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                this.isAnimating = true;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && this.oldState.LeftButton == ButtonState.Pressed)
            {
                if (this.isAnimating)
                {
                    this.currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    this.value += (this.currentTime / this.duration) * 0.01f;
                    this.value = this.value < 0.01f ? this.value : 0.01f;

                    if (this.currentTime >= this.duration)
                    {
                        this.isAnimating = false;
                    }
                }

                var delta = CalculateXPositionDelta(this.oldState, mouseState);
                angle += (float)delta * this.value;
            }

            if ((this.oldState.LeftButton == ButtonState.Pressed || this.oldState.LeftButton == ButtonState.Released) && mouseState.LeftButton == ButtonState.Released)
            {
                this.isAnimating = false;
                this.currentTime = 0f;
                this.duration = 1000;
                this.value = 0f;
            }

            this.oldState = mouseState;
        }

        private int CalculateXPositionDelta(MouseState oldMouseState, MouseState newMouseState)
        {
            return oldState.Position.X - newMouseState.Position.X;
        }

        public void HandleKeyboardInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                angle -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                var forwardVector = new Vector3(0, -1, 0);

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 3;

                this.position += forwardVector * unitsPerSecond *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                var forwardVector = new Vector3(0, -1, 0);

                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 3;

                this.position -= forwardVector * unitsPerSecond *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
