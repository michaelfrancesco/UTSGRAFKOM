using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Tugas_Individu
{
    class Square_Texture
    {
        float _positionX;
        float _positionY;
        float _positionZ;
        float _length;

        private readonly float[] _vertices =
       {
            // Position         Texture coordinates
             0.9f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int _elementBufferObject;

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private Shader _shader;

        //penambahan texture
        private Texture _texture;

        protected Matrix4 transform;

        public Square_Texture() { }

        protected void setup(char x)
        {
            switch (x)
            {
                case 'x':
                    //top-right
                    _vertices[0] = _positionX;
                    _vertices[1] = _positionY + _length;
                    _vertices[2] = _positionZ + _length;

                    //bottom-right
                    _vertices[5] = _positionX;
                    _vertices[6] = _positionY - _length;
                    _vertices[7] = _positionZ + _length;

                    //bottom-left
                    _vertices[10] = _positionX;
                    _vertices[11] = _positionY - _length;
                    _vertices[12] = _positionZ - _length;

                    //top-left
                    _vertices[15] = _positionX;
                    _vertices[16] = _positionY + _length;
                    _vertices[17] = _positionZ - _length;

                    break;
                case 'y':
                    //top-right
                    _vertices[0] = _positionX + _length;
                    _vertices[1] = _positionY ;
                    _vertices[2] = _positionZ + _length;

                    //bottom-right
                    _vertices[5] = _positionX - _length;
                    _vertices[6] = _positionY ;
                    _vertices[7] = _positionZ + _length;

                    //bottom-left
                    _vertices[10] = _positionX - _length;
                    _vertices[11] = _positionY ;
                    _vertices[12] = _positionZ - _length;

                    //top-left
                    _vertices[15] = _positionX + _length;
                    _vertices[16] = _positionY ;
                    _vertices[17] = _positionZ - _length;
                    break;
                case 'z':
                    //top-right
                    _vertices[0] = _positionX + _length;
                    _vertices[1] = _positionY + _length;
                    _vertices[2] = _positionZ ;

                    //bottom-right
                    _vertices[5] = _positionX - _length;
                    _vertices[6] = _positionY + _length;
                    _vertices[7] = _positionZ;

                    //bottom-left
                    _vertices[10] = _positionX - _length;
                    _vertices[11] = _positionY - _length;
                    _vertices[12] = _positionZ ;

                    //top-left
                    _vertices[15] = _positionX + _length;
                    _vertices[16] = _positionY - _length;
                    _vertices[17] = _positionZ ;
                    break;
            }
        }


        public void load(float _positionX, float _positionY, float _positionZ, float _length ,char x)
        {
            this._positionX = _positionX;
            this._positionY = _positionY;
            this._positionZ = _positionZ;
            this._length = _length;

            setup(x);


            transform = Matrix4.Identity;
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            // ada perubahan di shader filenya
            _shader = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_texture.vert", 
                "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_texture.frag");
            _shader.Use();

            //penambahan disini
            var vertexLocation = _shader.GetAttribLocation("aPosition");

            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(vertexLocation);

            //penambahan disini
            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");

            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(texCoordLocation);
            _texture = Texture.LoadFromFile("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Resources/wall.png");
            _texture.Use(TextureUnit.Texture0);
        }

        public void render(Camera _camera)
        {
            GL.BindVertexArray(_vertexArrayObject);

            _texture.Use(TextureUnit.Texture0);
            _shader.Use();
            _shader.SetMatrix4("transform", transform);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());


            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }


    }
}
