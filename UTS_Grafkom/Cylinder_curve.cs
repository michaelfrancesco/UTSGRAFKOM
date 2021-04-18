using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Tugas_Individu
{
    class Cylinder_Curve : Mesh
    {
        
        float _radius;
        float _height;
        float _extended;

        public Cylinder_Curve(List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals, List<uint> vertexIndices, int vertexBufferObject, int vertexArrayObject, Shader shader, int elementBufferObject, Matrix4 transform)
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

        public Cylinder_Curve(string color = "red", string name = "allen")
        {
            this.color = color;
            this.name = name;
        }



        public float getRadius() { return _radius; }
        public float getHeight() { return _height; }


        public void createEllipsoidVertices(float _positionX = 0.4f,
        float _positionY = 0.4f,
        float _positionZ = 0.4f,
        float _radius = 0.3f, float _height= 0.2f, float _extended = 0.5f)
        {
            this._positionX = _positionX;
            this._positionY = _positionY;
            this._positionZ = _positionZ;
            this._radius = _radius;
            this._height = _height;
            this._extended = _extended;

            Vector3 temp_vector;
            float _pi = (float)Math.PI;


            for (float v = -_height / 2; v <= (_height / 2); v += 0.0001f)
            {
                Vector3 p = setBeizer((v+(_height/2))/_height);
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {
                
                    temp_vector.X = p.X + _radius * (float)Math.Cos(u);
                    temp_vector.Y = p.Y + _radius * (float)Math.Sin(u);
                    temp_vector.Z = _positionZ +  v;

                    vertices.Add(temp_vector);
 
                }
            }

           

        }


        Vector3 setBeizer(float t)
        {
            //Console.WriteLine(t);
            Vector3 p = new Vector3(0f, 0f,0f);
            float[] k = new float[3];

            k[0] = (float)Math.Pow((1 - t), 3 - 1 - 0) * (float)Math.Pow(t, 0) * 1;
            k[1] = (float)Math.Pow((1 - t), 3 - 1 - 1) * (float)Math.Pow(t, 1) * 2;
            k[2] = (float)Math.Pow((1 - t), 3 - 1 - 2) * (float)Math.Pow(t, 2) * 1;


            //titik 1
            p.X += k[0] * _positionX;
            p.Y += k[0] * _positionY - _height;

            //titik 2
            p.X += k[1] * (_positionX + _extended);
            p.Y += k[1] * _positionY;

            //titik 3
            p.X += k[2] * _positionX;
            p.Y += k[2] * _positionY + _height;

            //Console.WriteLine(p.X + " "+ p.Y);

            return p;

        }


        public void render()
        {
            
            
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, vertices.Count);

        }



    }

}
