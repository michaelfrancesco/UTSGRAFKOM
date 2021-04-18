using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace Tugas_Individu
{
    class Window : GameWindow
    {

        X07 x07 = new X07();
        DD3 dd3 = new DD3();
        g3r rbt = new g3r();

        Square_Texture wall = new Square_Texture();
        Square_Texture wall1 = new Square_Texture();
        Square_Texture wall2 = new Square_Texture();

        Square_Texture wall3 = new Square_Texture();
        Square_Texture wall4 = new Square_Texture();
        Square_Texture wall5 = new Square_Texture();

        bool animated = false;


        // We need an instance of the new camera class so it can manage the view and projection matrix code
        // We also need a boolean set to true to detect whether or not the mouse has been moved for the first time
        // Finally we add the last position of the mouse so we can calculate the mouse offset easily
        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        private double _time;
        private double _deltaTime;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            _time = RenderTime;
            _deltaTime = 0;

        }

        
       

        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.1f, 1);
            GL.Enable(EnableCap.DepthTest);

            float size = 15f;

            wall.load(0.0f,0.0f,0.0f,size,'y');
            wall1.load(0f, size,size,size, 'z');
            wall2.load(size,size,0,size, 'x');

            wall3.load(0.0f, 2*size, 0.0f, size, 'y');
            wall4.load(0f, size, -size, size, 'z');
            wall5.load(-size, size, 0f, size, 'x');


            dd3.load();
            rbt.load();
            x07.load();
            

            x07.translate(3.0f, 'd');

            // We initialize the camera so that it is 3 units back from where the rectangle is
            // and give it the proper aspect ratio
            _camera = new Camera(new Vector3(3.0f,3.0f,3.0f), Size.X / (float)Size.Y);
            

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement
            CursorGrabbed = true;

            base.OnLoad();
        }

        protected void rotate(float angle, char x)
        {
            x07.rotate(angle, x);
            dd3.rotate(angle, x);
            rbt.rotate(angle, x);
            
        }

        protected void rotate_center(float angle, char x)
        {
            dd3.rotate_center(angle, x);
        }

        protected void translate(float length, char x)
        {
            //x07.translate(length, x);
            //rbt.translate(length, x);
            dd3.translate(length, x);
        }

        protected void scale(float m)
        {
           x07.scale(m);
            dd3.scale(m);
            rbt.scale(m);
        }

        protected void keypress()
        {
            float angle = 0.7f;
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad4))
            {
                rotate(angle, 'y');

            }
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad6))
            {
                rotate(-angle, 'y');

            }

            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad8))
            {
                rotate(angle, 'x');

            }
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad2))
            {
                rotate(-angle, 'x');
            }

            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad7))
            {
                rotate(angle, 'z');
            }
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.KeyPad9))
            {
                rotate(-angle, 'z');
            }

            //float length = 0.1f;
            //if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W))
            //{
            //    translate(length, 'w');
            //}
            //if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S))
            //{
            //    translate(length, 's');
            //}
            //if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A))
            //{
            //    translate(length, 'a');
            //}
            //if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D))
            //{
            //    translate(length, 'd');
            //}

            float m = 0.01f;
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Up))
            {
                scale(m);
            }
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Down))
            {
                scale(-m);
            }
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.M))
            {
                OnLoad();
            }

            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.P))
            {
                if (!animated)
                {
                    animated = true;
                }
                else
                {
                    animated = false;
                }
            }
        }

        protected void use_camera()
        {
            if (!IsFocused) // check to see if the window is focused
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            

            const float cameraSpeed = 2.5f;
            const float sensitivity = 0.5f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float) RenderTime; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)RenderTime; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)RenderTime; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)RenderTime; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)RenderTime; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)RenderTime; // Down
            }

            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // this bool variable is initially set to true
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // reversed since y-coordinates range from bottom to top
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            keypress();
            use_camera();

            base.OnUpdateFrame(args);
        }

        protected void render()
        {
            dd3.render(_camera);
            rbt.render(_camera);
            x07.render(_camera);
            

            wall.render(_camera);
            wall1.render(_camera);
            wall2.render(_camera);

            wall3.render(_camera);
            wall4.render(_camera);
            wall5.render(_camera);
        }

        protected void animate()
        {
            x07.animate();
            rbt.animate();
            dd3.animate();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            _deltaTime = RenderTime - _time;
            _time = RenderTime;
            //Console.WriteLine(RenderTime);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (animated)
            {
                animate();
            }

            render();

            SwapBuffers();
            base.OnRenderFrame(args);
        }


        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _camera.Fov -= e.OffsetY;
            base.OnMouseWheel(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            // We need to update the aspect ratio once the window has been resized
            _camera.AspectRatio = Size.X / (float)Size.Y;
            base.OnResize(e);
        }
    }
}
