using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Tugas_Individu
{
    class Body : Mesh
    {
        
        float _radius;
        float _radius_x;

        public Body(List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals, List<uint> vertexIndices, int vertexBufferObject, int vertexArrayObject, Shader shader, int elementBufferObject, Matrix4 transform)
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

        public Body(string color = "red", string name = "allen")
        {
            this.color = color;
            this.name = name;
        }

        public float getRadius() { return _radius; }

        public void createEllipsoidVertices(float _positionX = 0.4f,
        float _positionY = 0.4f,
        float _positionZ = 0.4f, 
        float _radius = 0.5f, float _radius_x = 0.3f)
        {
            this._positionX = _positionX;
            this._positionY = _positionY;
            this._positionZ = _positionZ;
            this._radius = _radius;
            this._radius_x = _radius_x;

            Vector3 temp_vector;
            float _pi = (float)Math.PI;


            for (float v = 0; v <= _pi / 2; v += 0.0001f)
            {
                for (float u = -_pi; u <= _pi; u += _pi / 30)
                {
                    temp_vector.X = _positionX + _radius_x * (float)Math.Cos(v) * (float)Math.Cos(u); //x
                    temp_vector.Y = _positionY + _radius * 2.0f * (float)Math.Sin(v); //y
                    //temp_vector.Z = _positionZ + _radius * (float)Math.Sin(v); //z
                    temp_vector.Z = _positionZ + _radius * (float)Math.Cos(v) * (float)Math.Sin(u); //z
                    vertices.Add(temp_vector);
                }
                //Console.WriteLine("v : " + v);
            }

        }

       

        public void render()
        {
            //rotate_center(0.01f, 'x');
            
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, vertices.Count);

        }

    }

}
