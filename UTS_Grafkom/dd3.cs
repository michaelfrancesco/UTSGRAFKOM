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
    class DD3
    {
        float _positionX;
        float _positionY;

        static string name = "mike";

        // Initial
        Circle eye = new Circle("darkgray", name);
        head head = new head("white", name);
        Cylinder accent3 = new Cylinder("orange", name);
        Circle neckeye = new Circle("darkgray", name);
        Circle neck = new Circle("gray", name);
        Cylinder accent1 = new Cylinder("orange", name);
        Cylinder accent2 = new Cylinder("orange", name);
        Cylinder body = new Cylinder("white", name);
        Box arm_left = new Box("gray", name);
        Box arm_right = new Box("gray", name);
        Cylinder body_bottom = new Cylinder("gray", name);
        Circle ball = new Circle("darkgray", name);
        Cylinder_Curve curved = new Cylinder_Curve("gray",name);

        int animate_number=0;
        float angle;
        float counter = 0;

        public DD3(float _positionX = -3f, float _positionY = 0.3f)
        {
            this._positionX = _positionX;
            this._positionY = _positionY;
        }

        protected void setup()
        {
            eye.setupObject();
            neckeye.setupObject();
            head.setupObject();
            accent3.setupObject();
            neck.setupObject();
            accent1.setupObject();
            accent2.setupObject();
            body.setupObject();
            arm_left.setupObject();
            arm_right.setupObject();
            body_bottom.setupObject();
            ball.setupObject();
            curved.setupObject();
        }

        public void load() {

            body.createEllipsoidVertices(_positionX, _positionY, 0.0f, 0.35f, 0.5f);
            body_bottom.createEllipsoidVertices(body.getPosX() + 0.0f, body.getPosY() - body.getHeight() / 2 - 0.01f, body.getPosZ() + 0.0f, 0.31f, 0.05f);
            arm_left.createBoxvertices(body.getPosX() - 0.38f, body.getPosY() - 0.0f, body.getPosZ() - 0.0f, 0.04f, 0.2f, 0.23f);
            arm_right.createBoxvertices(body.getPosX() + 0.38f, body.getPosY() - 0.0f, body.getPosZ() - 0.0f, 0.04f, 0.2f, 0.23f);

            eye.createEllipsoidVertices(body.getPosX() + 0.0f, body.getPosY() + 0.48f, body.getPosZ() - 0.32f, 0.09f);
            neckeye.createEllipsoidVertices(body.getPosX() + 0.0f, body.getPosY() + 0.25f, body.getPosZ() - 0.19f, 0.08f);
            head.createHalfEllipsoidVetices(body.getPosX() + 0.0f, body.getPosY() + 0.13f, body.getPosZ() + 0.0f, 0.55f);
            accent3.createEllipsoidVertices(head.getPosX() + 0.0f, head.getPosY() + 0.2f, head.getPosZ() + 0.0f, 0.517f, 0.03f);
            neck.createEllipsoidVertices(body.getPosX() + 0.0f, body.getPosY() + 0.08f, body.getPosZ() + 0.0f, 0.3f);
            accent1.createEllipsoidVertices(body.getPosX() + 0.0f, body.getPosY() + 0.2f, body.getPosZ() + 0.0f, 0.355f, 0.04f);
            accent2.createEllipsoidVertices(body.getPosX() + 0.0f, body.getPosY() - 0.2f, body.getPosZ() + 0.0f, 0.355f, 0.04f);
            ball.createEllipsoidVertices(body.getPosX() + 0.0f, body.getPosY() - 0.15f, body.getPosZ() + 0.0f, 0.3f);
            curved.createEllipsoidVertices(body.getPosX() - 0.02f, body.getPosY() + 0.5f, body.getPosZ() + 0.0f, 0.3f, 0.02f, 0.1f);


            setup();
            head.rotate_center(90, 'x');
            body.rotate_center(90, 'x');
            body_bottom.rotate_center(90, 'x');
            arm_left.rotate_center(140, 'z');
            arm_right.rotate_center(-140, 'z');
            accent1.rotate_center(90, 'x');
            accent3.rotate_center(90, 'x');
            accent2.rotate_center(90, 'x');
            curved.rotate_center(90, 'z');
            

            //translate(0f, 'y');
            //rotate(90, 'x');
            //rotate(-90, 'x');
            //rotate(90, 'y');
            //rotate(-90, 'y');
            //rotate(90, 'z');
            //rotate(-90, 'z');
            translate(-0f - (ball.getPosY() - ball.getRadius()), 'y');

            Console.WriteLine(body.getPosX());
            //rotate_point(90f, 'y', new Vector3(body.getPosX(), body.getPosY(), body.getPosZ()));
        }

        public void animate()
        {
            switch (animate_number)
            {
                case 0:
                    translate(0.5f, 'z');
                    counter += 0.5f;
                    if(counter >= 3)
                    {
                        animate_number++;
                        counter = 0;
                    }

                    break;
                case 1:
                    head.translate(-0.1f, 'y');
                    accent3.translate(-0.1f, 'y');
                    eye.translate(-0.1f, 'y');
                    neck.translate(-0.1f, 'y');
                    curved.translate(-0.1f, 'y');
                    counter += 0.1f;
                    if(counter <= neck.getRadius())
                    {
                        animate_number++;
                        counter = 0;
                        Console.WriteLine("masuk");
                    }
                    break;
                case 2:
                    arm_left.rotate_center(-5f, 'z');
                    arm_right.rotate_center(5f, 'z');
                    counter += 5;
                    if(counter >= 140)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;
                case 3:
                    arm_left.translate(0.01f, 'x');
                    arm_right.translate(-0.01f, 'x');
                    counter += 0.01f;
                    if(counter >= 0.05f)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 4:
                    counter++;
                    if(counter >= 5)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 5:
                    head.translate(0.1f, 'y');
                    accent3.translate(0.1f, 'y');
                    eye.translate(0.1f, 'y');
                    neck.translate(0.1f, 'y');
                    curved.translate(0.1f, 'y');
                    counter += 0.1f;
                    if (counter <= neck.getRadius())
                    {
                        animate_number++;
                        counter = 0;
                        Console.WriteLine("masuk");
                    }
                    break;

                case 6:
                    arm_left.translate(-0.01f, 'x');
                    arm_right.translate(0.01f, 'x');
                    counter += 0.01f;
                    if (counter >= 0.05f)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 7:
                    arm_left.rotate_center(5f, 'z');
                    arm_right.rotate_center(-5f, 'z');
                    counter += 5;
                    if (counter >= 140)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 8:
                    translate(-0.5f, 'z');
                    counter += 0.5f;
                    if (counter >= 3)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 9:
                    rotate_center(-3f, 'y');
                    angle += 3.0f;
                    if(angle >= 360)
                    {
                        angle = 0;
                        animate_number++;
                    }
                    break;

                case 10:
                    head.translate(-0.1f, 'y');
                    accent3.translate(-0.1f, 'y');
                    eye.translate(-0.1f, 'y');
                    neck.translate(-0.1f, 'y');
                    curved.translate(-0.1f, 'y');
                    counter += 0.1f;
                    if (counter <= neck.getRadius())
                    {
                        animate_number++;
                        counter = 0;
                        Console.WriteLine("masuk");
                    }
                    break;
                case 11:
                    arm_left.rotate_center(-5f, 'z');
                    arm_right.rotate_center(5f, 'z');
                    counter += 5;
                    if (counter >= 140)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;
                case 12:
                    arm_left.translate(0.01f, 'x');
                    arm_right.translate(-0.01f, 'x');
                    counter += 0.01f;
                    if (counter >= 0.05f)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 13:
                    counter++;
                    if (counter >= 5)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 14:
                    head.translate(0.1f, 'y');
                    accent3.translate(0.1f, 'y');
                    eye.translate(0.1f, 'y');
                    neck.translate(0.1f, 'y');
                    curved.translate(0.1f, 'y');
                    counter += 0.1f;
                    if (counter <= neck.getRadius())
                    {
                        animate_number++;
                        counter = 0;
                        Console.WriteLine("masuk");
                    }
                    break;

                case 15:
                    arm_left.translate(-0.01f, 'x');
                    arm_right.translate(0.01f, 'x');
                    counter += 0.01f;
                    if (counter >= 0.05f)
                    {
                        animate_number++;
                        counter = 0;
                    }
                    break;

                case 16:
                    arm_left.rotate_center(5f, 'z');
                    arm_right.rotate_center(-5f, 'z');
                    counter += 5;
                    if (counter >= 140)
                    {
                        animate_number = 0;
                        counter = 0;
                    }
                    break;
            }
        }

        public void rotate(float angle, char x)
        {
            eye.rotate(angle, x);
            neckeye.rotate(angle, x);
            head.rotate(angle, x);
            accent3.rotate(angle, x);
            neck.rotate(angle, x);
            accent1.rotate(angle, x);
            accent2.rotate(angle, x);
            body.rotate(angle, x);
            body_bottom.rotate(angle, x);
            arm_left.rotate(angle, x);
            arm_right.rotate(angle, x);
            ball.rotate(angle, x);
            curved.rotate(angle, x);
        }

        public void scale(float m)
        {
            eye.scale(m);
            neckeye.scale(m);
            head.scale(m);
            accent3.scale(m);
            neck.scale(m);
            accent1.scale(m);
            accent2.scale(m);
            body.scale(m);
            body_bottom.scale(m);
            arm_left.scale(m);
            arm_right.scale(m);
            ball.scale(m);
            curved.scale(m);
        }



        public void rotate_point(float angle, char x, Vector3 pos)
        {
            eye.rotate_point(angle, x, pos);
            neckeye.rotate_point(angle, x, pos);
            head.rotate_point(angle, x, pos);
            accent3.rotate_point(angle, x, pos);
            neck.rotate_point(angle, x, pos);
            accent1.rotate_point(angle, x, pos);
            accent2.rotate_point(angle, x, pos);
            body.rotate_point(angle, x, pos);
            body_bottom.rotate_point(angle, x, pos);
            arm_left.rotate_point(angle, x, pos);
            arm_right.rotate_point(angle, x, pos);
            ball.rotate_point(angle, x, pos);
            curved.rotate_point(angle, x, pos);
        }

        public void rotate_center(float angle, char x)
        {
            //eye.rotate_center(angle, x);
            //neckeye.rotate_center(angle, x);
            //head.rotate_center(angle, x);
            //accent3.rotate_center(angle, x);
            //neck.rotate_center(angle, x);
            //accent1.rotate_center(angle, x);
            //accent2.rotate_center(angle, x);
            //body.rotate_center(angle, x);
            //body_bottom.rotate_center(angle, x);
            //arm_left.rotate_center(angle, x);
            //arm_right.rotate_center(angle, x);
            //ball.rotate_center(angle, x);
            //curved.rotate_center(angle, x);

            rotate_point(angle, x, new Vector3(body.getPosX(), body.getPosY(), body.getPosZ()));
        }

        public void translate(float length,char x)
        {
            eye.translate(length, x);
            neckeye.translate(length, x);
            head.translate(length, x);
            accent3.translate(length, x);
            neck.translate(length, x);
            accent1.translate(length, x);
            accent2.translate(length, x);
            body.translate(length, x);
            body_bottom.translate(length, x);
            arm_left.translate(length, x);
            arm_right.translate(length, x);
            ball.translate(length, x);
            curved.translate(length, x);
        }

        public void render(Camera _camera)
        {
            eye.use_camera(_camera);
            eye.render();

            neckeye.use_camera(_camera);
            neckeye.render();

            neck.use_camera(_camera);
            neck.render();

            head.use_camera(_camera);
            head.render();

            accent3.use_camera(_camera);
            accent3.render();

            accent1.use_camera(_camera);
            accent1.render();

            accent2.use_camera(_camera);
            accent2.render();

            body.use_camera(_camera);
            body.render();

            body_bottom.use_camera(_camera);
            body_bottom.render();

            arm_left.use_camera(_camera);
            arm_left.render();

            arm_right.use_camera(_camera);
            arm_right.render();

            ball.use_camera(_camera);
            ball.render();

            curved.use_camera(_camera);
            curved.render();
        }
    }
}
