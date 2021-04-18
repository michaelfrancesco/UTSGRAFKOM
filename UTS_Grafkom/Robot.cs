using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Tugas_Individu
{
    class Robot
    {
        //kubus kiri
        protected List<Vector3> _box_vertices = new List<Vector3>();
        protected List<uint> vertexIndices_box = new List<uint>();
        // private float[] _box_vertices = new float[24]; //8 titik, masing-masing 3 koordinat
        private int _vertexBufferObject_box;
        private int _vertexArrayObject_box;
        private Shader _shader_box;
        private int _elementBufferObject_box;

        //kubus kanan
        protected List<Vector3> _box_vertices_right = new List<Vector3>();
        protected List<uint> vertexIndices_box_right = new List<uint>();
        // private float[] _box_vertices = new float[24]; //8 titik, masing-masing 3 koordinat
        private int _vertexBufferObject_box_right;
        private int _vertexArrayObject_box_right;
        private Shader _shader_box_right;
        private int _elementBufferObject_box_right;

        //elipsoid
        protected List<Vector3> _elipsoid_vertices = new List<Vector3>();
        //private float[] _elipsoid_vertices = new float[1900 * 3];
        private int _elipsoid_index = 0;
        private int _vertexBufferObject_elipsoid;
        private int _vertexArrayObject_elipsoid;
        private Shader _shader_elipsoid;

        //hyperboloid
        private float[] _hyperboloid_vertices = new float[1900 * 3];
        private int _hyperboloid_index = 0;
        private int _vertexBufferObject_hyperboloid;
        private int _vertexArrayObject_hyperboloid;
        private Shader _shader_hyperboloid;

        //Face
        protected List<Vector3> _elipsoid_vertices_face = new List<Vector3>();
        //private float[] _elipsoid_vertices = new float[1900 * 3];
        private int _elipsoid_index_face = 0;
        private int _vertexBufferObject_elipsoid_face;
        private int _vertexArrayObject_elipsoid_face;
        private Shader _shader_elipsoid_face;

        //lefteye
        protected List<Vector3> _elipsoid_vertices_lefteye = new List<Vector3>();
        //private float[] _elipsoid_vertices = new float[1900 * 3];
        private int _elipsoid_index_lefteye = 0;
        private int _vertexBufferObject_elipsoid_lefteye;
        private int _vertexArrayObject_elipsoid_lefteye;
        private Shader _shader_elipsoid_lefteye;

        //right eye
        protected List<Vector3> _elipsoid_vertices_righteye = new List<Vector3>();
        //private float[] _elipsoid_vertices = new float[1900 * 3];
        private int _elipsoid_index_righteye = 0;
        private int _vertexBufferObject_elipsoid_righteye;
        private int _vertexArrayObject_elipsoid_righteye;
        private Shader _shader_elipsoid_righteye;

        //body
        protected List<Vector3> _elipsoid_vertices_body = new List<Vector3>();
        //private float[] _elipsoid_vertices = new float[1900 * 3];
        private int _elipsoid_index_body = 0;
        private int _vertexBufferObject_elipsoid_body;
        private int _vertexArrayObject_elipsoid_body;
        private Shader _shader_elipsoid_body;

        //left foot
        protected List<Vector3> _box_vertices_leftfoot = new List<Vector3>();
        protected List<uint> vertexIndices_box_leftfoot = new List<uint>();
        // private float[] _box_vertices = new float[24]; //8 titik, masing-masing 3 koordinat
        private int _vertexBufferObject_box_leftfoot;
        private int _vertexArrayObject_box_leftfoot;
        private Shader _shader_box_leftfoot;
        private int _elementBufferObject_box_leftfoot;

        //right foot
        protected List<Vector3> _box_vertices_rightfoot = new List<Vector3>();
        protected List<uint> vertexIndices_box_rightfoot = new List<uint>();
        // private float[] _box_vertices = new float[24]; //8 titik, masing-masing 3 koordinat
        private int _vertexBufferObject_box_rightfoot;
        private int _vertexArrayObject_box_rightfoot;
        private Shader _shader_box_rightfoot;
        private int _elementBufferObject_box_rightfoot;

        //left hand
        protected List<Vector3> _cylinder_vertices_lefthand = new List<Vector3>();
        protected List<uint> vertexIndices_cylinder_lefthand = new List<uint>();
        private int _cylinder_index_lefthand = 0;
        private int _vertexBufferObject_cylinder_lefthand;
        private int _vertexArrayObject_cylinder_lefthand;
        private Shader _shader_cylinder_lefthand;
        private int _elementBufferObject_cylinder_lefthand;

        //right hand
        protected List<Vector3> _cylinder_vertices_righthand = new List<Vector3>();
        protected List<uint> vertexIndices_cylinder_righthand = new List<uint>();
        private int _cylinder_index_righthand = 0;
        private int _vertexBufferObject_cylinder_righthand;
        private int _vertexArrayObject_cylinder_righthand;
        private Shader _shader_cylinder_righthand;
        private int _elementBufferObject_cylinder_righthand;

        //transformasi
        private Matrix4 transform;

        public Robot()
        {
        }

        Vector3 setBezier(float t, float _positionX, float _positionY, float _height, float _extended)
        {
            //Console.WriteLine(t);
            Vector3 p = new Vector3(0f, 0f, 0f);
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

        private void createBoxVertices_right()
        {
            float _positionX = -0.05f;
            float _positionY = 0.75f;
            float _positionZ = 0.0f;

            float _boxLength = 0.03f;
            float _boxHeight = 0.25f;

            Vector3 temp_vector;

            //1. Inisialisasi vertex
            //Titik 1
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //Titik 2
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //Titik 3
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //Titik 4
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //Titik 5
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //Titik 6
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //Titik 7
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //Titik 8
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices_right.Add(temp_vector);

            //2. Inisialisasi index vertex
            vertexIndices_box_right = new List<uint>
            {
                // Segitiga Depan 1
                0, 1, 2,
                // Segitiga Depan 2
                1, 2, 3,
                // Segitiga Atas 1
                0, 4, 5,
                // Segitiga Atas 2
                0, 1, 5,
                // Segitiga Kanan 1
                1, 3, 5,
                // Segitiga Kanan 2
                3, 5, 7,
                // Segitiga Kiri 1
                0, 2, 4,
                // Segitiga Kiri 2
                2, 4, 6,
                // Segitiga Belakang 1
                4, 5, 6,
                // Segitiga Belakang 2
                5, 6, 7,
                // Segitiga Bawah 1
                2, 3, 6,
                // Segitiga Bawah 2
                3, 6, 7,
                3, 7, 5,
                4, 0, 2,
                2, 6, 7
            };
        }

        private void createBoxVertices_left()
        {
            float _positionX = 0.25f;
            float _positionY = 0.75f;
            float _positionZ = 0.0f;

            float _boxLength = 0.03f;
            float _boxHeight = 0.25f;

            Vector3 temp_vector;

            //1. Inisialisasi vertex
            //Titik 1
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //Titik 2
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //Titik 3
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //Titik 4
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //Titik 5
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //Titik 6
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //Titik 7
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //Titik 8
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; //z

            _box_vertices.Add(temp_vector);

            //2. Inisialisasi index vertex
            vertexIndices_box = new List<uint>
            {
                // Segitiga Depan 1
                0, 1, 2,
                // Segitiga Depan 2
                1, 2, 3,
                // Segitiga Atas 1
                0, 4, 5,
                // Segitiga Atas 2
                0, 1, 5,
                // Segitiga Kanan 1
                1, 3, 5,
                // Segitiga Kanan 2
                3, 5, 7,
                // Segitiga Kiri 1
                0, 2, 4,
                // Segitiga Kiri 2
                2, 4, 6,
                // Segitiga Belakang 1
                4, 5, 6,
                // Segitiga Belakang 2
                5, 6, 7,
                // Segitiga Bawah 1
                2, 3, 6,
                // Segitiga Bawah 2
                3, 6, 7,
                3, 7, 5,
                4, 0, 2,
                2, 6, 7
            };
        }

        private void createElipsoidVertices()
        {
            float _positionX = 0.1f;
            float _positionY = 0.4f;
            float _positionZ = 0.0f;

            float _radius = 0.3f;

            float _pi = 3.1416f;

            Vector3 tempvector_elipsoid;

            for (float v = -_pi / 2; v <= _pi / 2; v += 0.001f)
            {
                for (float u = -_pi; u <= _pi; u += 0.001f)
                {
                    tempvector_elipsoid.X = _positionX + _radius * (float)Math.Cos(v) * (float)Math.Cos(u); //x
                    tempvector_elipsoid.Y = _positionY + _radius * (float)Math.Cos(v) * (float)Math.Sin(u); //y
                    tempvector_elipsoid.Z = _positionZ + _radius * (float)Math.Sin(v); //z
                    _elipsoid_vertices.Add(tempvector_elipsoid);
                }
                //Console.WriteLine(_elipsoid_vertices.Count);
            }
        }

        private void createElipsoidVertices_face()
        {
            float _positionX = -0.0575f;
            float _positionY = 0.4f;
            float _positionZ = -0.2f;

            float _radius = 0.15f;

            float _pi = 3.1416f;

            Vector3 tempvector_elipsoid;

            for (float v = -_pi / 2; v <= 0; v += 0.001f)
            {
                for (float u = -_pi; u <= _pi; u += 0.001f)
                {
                    tempvector_elipsoid.X = _positionX + _radius + 0.2f * (float)Math.Cos(v) * (float)Math.Cos(u); //x
                    tempvector_elipsoid.Y = _positionY + _radius - 0.075f * (float)Math.Cos(v) * (float)Math.Sin(u); //y
                    tempvector_elipsoid.Z = _positionZ + _radius * (float)Math.Sin(v); //z
                    _elipsoid_vertices_face.Add(tempvector_elipsoid);
                }
                //Console.WriteLine(_elipsoid_vertices_face.Count);
            }
            //transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(500.0f));
            //transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(450.0f));
        }

        private void createElipsoidVertices_lefteye()
        {
            float _positionX = 0.25f;
            float _positionY = 0.9f;
            float _positionZ = 0.01f;

            float _radius = 0.05f;

            float _pi = 3.1416f;

            Vector3 tempvector_elipsoid;

            for (float v = -_pi / 2; v <= 0; v += 0.001f)
            {
                for (float u = -_pi; u <= _pi; u += 0.001f)
                {
                    tempvector_elipsoid.X = _positionX + _radius * (float)Math.Cos(v) * (float)Math.Cos(u); //x
                    tempvector_elipsoid.Y = _positionY + _radius * (float)Math.Cos(v) * (float)Math.Sin(u); //y
                    tempvector_elipsoid.Z = _positionZ + _radius * (float)Math.Sin(v); //z
                    _elipsoid_vertices_lefteye.Add(tempvector_elipsoid);
                }
                //Console.WriteLine(_elipsoid_vertices_lefteye.Count);
            }
        }

        private void createElipsoidVertices_righteye()
        {
            float _positionX = -0.05f;
            float _positionY = 0.9f;
            float _positionZ = 0.01f;

            float _radius = 0.05f;

            float _pi = 3.1416f;

            Vector3 tempvector_elipsoid;

            for (float v = -_pi / 2; v <= 0; v += 0.001f)
            {
                for (float u = -_pi; u <= _pi; u += 0.001f)
                {
                    tempvector_elipsoid.X = _positionX + _radius * (float)Math.Cos(v) * (float)Math.Cos(u); //x
                    tempvector_elipsoid.Y = _positionY + _radius * (float)Math.Cos(v) * (float)Math.Sin(u); //y
                    tempvector_elipsoid.Z = _positionZ + _radius * (float)Math.Sin(v); //z
                    _elipsoid_vertices_righteye.Add(tempvector_elipsoid);
                }
                //Console.WriteLine(_elipsoid_vertices_righteye.Count);
            }
        }

        private void createElipsoidVertices_body()
        {
            float _positionX = 0.1f;
            float _positionY = -0.9f;
            float _positionZ = -0.0f;

            float _radius = 0.5f;
            float _radius_x = 0.3f;

            float _pi = 3.1416f;

            Vector3 tempvector_elipsoid;

            for (float v = 0; v <= _pi / 2; v += _pi / 180000 * 2)
            {
                for (float u = -_pi; u <= _pi; u += _pi / 30)
                {
                    tempvector_elipsoid.X = _positionX + _radius_x * (float)Math.Cos(v) * (float)Math.Cos(u); //x
                    tempvector_elipsoid.Y = _positionY + _radius * 2.0f * (float)Math.Sin(v); //y
                    //tempvector_elipsoid.Z = _positionZ + _radius * (float)Math.Sin(v); //z
                    tempvector_elipsoid.Z = _positionZ + _radius * (float)Math.Cos(v) * (float)Math.Sin(u); //z
                    _elipsoid_vertices_body.Add(tempvector_elipsoid);
                }
                //Console.WriteLine("v : " + v);
            }
            //transform = transform * Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
            //Matrix4.CreateRotationX(MathHelper.DegreesToRadians(450.0f)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ);
            //transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(450.0f));
            //transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(450.0f));
        }

        private void createHyperboloidVertices()
        {
            float _positionX = -0.5f;
            float _positionY = 0.0f;
            float _positionZ = 0.0f;

            float _radiusX = 0.1f;
            float _radiusY = 0.1f;
            float _radiusZ = 0.1f;

            float _pi = 3.1426f;

            for (float u = -_pi; u <= _pi; u += _pi / 30)
            {
                for (float v = -_pi / 2; v < _pi / 2; v += _pi / 30)
                {
                    _hyperboloid_vertices[_hyperboloid_index * 3] = _positionX + (1.0f / (float)Math.Cos(v)) * (float)Math.Cos(u) * _radiusX;
                    _hyperboloid_vertices[_hyperboloid_index * 3 + 1] = _positionY + (1.0f / (float)Math.Cos(v)) * (float)Math.Sin(u) * _radiusY;
                    _hyperboloid_vertices[_hyperboloid_index * 3 + 2] = _positionZ + (float)Math.Tan(v) * _radiusZ;
                    _hyperboloid_index++;
                }
            }
        }

        private void createBoxVertices_leftfoot()
        {
            float _positionX = -0.1f;
            float _positionY = -0.915f;
            float _positionZ = -0.2f;

            float _boxLength = 0.1f;
            float _boxHeight = 0.03f;
            float _boxWidht = 0.55f;

            Vector3 temp_vector;

            //1. Inisialisasi vertex
            //Titik 1
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //Titik 2
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //Titik 3
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //Titik 4
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //Titik 5
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //Titik 6
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //Titik 7
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //Titik 8
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_leftfoot.Add(temp_vector);

            //2. Inisialisasi index vertex
            vertexIndices_box_leftfoot = new List<uint>
            {
                // Segitiga Depan 1
                0, 1, 2,
                // Segitiga Depan 2
                1, 2, 3,
                // Segitiga Atas 1
                0, 4, 5,
                // Segitiga Atas 2
                0, 1, 5,
                // Segitiga Kanan 1
                1, 3, 5,
                // Segitiga Kanan 2
                3, 5, 7,
                // Segitiga Kiri 1
                0, 2, 4,
                // Segitiga Kiri 2
                2, 4, 6,
                // Segitiga Belakang 1
                4, 5, 6,
                // Segitiga Belakang 2
                5, 6, 7,
                // Segitiga Bawah 1
                2, 3, 6,
                // Segitiga Bawah 2
                3, 6, 7,
                3, 7, 5,
                4, 0, 2,
                2, 6, 7
            };
        }

        private void createBoxVertices_rightfoot()
        {
            float _positionX = 0.3f;
            float _positionY = -0.915f;
            float _positionZ = -0.2f;

            float _boxLength = 0.1f;
            float _boxHeight = 0.03f;
            float _boxWidht = 0.55f;

            Vector3 temp_vector;

            //1. Inisialisasi vertex
            //Titik 1
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //Titik 2
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //Titik 3
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //Titik 4
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ - _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //Titik 5
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //Titik 6
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY + _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //Titik 7
            temp_vector.X = _positionX - _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //Titik 8
            temp_vector.X = _positionX + _boxLength / 2.0f; //x
            temp_vector.Y = _positionY - _boxHeight / 2.0f; //y
            temp_vector.Z = _positionZ + _boxWidht / 2.0f; //z

            _box_vertices_rightfoot.Add(temp_vector);

            //2. Inisialisasi index vertex
            vertexIndices_box_rightfoot = new List<uint>
            {
                // Segitiga Depan 1
                0, 1, 2,
                // Segitiga Depan 2
                1, 2, 3,
                // Segitiga Atas 1
                0, 4, 5,
                // Segitiga Atas 2
                0, 1, 5,
                // Segitiga Kanan 1
                1, 3, 5,
                // Segitiga Kanan 2
                3, 5, 7,
                // Segitiga Kiri 1
                0, 2, 4,
                // Segitiga Kiri 2
                2, 4, 6,
                // Segitiga Belakang 1
                4, 5, 6,
                // Segitiga Belakang 2
                5, 6, 7,
                // Segitiga Bawah 1
                2, 3, 6,
                // Segitiga Bawah 2
                3, 6, 7,
                3, 7, 5,
                4, 0, 2,
                2, 6, 7
            };
        }

        private void createCylinderVertices_lefthand()
        {
            float _positionX = -0.5f;
            float _positionY = -0.15f;
            float _positionZ = -0.01f;

            float _radius = 0.1f;
            float _height = 0.8f;
            float _extended = 0.6f;

            Vector3 temp_vector;
            float _pi = (float)Math.PI;

            for (float v = -_height / 2; v <= (_height / 2); v += 0.0001f)
            {
                Vector3 p = setBezier(((v + (_height / 2)) / _height), _positionX, _positionY, _height, _extended);
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {

                    temp_vector.X = p.X + _radius * (float)Math.Cos(u);
                    temp_vector.Y = p.Y + _radius * (float)Math.Sin(u);
                    temp_vector.Z = _positionZ + v;

                    _cylinder_vertices_lefthand.Add(temp_vector);
                }
            }
        }



        private void createCylinderVertices_righthand()
        {
            float _positionX = 0.7f;
            float _positionY = -0.15f;
            float _positionZ = -0.01f;

            float _radius = 0.1f;
            float _height = 0.8f;
            float _extended = -0.6f;

            Vector3 temp_vector;
            float _pi = (float)Math.PI;

            for (float v = -_height / 2; v <= (_height / 2); v += 0.0001f)
            {
                Vector3 p = setBezier(((v + (_height / 2)) / _height), _positionX, _positionY, _height, _extended);
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {

                    temp_vector.X = p.X + _radius * (float)Math.Cos(u);
                    temp_vector.Y = p.Y + _radius * (float)Math.Sin(u);
                    temp_vector.Z = _positionZ + v;

                    _cylinder_vertices_righthand.Add(temp_vector);

                }
            }
        }

        public void Load()
        {
            transform = Matrix4.Identity;
            createBoxVertices_left();
            createBoxVertices_right();
            createElipsoidVertices();
            //createHyperboloidVertices();
            createElipsoidVertices_face();
            createElipsoidVertices_lefteye();
            createElipsoidVertices_righteye();
            createElipsoidVertices_body();
            createBoxVertices_leftfoot();
            createBoxVertices_rightfoot();
            createCylinderVertices_lefthand();
            createCylinderVertices_righthand();

            //box left
            //VBO
            _vertexBufferObject_box = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_box);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _box_vertices.Count * Vector3.SizeInBytes, _box_vertices.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject_box = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_box);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_box = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_antena.frag");
            _shader_box.Use();

            //EBO
            _elementBufferObject_box = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_box);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices_box.Count * sizeof(uint), vertexIndices_box.ToArray(), BufferUsageHint.StaticDraw);

            //box right
            //VBO
            _vertexBufferObject_box_right = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_box_right);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _box_vertices_right.Count * Vector3.SizeInBytes, _box_vertices_right.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject_box_right = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_box_right);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_box_right = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_antena.frag");
            _shader_box_right.Use();

            //EBO
            _elementBufferObject_box_right = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_box_right);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices_box_right.Count * sizeof(uint), vertexIndices_box_right.ToArray(), BufferUsageHint.StaticDraw);

            //elipsoid
            //inisialisasi buffer
            _vertexBufferObject_elipsoid = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_elipsoid);
            //parameter 2 yang kita panggil vertices.Count == array.length
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _elipsoid_vertices.Count * Vector3.SizeInBytes, _elipsoid_vertices.ToArray(), BufferUsageHint.StaticDraw);

            //Inisialisasi array
            _vertexArrayObject_elipsoid = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_elipsoid);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_elipsoid = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_head.frag");
            _shader_elipsoid.Use();

            //face
            //inisialisasi buffer
            _vertexBufferObject_elipsoid_face = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_elipsoid_face);
            //parameter 2 yang kita panggil vertices.Count == array.length
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _elipsoid_vertices_face.Count * Vector3.SizeInBytes, _elipsoid_vertices_face.ToArray(), BufferUsageHint.StaticDraw);

            //Inisialisasi array
            _vertexArrayObject_elipsoid_face = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_elipsoid_face);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_elipsoid_face = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader.frag");
            _shader_elipsoid_face.Use();

            //left eye
            //inisialisasi buffer
            _vertexBufferObject_elipsoid_lefteye = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_elipsoid_lefteye);
            //parameter 2 yang kita panggil vertices.Count == array.length
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _elipsoid_vertices_lefteye.Count * Vector3.SizeInBytes, _elipsoid_vertices_lefteye.ToArray(), BufferUsageHint.StaticDraw);

            //Inisialisasi array
            _vertexArrayObject_elipsoid_lefteye = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_elipsoid_lefteye);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_elipsoid_lefteye = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_eye.frag");
            _shader_elipsoid_lefteye.Use();

            //right eye
            //inisialisasi buffer
            _vertexBufferObject_elipsoid_righteye = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_elipsoid_righteye);
            //parameter 2 yang kita panggil vertices.Count == array.length
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _elipsoid_vertices_righteye.Count * Vector3.SizeInBytes, _elipsoid_vertices_righteye.ToArray(), BufferUsageHint.StaticDraw);

            //Inisialisasi array
            _vertexArrayObject_elipsoid_righteye = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_elipsoid_righteye);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_elipsoid_righteye = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_eye.frag");
            _shader_elipsoid_righteye.Use();

            //body
            //inisialisasi buffer
            _vertexBufferObject_elipsoid_body = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_elipsoid_body);
            //parameter 2 yang kita panggil vertices.Count == array.length
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _elipsoid_vertices_body.Count * Vector3.SizeInBytes, _elipsoid_vertices_body.ToArray(), BufferUsageHint.StaticDraw);

            //Inisialisasi array
            _vertexArrayObject_elipsoid_body = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_elipsoid_body);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_elipsoid_body = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_body.frag");
            _shader_elipsoid_body.Use();

            //Hyperboloid
            //VBO
            _vertexBufferObject_hyperboloid = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_hyperboloid);
            GL.BufferData(BufferTarget.ArrayBuffer, _hyperboloid_vertices.Length * sizeof(float), _hyperboloid_vertices, BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject_hyperboloid = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_hyperboloid);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_hyperboloid = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader.frag");
            _shader_hyperboloid.Use();

            //box leftfoot
            //VBO
            _vertexBufferObject_box_leftfoot = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_box_leftfoot);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _box_vertices_leftfoot.Count * Vector3.SizeInBytes, _box_vertices_leftfoot.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject_box_leftfoot = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_box_leftfoot);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_box_leftfoot = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_foot.frag");
            _shader_box_leftfoot.Use();

            //EBO
            _elementBufferObject_box_leftfoot = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_box_leftfoot);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices_box_leftfoot.Count * sizeof(uint), vertexIndices_box_leftfoot.ToArray(), BufferUsageHint.StaticDraw);

            //box rightbox
            //VBO
            _vertexBufferObject_box_rightfoot = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_box_rightfoot);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _box_vertices_rightfoot.Count * Vector3.SizeInBytes, _box_vertices_rightfoot.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject_box_rightfoot = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_box_rightfoot);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_box_rightfoot = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_foot.frag");
            _shader_box_rightfoot.Use();

            //EBO
            _elementBufferObject_box_rightfoot = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_box_rightfoot);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices_box_rightfoot.Count * sizeof(uint), vertexIndices_box_rightfoot.ToArray(), BufferUsageHint.StaticDraw);

            //left hand
            //VBO
            _vertexBufferObject_cylinder_lefthand = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_cylinder_lefthand);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _cylinder_vertices_lefthand.Count * Vector3.SizeInBytes, _cylinder_vertices_lefthand.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject_cylinder_lefthand = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_cylinder_lefthand);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_cylinder_lefthand = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_hand.frag");
            _shader_cylinder_lefthand.Use();

            //EBO
            _elementBufferObject_cylinder_lefthand = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_cylinder_lefthand);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices_cylinder_lefthand.Count * sizeof(uint), vertexIndices_cylinder_lefthand.ToArray(), BufferUsageHint.StaticDraw);

            //right hand
            //VBO
            _vertexBufferObject_cylinder_righthand = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_cylinder_righthand);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, _cylinder_vertices_righthand.Count * Vector3.SizeInBytes, _cylinder_vertices_righthand.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject_cylinder_righthand = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject_cylinder_righthand);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //shader
            _shader_cylinder_righthand = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_transform.vert", "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_hand.frag");
            _shader_cylinder_righthand.Use();

            //EBO
            _elementBufferObject_cylinder_righthand = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_cylinder_righthand);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices_cylinder_righthand.Count * sizeof(uint), vertexIndices_cylinder_righthand.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void Render(Camera _camera)
        {
            //transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(5.0f));
            //transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(5.0f));


            //box left
            _shader_box.Use();
            _shader_box.SetMatrix4("transform", transform);
            _shader_box.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_box.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_box);
            GL.DrawElements(PrimitiveType.TriangleFan, vertexIndices_box.Count, DrawElementsType.UnsignedInt, 0);

            //box right
            _shader_box_right.Use();
            _shader_box_right.SetMatrix4("transform", transform);
            _shader_box_right.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_box_right.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_box_right);
            GL.DrawElements(PrimitiveType.TriangleFan, vertexIndices_box_right.Count, DrawElementsType.UnsignedInt, 0);

            //elipsoid
            _shader_elipsoid.Use();
            _shader_elipsoid.SetMatrix4("transform", transform);
            _shader_elipsoid.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_elipsoid.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_elipsoid);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _elipsoid_vertices.Count);

            //face
            _shader_elipsoid_face.Use();
            _shader_elipsoid_face.SetMatrix4("transform", transform);
            _shader_elipsoid_face.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_elipsoid_face.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_elipsoid_face);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _elipsoid_vertices_face.Count);

            //left eye
            _shader_elipsoid_lefteye.Use();
            _shader_elipsoid_lefteye.SetMatrix4("transform", transform);
            _shader_elipsoid_lefteye.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_elipsoid_lefteye.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_elipsoid_lefteye);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _elipsoid_vertices_lefteye.Count);

            //right eye
            _shader_elipsoid_righteye.Use();
            _shader_elipsoid_righteye.SetMatrix4("transform", transform);
            _shader_elipsoid_righteye.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_elipsoid_righteye.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_elipsoid_righteye);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _elipsoid_vertices_righteye.Count);

            //body
            _shader_elipsoid_body.Use();
            _shader_elipsoid_body.SetMatrix4("transform", transform);
            _shader_elipsoid_body.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_elipsoid_body.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_elipsoid_body);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _elipsoid_vertices_body.Count);

            //left foot
            _shader_box_leftfoot.Use();
            _shader_box_leftfoot.SetMatrix4("transform", transform);
            _shader_box_leftfoot.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_box_leftfoot.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_box_leftfoot);
            GL.DrawElements(PrimitiveType.TriangleFan, vertexIndices_box_leftfoot.Count, DrawElementsType.UnsignedInt, 0);

            //right foot
            _shader_box_rightfoot.Use();
            _shader_box_rightfoot.SetMatrix4("transform", transform);
            _shader_box_rightfoot.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_box_rightfoot.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_box_rightfoot);
            GL.DrawElements(PrimitiveType.TriangleFan, vertexIndices_box_rightfoot.Count, DrawElementsType.UnsignedInt, 0);

            //left hand
            _shader_cylinder_lefthand.Use();
            _shader_cylinder_lefthand.SetMatrix4("transform", transform);
            _shader_cylinder_lefthand.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_cylinder_lefthand.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_cylinder_lefthand);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, _cylinder_vertices_lefthand.Count);

            //right hand
            _shader_cylinder_righthand.Use();
            _shader_cylinder_righthand.SetMatrix4("transform", transform);
            _shader_cylinder_righthand.SetMatrix4("view", _camera.GetViewMatrix());
            _shader_cylinder_righthand.SetMatrix4("projection", _camera.GetProjectionMatrix());
            GL.BindVertexArray(_vertexArrayObject_cylinder_righthand);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, _cylinder_vertices_righthand.Count);
        }
    }
}
