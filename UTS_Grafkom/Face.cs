using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Tugas_Individu
{
    class Face : Mesh
    {
        
        float _radius;

        public Face(List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals, List<uint> vertexIndices, int vertexBufferObject, int vertexArrayObject, Shader shader, int elementBufferObject, Matrix4 transform)
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

        public Face(string color = "red", string name = "allen")
        {
            this.color = color;
            this.name = name;
        }

        public void createEllipsoidVertices(float _positionX = 0.4f,
        float _positionY = 0.4f,
        float _positionZ = 0.4f, 
        float _radius = 0.3f)
        {
            this._positionX = _positionX;
            this._positionY = _positionY;
            this._positionZ = _positionZ;
            this._radius = _radius;

            Vector3 temp_vector;
            float _pi = (float)Math.PI;


            for (float v = -_pi / 2; v <= 0; v += 0.01f)
            {
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {
                    temp_vector.X = _positionX + _radius * 1.2f * (float)Math.Cos(v) * (float)Math.Cos(u); //x
                    temp_vector.Y = _positionY + _radius * 0.425f * (float)Math.Cos(v) * (float)Math.Sin(u); //y
                    temp_vector.Z = _positionZ + _radius * (float)Math.Sin(v); //z
                    vertices.Add(temp_vector);
                }
            }

        }

       

        public void render()
        {
            //rotate_center(0.01f, 'x');
            
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertices.Count);

        }

    }

}
