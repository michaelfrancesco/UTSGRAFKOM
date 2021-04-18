using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Tugas_Individu
{
    class Mesh
    {
        protected List<Vector3> vertices = new List<Vector3>();
        protected List<Vector3> textureVertices = new List<Vector3>();
        protected List<Vector3> normals = new List<Vector3>();
        protected List<uint> vertexIndices = new List<uint>();

        protected int _vertexBufferObject;
        protected int _vertexArrayObject;
        protected Shader _shader;
        protected int _elementBufferObject;
        protected Matrix4 transform;

        protected string color;

        protected float _positionX;
        protected float _positionY;
        protected float _positionZ;
        protected float _size = 1;

        protected string name;

        public Mesh(List<Vector3> vertices, List<Vector3> textureVertices, List<Vector3> normals, List<uint> vertexIndices, int vertexBufferObject, int vertexArrayObject, Shader shader, int elementBufferObject, Matrix4 transform)
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

        public Mesh(string color = "red", string name="allen")
        {
            this.color = color;
            this.name = name;
        }

        public float getPosX() { return _positionX; }
        public float getPosY() { return _positionY; }
        public float getPosZ() { return _positionZ; }

        public void setupObject()
        {
            transform = Matrix4.Identity;

            //Console.WriteLine(transform);

            //VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                vertices.Count * Vector3.SizeInBytes,
                vertices.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float,
                false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //Shader
            _shader = new Shader("C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_"+name+".vert",
                "C:/Data UKP/GrafKom/Pertemuan 1/Tugas Individu/Shaders/shader_" + color + ".frag");
            _shader.Use();

            //EBO
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices.Count * sizeof(uint),
                            vertexIndices.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void render()
        {
            //rotate_center(0.01f, 'x');
            _shader.Use();

            _shader.SetMatrix4("transform", transform);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, vertices.Count);
        }

        public Vector3 getPos() { return new Vector3(_positionX, _positionY, _positionZ); }

        public void append(Mesh obj)
        {
            vertices.AddRange(obj.vertices);

            Console.WriteLine(vertices.Count);
        }

        public void rotate(float angle = 0.01f, char a = 'x')
        {
            switch (a)
            {
                case 'x': transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle)); break;
                case 'y': transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle)); break;
                case 'z': transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle)); break;
            }

            //Console.WriteLine(transform);
            //Console.WriteLine(" ");

            Matrix4 data = new Matrix4(
                _positionX, 0, 0, 0,
                0, _positionY, 0, 0,
                0, 0, _positionZ, 0,
                0, 0, 0, 1);

            data = transform * data;

            

            _positionX = data.M11;
            _positionY = data.M22;
            _positionZ = data.M33;
        }

        public void rotate_center(float angle = 0.01f, char a = 'x')
        {
            Vector4 pos = new Vector4(getPos());
            switch (a)
            {
                case 'x':
                    transform = transform * Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
                  Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ);


                    pos = Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
                  Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ) * pos;
                    
                    break;
                case 'y':
                    transform = transform * Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ);
                    
                    pos = Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ) * pos;


                    break;
                case 'z':
                    transform = transform * Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ); 
                    
                    pos = Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ) * pos;


                    break;


            }

            

            

            //_positionX = pos.X;
            //_positionY = pos.Y;
            //_positionZ = pos.Z;
           
    
            

            //Matrix4 data = new Matrix4(
            //   _positionX, 0, 0, 0,
            //   0, _positionY, 0, 0,
            //   0, 0, _positionZ, 0,
            //   0, 0, 0, 1);

            //data = transform * data;

            //_positionX = data.M11;
            //_positionY = data.M22;
            //_positionZ = data.M33;

            //Console.WriteLine(_positionX + " " + _positionY);
        }

        //public void rotate(float angle = 0.01f, char a = 'x')
        //{
        //    Vector3 temp = new Vector3();
        //    switch (a)
        //    {
        //        case 'x': transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle));
        //            temp.X = _positionX;
        //            temp.Y = (float)Math.Cos(angle) * _positionY - (float)Math.Sin(angle) * _positionZ;
        //            temp.Z = (float)Math.Sin(angle) * _positionY + (float)Math.Cos(angle) * _positionZ;

        //            break;
        //        case 'y': transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle));
        //            temp.X = (float)Math.Cos(angle) * _positionX - (float)Math.Sin(angle) * _positionZ;
        //            temp.Y = _positionY;
        //            temp.Z = (float)Math.Sin(angle) * _positionX + (float)Math.Cos(angle) * _positionZ;
        //            break;
        //        case 'z': transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle));
        //            temp.X = (float)Math.Cos(angle) * _positionX - (float)Math.Sin(angle) * _positionY;
        //            temp.Y = (float)Math.Sin(angle) * _positionX + (float)Math.Cos(angle) * _positionY;
        //            temp.Z = _positionZ;
        //            break;
        //    }




        //    _positionX = temp.X;
        //    _positionY = temp.Y;
        //    _positionZ = temp.Z;
        //}

        public void rotate_point(float angle = 0.01f, char a = 'x', Vector3 pos = new Vector3())
        {
            //Console.WriteLine(pos);
            Vector3 temp = new Vector3();
            switch (a)
            {
                case 'x':
                    transform = transform * Matrix4.CreateTranslation(1 * pos.X, 1 * pos.Y, 1 * pos.Z) *
                  Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(-pos.X, -pos.Y, -pos.Z);

                    temp.X = _positionX;
                    temp.Y = (float)Math.Cos(angle) * (pos.Y) - (float)Math.Sin(angle) * (pos.Z) + (_positionY - pos.Y);
                    temp.Z = (float)Math.Sin(angle) * (pos.Y) + (float)Math.Cos(angle) * (pos.Z) + (_positionZ - pos.Z);

                    break;
                case 'y':
                    transform = transform * Matrix4.CreateTranslation(1 * pos.X, 1 * pos.Y, 1 * pos.Z) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(-pos.X, -pos.Y, -pos.Z);


                    temp = pos;

                    temp.X = (float)Math.Cos(angle) * pos.Y - (float)Math.Sin(angle) * pos.Z;
                    temp.Z = (float)Math.Sin(angle) * pos.Y + (float)Math.Cos(angle) * pos.Z;

                    temp.X += (pos.X - _positionX);
                    temp.Y += (pos.Y - _positionY);
                    temp.Z += (pos.Z - _positionZ);

                    //temp.X = (float)Math.Cos(angle) * (   pos.X) - (float)Math.Sin(angle) * ( pos.Z) + (_positionX - pos.X);
                    //temp.Y = _positionY;
                    //temp.Z = (float)Math.Sin(angle) * (   pos.X) + (float)Math.Cos(angle) * ( pos.Z) + (_positionZ - pos.Z);

                    break;
                case 'z':
                    transform = transform * Matrix4.CreateTranslation(1 * pos.X, 1 * pos.Y, 1 * pos.Z) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(-pos.X, -pos.Y, -pos.Z);

                    temp.X = (float)Math.Cos(angle) * (pos.X) - (float)Math.Sin(angle) * (pos.Y) + (_positionX - pos.X);
                    temp.Y = (float)Math.Sin(angle) * (pos.X) + (float)Math.Cos(angle) * (pos.Y) + (_positionY - pos.Y);
                    temp.Z = _positionZ;


                    break;

            }

            //_positionX = temp.X;
            //_positionY = temp.Y;
            //_positionZ = temp.Z;


            //Console.WriteLine(temp);



            //temp.X = _positionX * transform.M11 + _positionY * transform.M12 + _positionZ * transform.M13 + 1 * transform.M14;
            //temp.Y = _positionX * transform.M21 + _positionY * transform.M22 + _positionZ * transform.M23 + 1 * transform.M24;
            //temp.Z = _positionX * transform.M31 + _positionY * transform.M32 + _positionZ * transform.M33 + 1 * transform.M34;


            //_positionX = temp.X;
            //_positionY = temp.Y;
            //_positionZ = temp.Z;

            //Console.WriteLine(_positionX + " " + _positionY);
        }

        //public void rotate_center(float angle = 0.01f, char a = 'x')
        //{

        //    switch (a)
        //    {
        //        case 'x':
        //            transform = transform * Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
        //          Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ); break;
        //        case 'y':
        //            transform = transform * Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
        //    Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ); break;
        //        case 'z':
        //            transform = transform * Matrix4.CreateTranslation(-1 * _positionX, -1 * _positionY, -1 * _positionZ) *
        //    Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle)) * Matrix4.CreateTranslation(_positionX, _positionY, _positionZ); break;

        //    }

        //    //Vector3 temp;

        //    //temp.X = _positionX * transform.M11 + _positionY * transform.M12 + _positionZ * transform.M13 + 1 * transform.M14;
        //    //temp.Y = _positionX * transform.M21 + _positionY * transform.M22 + _positionZ * transform.M23 + 1 * transform.M24;
        //    //temp.Z = _positionX * transform.M31 + _positionY * transform.M32 + _positionZ * transform.M33 + 1 * transform.M34;


        //    //_positionX = temp.X;
        //    //_positionY = temp.Y;
        //    //_positionZ = temp.Z;

        //    //Console.WriteLine(_positionX + " " + _positionY);
        //}

        public void use_camera(Camera _camera)
        {
            
            _shader.Use();
            
            _shader.SetMatrix4("transform", transform);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        }


        public void scale(float m = 2)
        {
            if (m == 1)
            {
                transform = transform * Matrix4.CreateScale(1);
                return;
            }
            transform = transform * Matrix4.CreateScale(_size+ m*_size);
            _positionX *= (_size + m * _size);
            _positionY *= (_size + m * _size);
            _positionZ *= (_size + m * _size);
        }

        public void translate(float _x, float _y, float _z)
        {
            transform = transform * Matrix4.CreateTranslation(_x, _y, _z);
        }

        public void translate(Vector3 pos)
        {
            transform = transform * Matrix4.CreateTranslation(pos.X - _positionX, pos.Y - _positionY, pos.Z - _positionZ);
        }

        public void translate(float length, char x)
        {
            Vector3 trans = new Vector3();
            switch (x)
            {
                case 'a':
                    trans.X =  - length; 
                    trans.Y = 0;
                    trans.Z = 0; break;
                case 'w':
                    trans.X = 0;
                    trans.Y = 0;
                    trans.Z =  - length; break;
                case 's':
                    trans.X = 0;
                    trans.Y = 0;
                    trans.Z = length; break;
                case 'd':
                    trans.X = length;
                    trans.Y = 0;
                    trans.Z = 0; break;

                case 'x':
                    trans.X =  length;
                    trans.Y = 0;
                    trans.Z = 0; break;

                case 'y':
                    trans.X = 0;
                    trans.Y = length;
                    trans.Z = 0; break;

                case 'z':
                    trans.X = 0;
                    trans.Y = 0;
                    trans.Z = length; break;

            }
            
            transform = transform * Matrix4.CreateTranslation(trans);





            //Console.WriteLine(data);

            _positionX += trans.X;
            _positionY += trans.Y;
            _positionZ += trans.Z;
        }

        public void LoadObjFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
            }

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
                    words.RemoveAll(s => s == string.Empty);

                    if (words.Count == 0)
                        continue;

                    //System.Console.WriteLine("New While");
                    //foreach (string x in words)
                    //               {
                    //	System.Console.WriteLine("tes");
                    //	System.Console.WriteLine(x);
                    //               }

                    string type = words[0];
                    words.RemoveAt(0);

                    switch (type)
                    {
                        // vertex
                        case "v":
                            vertices.Add(new Vector3(float.Parse(words[0]) / 10, float.Parse(words[1]) / 10, float.Parse(words[2]) / 10));
                            break;

                        case "vt":
                            textureVertices.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]),
                                                            words.Count < 3 ? 0 : float.Parse(words[2])));
                            break;

                        case "vn":
                            normals.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2])));
                            break;
                        // face
                        case "f":
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                    continue;

                                string[] comps = w.Split('/');

                                vertexIndices.Add(uint.Parse(comps[0]) - 1);

                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }



    }

}
