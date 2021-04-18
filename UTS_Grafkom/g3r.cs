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
    class g3r
    {
        static string name = "ger";

        Box box_right = new Box("antena", name);
        Box box_left = new Box("antena", name);

        Sphere elipsoid = new Sphere("head", name);

        Face face = new Face("black", name);
        Eye left_eye = new Eye("eye", name);
        Eye right_eye = new Eye("eye", name);

        Body body = new Body("body", name);

        Box foot_right = new Box("foot", name);
        Box foot_left = new Box("foot", name);

        Cylinder_Curve hand_left = new Cylinder_Curve("hand", name);
        Cylinder_Curve hand_right = new Cylinder_Curve("hand", name);

        
        int animate_number = 0;
        float counter = 0;
        Vector3 laspos = new Vector3();

        public g3r() { }

        protected void setup()
        {
            box_right.setupObject();
            box_left.setupObject();
            elipsoid.setupObject();
            face.setupObject();
            left_eye.setupObject();
            right_eye.setupObject();
            body.setupObject();
            foot_left.setupObject();
            foot_right.setupObject();
            hand_left.setupObject();
            hand_right.setupObject();
        }

        public void load()
        {
            body.createEllipsoidVertices(0.0f, 0.0f, 0.0f, 0.5f, 0.3f);

            elipsoid.createEllipsoidVertices(body.getPosX(), body.getPosY()+2*body.getRadius()+0.3f, body.getPosZ(), 0.3f);

            face.createEllipsoidVertices(elipsoid.getPosX(), elipsoid.getPosY(), elipsoid.getPosZ()- elipsoid.getRadius(), 0.15f);

            box_right.createBoxvertices(elipsoid.getPosX() + elipsoid.getRadius()*2/3,elipsoid.getPosY() + elipsoid.getRadius(), elipsoid.getPosZ(), 0.03f, 0.25f, 0.03f);
            box_left.createBoxvertices(elipsoid.getPosX() - elipsoid.getRadius()*2/3, elipsoid.getPosY() + elipsoid.getRadius(), elipsoid.getPosZ(), 0.03f, 0.25f, 0.03f);

            

            left_eye.createEllipsoidVertices(box_left.getPosX(), box_left.getPosY() + box_left.getLengthY()/3,box_left.getPosZ(), 0.05f);
            right_eye.createEllipsoidVertices(box_right.getPosX(), box_right.getPosY() + box_right.getLengthY() / 3, box_right.getPosZ(), 0.05f);

            

            foot_left.createBoxvertices(body.getPosX() + body.getRadius()/3, body.getPosY(), body.getPosZ() - 0.55f/2,0.1f, 0.03f, 0.55f);
            foot_right.createBoxvertices(body.getPosX() - body.getRadius() / 3, body.getPosY(), body.getPosZ() - 0.55f / 2, 0.1f, 0.03f, 0.55f);

            hand_left.createEllipsoidVertices(body.getPosX()-body.getRadius()*1.25f, body.getPosY()+body.getRadius(), body.getPosZ(), 0.1f, 0.8f, 0.6f);
            hand_right.createEllipsoidVertices(body.getPosX()+body.getRadius()*1.25f, body.getPosY()+body.getRadius(), body.getPosZ(), 0.1f, 0.8f, -0.6f);

            
            setup();
            
            
            //Console.WriteLine("Start :" + (0f - (foot_right.getPosY() - foot_right.getLengthY() / 2)));
            //translate(0f, 'y');
            //rotate(90, 'x');
            //rotate(-90, 'x');
            //rotate(90, 'y');
            //rotate(-90, 'y');
            //rotate(90, 'z');
            //rotate(-90, 'z');

            //hand_left.rotate_center(45, 'x');

            translate(-0 - (foot_left.getPosY() - foot_left.getLengthY() / 2) + 0.05f, 'y');

        }

        public void translate(float length, char x)
        {
            box_right.translate(length, x);
            box_left.translate(length, x);
            elipsoid.translate(length, x);
            face.translate(length, x);
            left_eye.translate(length, x);
            right_eye.translate(length, x);
            body.translate(length, x);
            foot_left.translate(length, x);
            foot_right.translate(length, x);
            hand_left.translate(length, x);
            hand_right.translate(length, x);

        }
        public void translate(Vector3 pos)
        {
            box_right.translate(pos);
            box_left.translate(pos);
            elipsoid.translate(pos);
            face.translate(pos);
            left_eye.translate(pos);
            right_eye.translate(pos);
            body.translate(pos);
            foot_left.translate(pos);
            foot_right.translate(pos);
            hand_left.translate(pos);
            hand_right.translate(pos);

        }

        public void rotate(float angle, char x)
        {
            box_right.rotate(angle, x);
            box_left.rotate(angle, x);
            elipsoid.rotate(angle, x);
            face.rotate(angle, x);
            left_eye.rotate(angle, x);
            right_eye.rotate(angle, x);
            body.rotate(angle, x);
            foot_left.rotate(angle, x);
            foot_right.rotate(angle, x);
            hand_left.rotate(angle, x);
            hand_right.rotate(angle, x);
        }

        public void rotate_center(float angle, char x)
        {
            box_right.rotate_center(angle, x);
            box_left.rotate_center(angle, x);
            elipsoid.rotate_center(angle, x);
            face.rotate_center(angle, x);
            left_eye.rotate_center(angle, x);
            right_eye.rotate_center(angle, x);
            body.rotate_center(angle, x);
            foot_left.rotate_center(angle, x);
            foot_right.rotate_center(angle, x);
            hand_left.rotate_center(angle, x);
            hand_right.rotate_center(angle, x);
        }

        public void scale(float m)
        {
            box_right.scale(m);
            box_left.scale(m);
            elipsoid.scale(m);
            face.scale(m);
            left_eye.scale(m);
            right_eye.scale(m);
            body.scale(m);
            foot_left.scale(m);
            foot_right.scale(m);
            hand_left.scale(m);
            hand_right.scale(m);
        }

        public void animate()
        {
            switch (animate_number)
            {
                case 0:
                    translate(-0.005f, 'z');
                    counter += 0.005f;

                    if((int)(counter*1000) % 2 == 0)
                    {
                        foot_left.translate(-0.01f, 'z');
                        foot_right.translate(0.01f, 'z');
                    }
                    else
                    {
                        foot_right.translate(-0.01f, 'z');
                        foot_left.translate(0.01f, 'z');
                    }

                    if(counter >= 3f)
                    {
                        animate_number++;
                        counter = 0;
                        foot_left.translate(0.01f, 'z');
                    }
                    break;

                case 1:
                    hand_left.rotate_center(0.3f, 'x');
                    hand_right.rotate_center(0.3f, 'x');
                    counter += 0.3f;
                    if(counter >= 180)
                    {
                        animate_number++;
                        counter = 0;
                        laspos = body.getPos();
                    }
                    break;

                case 2:
                    hand_left.rotate_center(-0.5f, 'x');
                    hand_right.rotate_center(-0.5f, 'x');
                    translate(0.005f, 'z');
                    counter += 0.005f;

                    if ((int)(counter * 1000) % 2 == 0)
                    {
                        foot_left.translate(0.01f, 'z');
                        foot_right.translate(-0.01f, 'z');
                    }
                    else
                    {
                        foot_right.translate(0.01f, 'z');
                        foot_left.translate(-0.01f, 'z');
                    }

                    if (counter >= 3f)
                    {
                        animate_number = 0;
                        counter = 0;
                        foot_left.translate(-0.01f, 'z');
                    }

                    break;

            }
        }

        public void render(Camera _camera)
        {
            box_right.use_camera(_camera);
            box_right.render();

            box_left.use_camera(_camera);
            box_left.render();

            elipsoid.use_camera(_camera);
            elipsoid.render();

            face.use_camera(_camera);
            face.render();

            left_eye.use_camera(_camera);
            left_eye.render();

            right_eye.use_camera(_camera);
            right_eye.render();

            body.use_camera(_camera);
            body.render();

            foot_left.use_camera(_camera);
            foot_left.render();

            foot_right.use_camera(_camera);
            foot_right.render();

            hand_left.use_camera(_camera);
            hand_left.render();

            hand_right.use_camera(_camera);
            hand_right.render();

            //Console.WriteLine(foot_right.getPosY());
        }
    }
}
