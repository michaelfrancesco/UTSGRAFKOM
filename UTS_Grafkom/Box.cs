using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Tugas_Individu
{
    class Box : Mesh
    {
        

        float _boxLengthX ;
        float _boxLengthY ;
        float _boxLengthZ ;



        public Box(List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals, List<uint> vertexIndices, int vertexBufferObject, int vertexArrayObject, Shader shader, int elementBufferObject, Matrix4 transform)
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

        public Box(string color = "red", string name = "allen")
        {
            this.color = color;
            this.name = name;
        }

        public float getLengthX() { return _boxLengthX; }
        public float getLengthY() { return _boxLengthY; }
        public float getLengthZ() { return _boxLengthZ; }

        public void createBoxvertices(float _positionX = 0.0f,
        float _positionY = 0.0f,
        float _positionZ = 0.0f,

        float _boxLengthX = 0.5f,
        float _boxLengthY = 0.5f,
        float _boxLengthZ = 0.5f)
        {
            this._positionX = _positionX;
            this._positionY = _positionY;
            this._positionZ = _positionZ;

            this._boxLengthX = _boxLengthX;
            this._boxLengthY = _boxLengthY;
            this._boxLengthZ = _boxLengthZ;

            Vector3 temp_vector;


            //titik 1
            temp_vector.X = _positionX - _boxLengthX / 2.0f;
            temp_vector.Y = _positionY + _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ - _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //titik 2
            temp_vector.X = _positionX + _boxLengthX / 2.0f;
            temp_vector.Y = _positionY + _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ - _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //titik 3
            temp_vector.X = _positionX - _boxLengthX / 2.0f;
            temp_vector.Y = _positionY - _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ - _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //titik 4
            temp_vector.X = _positionX + _boxLengthX / 2.0f;
            temp_vector.Y = _positionY - _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ - _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //titik 5
            temp_vector.X = _positionX - _boxLengthX / 2.0f;
            temp_vector.Y = _positionY + _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ + _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //titik 6
            temp_vector.X = _positionX + _boxLengthX / 2.0f;
            temp_vector.Y = _positionY + _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ + _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //titik 7
            temp_vector.X = _positionX - _boxLengthX / 2.0f;
            temp_vector.Y = _positionY - _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ + _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //titik 8
            temp_vector.X = _positionX + _boxLengthX / 2.0f;
            temp_vector.Y = _positionY - _boxLengthY / 2.0f;
            temp_vector.Z = _positionZ + _boxLengthZ / 2.0f;
            vertices.Add(temp_vector);

            //2. Insialisasi verticesindices
            vertexIndices = new List<uint>
        {
                //Segitiga Depan 1
                0,1,2,
            //Segitiga Depan 2
            1,2,3,
            //Segitiga Atas 1
            0,4,5,
            //Segitiga Atas 2
            0,1,5,
            //Segitiga Kiri 1
            0,2,4,
            //Segitiga Kiri 2
            2,4,6,
            //Segitiga Belakang 1
            4,5,6,
            //Segitiga Belakang 2
            5,6,7,
            //Segitiga Bawah 1
            2,3,6,
            //Segitiga Bawah 2
            3,6,7,
            //Segitiga Kanan 1
            1,3,5,
            //Segitiga Kanan 2
            3,5,7

        };
        }

       

        public void render()
        {
            //rotate_center(0.01f, 'x');
            
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, vertexIndices.Count, DrawElementsType.UnsignedInt, 0);

        }

    }

}
