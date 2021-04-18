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
    class X07
    {
        float _positionX;
        float _positionY;

        static string name = "allen";

        Wheel wheel_1 = new Wheel("darkgray",name);
        Wheel wheel_2 = new Wheel("darkgray", name);

        Cylinder cylinder1 = new Cylinder("gray", name);
        Cylinder cylinder2 = new Cylinder("gray", name);

        Cylinder Body = new Cylinder("lightgray", name);
        Half_Sphere head = new Half_Sphere("verylightgray", name);
        Half_Sphere eye = new Half_Sphere("red", name);

        Cylinder stick = new Cylinder("darkgray", name);

        Sphere ball = new Sphere("red", name);

        Box box1 = new Box("verylightgray", name);
        Box box2 = new Box("verylightgray", name);
        Box plank = new Box("darkgray", name);

        Box ear1 = new Box("darkgray", name);
        Box ear2 = new Box("darkgray", name);

        Cylinder_Curve pipe = new Cylinder_Curve("red", name);

        
        int animate_number;
        float angle=0;
        float counter=0;

        public X07(float _positionX = 0.0f, float _positionY = 0.0f) {
            this._positionX = _positionX;
            this._positionY = _positionY;
        }

        public void animate()
        {
            switch (animate_number)
            {
                case 0:
                    Console.WriteLine(plank.getPosY() + " "+ (box1.getPosY() + box1.getLengthX()));
                    plank.translate(0.01f, 'y');
                    if(plank.getPosY() >= box1.getPosY() + box1.getLengthX() - 0.05f)
                    {
                        animate_number++;
                        plank.translate(0f, 'y');
                    }
                    break;

                case 1:
                    //Console.WriteLine(plank.getPosY() + " " + (box1.getPosY() + box1.getLengthX()));
                    plank.translate(-0.01f, 'y');
                    if (plank.getPosY() <= box1.getPosY() )
                    {
                        animate_number++;
                        plank.translate(0f, 'y');
                    }
                    break;
                case 2:
                    //Console.WriteLine(plank.getPosY() + " " + (box1.getPosY() + box1.getLengthX()));
                    plank.translate(0.01f, 'y');
                    if (plank.getPosY() >= box1.getPosY() + box1.getLengthX() - 0.05f)
                    {
                        animate_number++;
                        plank.translate(0f, 'y');
                    }
                    break;

                case 3:
                    //Console.WriteLine(plank.getPosY() + " " + (box1.getPosY() + box1.getLengthX()));
                    plank.translate(-0.01f, 'y');
                    if (plank.getPosY() <= box1.getPosY())
                    {
                        animate_number++;
                        plank.translate(0f, 'y');
                    }
                    break;
                case 4:

                    if (angle <= 10 && angle >= 0)
                    {
                        translate(-0.1f, 'z');
                    }
                    else if (angle >= 80 && angle <= 100)
                    {
                        translate(-0.1f, 'x');

                    } else if (angle >=170 && angle <= 190) {

                        translate(0.1f, 'z');

                    } else if (angle >= 260 && angle <= 280)
                    {
                        translate(0.1f, 'x');
                    }
                    
                    counter += 0.1f;
                    if (counter >=10f)
                    {
                        animate_number++;
                        counter = 0;
                        
                    }
                    break;
                case 5:
                    rotate_center(1f, 'y');
                    angle += 1f;

                    if (angle >= 360f)
                    {
                        angle = 0;
                        animate_number = 0;
                    }else if(angle >= 90f && angle<90.5f)
                    {
                        animate_number = 0;
                    }
                    else if (angle >= 180f && angle < 180.5f)
                    {
                        animate_number = 0;
                    }
                    else if (angle >= 270f && angle < 270.5f)
                    {
                        animate_number = 0;
                    }
                    Console.WriteLine(angle);
                    break;
                
                default: break;
            }
        }

        protected void setup()
        {
            animate_number = 0;
            ball.setupObject();
            stick.setupObject();
            head.setupObject();
            Body.setupObject();
            eye.setupObject();
            wheel_1.setupObject();
            wheel_2.setupObject();
            cylinder1.setupObject();
            cylinder2.setupObject();

            box1.setupObject();
            box2.setupObject();
            plank.setupObject();

            ear1.setupObject();
            ear2.setupObject();

            pipe.setupObject();
        }

        public void load()
        {
            Body.createEllipsoidVertices(0.0f, 0f, 0.1f, 0.3f, 1f);
            wheel_1.createEllipsoidVertices(Body.getPosX(), Body.getPosY() - Body.getRadius() - 0.1f, Body.getPosZ() + Body.getHeight() / 3);
            wheel_2.createEllipsoidVertices(Body.getPosX(), Body.getPosY() + Body.getRadius() + 0.1f, Body.getPosZ() + Body.getHeight() / 3);
            cylinder1.createEllipsoidVertices(Body.getPosX(), Body.getPosY() - Body.getRadius() + 0.1f, Body.getPosZ() + Body.getHeight() / 3, 0.215f, 0.5f);
            cylinder2.createEllipsoidVertices(Body.getPosX(), Body.getPosY() + Body.getRadius() - 0.1f, Body.getPosZ() + Body.getHeight() / 3, 0.215f, 0.5f);

            head.createEllipsoidVertices(Body.getPosX(), Body.getPosY(), Body.getPosZ() - Body.getHeight() / 2, 0.35f);
            eye.createEllipsoidVertices(Body.getPosX() + Body.getRadius(), Body.getPosY(), Body.getPosZ() - Body.getHeight() / 4, 0.1f);

            box1.createBoxvertices(Body.getPosX() + Body.getRadius(), Body.getPosY() + Body.getRadius() / 2, Body.getPosZ() + Body.getRadius() * 3 / 4, 0.5f, 0.05f, 0.05f);
            box2.createBoxvertices(Body.getPosX() + Body.getRadius(), Body.getPosY() - Body.getRadius() / 2, Body.getPosZ() + Body.getRadius() * 3 / 4, 0.5f, 0.05f, 0.05f);
            plank.createBoxvertices((box1.getPosX() + box2.getPosX()) / 2 + 5 * box1.getLengthY(), (box1.getPosY() + box2.getPosY()) / 2, (box1.getPosZ() + box2.getPosZ()) / 2 + box1.getLengthX() / 2 - box1.getLengthY(), 0.5f, 0.5f, 0.05f);

            ear1.createBoxvertices(Body.getPosX(), Body.getPosY() + Body.getRadius() + 0.02f, eye.getPosZ(), 0.1f, 0.05f, 0.3f);
            ear2.createBoxvertices(Body.getPosX(), Body.getPosY() - Body.getRadius() - 0.02f, eye.getPosZ(), 0.1f, 0.05f, 0.3f);

            stick.createEllipsoidVertices(head.getPosX() - 0.1f, head.getPosY(), head.getPosZ() - head.getRadius() - 0.05f, 0.02f, 0.2f);
            ball.createEllipsoidVertices(stick.getPosX() - 0.06f, stick.getPosY(), stick.getPosZ() - 0.1f, 0.03f);

            pipe.createEllipsoidVertices(Body.getPosX() - Body.getRadius() + 0.1f, Body.getPosY(), Body.getPosZ(), 0.07f, Body.getHeight() * 7 / 8); 
            setup();


            //wheele
            wheel_1.rotate_center(90, 'y');
            wheel_1.rotate_center(90, 'z');
            wheel_2.rotate_center(90, 'y');
            wheel_2.rotate_center(90, 'z');

            cylinder1.rotate_center(90, 'y');
            cylinder1.rotate_center(90, 'z');

            cylinder2.rotate_center(-90, 'y');
            cylinder2.rotate_center(90, 'z');

            eye.rotate_center(-90, 'y');

            box1.rotate_center(90, 'y');
            box2.rotate_center(90, 'y');

            stick.rotate_center(30, 'y');

            pipe.rotate_center(180, 'z');

            //rotate_point(90, 'x', new Vector3(Body.getPosX(), Body.getPosY(), Body.getPosZ()));
            //rotate_point(90, 'y', new Vector3(Body.getPosX(), Body.getPosY(), Body.getPosZ()));

            rotate(90, 'x');
            rotate(90, 'y');

            //translate(3f, 'y');
            translate(-0 - (wheel_1.getPosY() - 2*wheel_1.getRadius()) + 0.15f,'y');
            //translate(-3f, 'z');
            //translate(2f, 'x');
        }

        public void rotate(float angle, char x)
        {
            box1.rotate(angle, x);
            box2.rotate(angle, x);
            plank.rotate(angle, x);
            head.rotate(angle, x);
            Body.rotate(angle, x);
            wheel_1.rotate(angle, x);
            wheel_2.rotate(angle, x);
            cylinder1.rotate(angle, x);
            cylinder2.rotate(angle, x);
            eye.rotate(angle, x);

            ear1.rotate(angle, x);
            ear2.rotate(angle, x);

            stick.rotate(angle, x);
            ball.rotate(angle, x);

            pipe.rotate(angle, x);
        }

        public void rotate_point(float angle, char x, Vector3 pos = new Vector3())
        {
            box1.rotate_point(angle,x,pos);
            box2.rotate_point(angle,x,pos);
            plank.rotate_point(angle,x,pos);
            head.rotate_point(angle,x,pos);
            Body.rotate_point(angle,x,pos);
            wheel_1.rotate_point(angle,x,pos);
            wheel_2.rotate_point(angle,x,pos);
            cylinder1.rotate_point(angle,x,pos);
            cylinder2.rotate_point(angle,x,pos);
            eye.rotate_point(angle,x,pos);

            ear1.rotate_point(angle,x,pos);
            ear2.rotate_point(angle,x,pos);

            stick.rotate_point(angle,x,pos);
            ball.rotate_point(angle,x,pos);

            pipe.rotate_point(angle,x,pos);
        }

        public void rotate_center(float angle, char x)
        {
            box1.rotate_center(angle, x);
            box2.rotate_center(angle, x);
            plank.rotate_center(angle, x);
            head.rotate_center(angle, x);
            Body.rotate_center(angle, x);
            wheel_1.rotate_center(angle, x);
            wheel_2.rotate_center(angle, x);
            cylinder1.rotate_center(angle, x);
            cylinder2.rotate_center(angle, x);
            eye.rotate_center(angle, x);

            ear1.rotate_center(angle, x);
            ear2.rotate_center(angle, x);

            stick.rotate_center(angle, x);
            ball.rotate_center(angle, x);

            pipe.rotate_center(angle, x);
        }

        public void translate(float length, char x)
        {
            box1.translate(length, x);
            box2.translate(length, x);
            plank.translate(length, x);
            head.translate(length, x);
            Body.translate(length, x);
            wheel_1.translate(length, x);
            wheel_2.translate(length, x);
            cylinder1.translate(length, x);
            cylinder2.translate(length, x);
            eye.translate(length, x);

            ear1.translate(length, x);
            ear2.translate(length, x);

            stick.translate(length, x);
            ball.translate(length, x);

            pipe.translate(length, x);
        }

        public void scale(float m)
        {
            box1.scale(m);
            box2.scale(m);
            plank.scale(m);
            head.scale(m);
            Body.scale(m);
            wheel_1.scale(m);
            wheel_2.scale(m);
            cylinder1.scale(m);
            cylinder2.scale(m);
            eye.scale(m);

            ear1.scale(m);
            ear2.scale(m);

            stick.scale(m);
            ball.scale(m);

            pipe.scale(m);
        }

        public void render(Camera _camera)
        {
            box1.use_camera(_camera);
            box1.render();

            box2.use_camera(_camera);
            box2.render();

            plank.use_camera(_camera);
            plank.render();

            head.use_camera(_camera);
            head.render();

            Body.use_camera(_camera);
            Body.render();

            wheel_1.use_camera(_camera);
            wheel_1.render();

            wheel_2.use_camera(_camera);
            wheel_2.render();

            cylinder1.use_camera(_camera);
            cylinder1.render();

            cylinder2.use_camera(_camera);
            cylinder2.render();

            eye.use_camera(_camera);
            eye.render();

            ear1.use_camera(_camera);
            ear1.render();

            ear2.use_camera(_camera);
            ear2.render();

            stick.use_camera(_camera);
            stick.render();

            ball.use_camera(_camera);
            ball.render();

            pipe.use_camera(_camera);
            pipe.render();
        }
    }
}
