using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Tugas_Individu
{
    class Circle : Mesh
    {
        float _radius;

        public Circle(List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals, List<uint> vertexIndices, int vertexBufferObject, int vertexArrayObject, Shader shader, int elementBufferObject, Matrix4 transform)
        {
            this.vertices = vertices;
            this.textureVertices = textureVertices;
            this.normals = normals;
            this.vertexIndices = vertexIndices;
            _vertexBufferObject = vertexBufferObject;
            _vertexArrayObject = vertexArrayObject;
            _shader = shader;
            _elementBufferObject = elementBufferObject;
            this.transform = transform;
        }

        public Circle(string color = "red", string name = "allen")
        {
            this.color = color;
            this.name = name;
        }

        public float getRadius() { return _radius; }

        public void createEllipsoidVertices(float _positionX = 0.0f, float _positionY = -0.4f, float _positionZ = 0.0f, float _radius = 0.3f)
        {
            this._positionX = _positionX;
            this._positionY = _positionY;
            this._positionZ = _positionZ;
            this._radius = _radius;
            Vector3 temp_vector;
            float _pi = 3.14f;

            for (float v = -_pi / 2; v <= _pi / 2; v += 0.01f)
            {
                for (float u = -_pi; u <= _pi; u += _pi / 30)
                {
                    temp_vector.X = _positionX + _radius * (float)Math.Cos(v) * (float)Math.Cos(u);
                    temp_vector.Y = _positionY + _radius * (float)Math.Cos(v) * (float)Math.Sin(u);
                    temp_vector.Z = _positionZ + _radius * (float)Math.Sin(v);
                    vertices.Add(temp_vector);
                }
            }

        }

        public void render()
        {
            
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertices.Count);


        }
    }
}
