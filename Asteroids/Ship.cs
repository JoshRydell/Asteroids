using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Ship
    {

        public Point position;
        public int score { get; set; }
        public float vectorMagnitude { get; set; }

        float xvel;
        float yvel;




        public double rotation { get; set; }
        public bool lRotate { get; set; }
        public bool rRotate { get; set; }
        public bool up { get; set; }
        float Xdrift;
        float Ydrift;

        public Point centre { get; private set; }
        public Point top { get; private set; }
        public Point vertex2 { get; private set; }
        public Point vertex3 { get; private set; }
        
        


        public Ship(Point position)
        {
            this.position = position;
            score = 0;
            speedReset();

            
            rotation = -Math.PI / 2;

        }

        public void Update(Graphics g)
        {

            yvel = (int)(vectorMagnitude * Math.Sin(rotation));
            xvel = (int)(vectorMagnitude * Math.Cos(rotation));

            double rotationMag = 0.15;
            int sideLen = 30;
            int angle = 60;

            top = new Point(position.X, position.Y);
            vertex2 = PointManip.CartToPol(angle, top, sideLen);
            vertex3 = PointManip.CartToPol(-angle, top, sideLen);
            centre = new Point((position.X+ vertex2.X + vertex3.X) / 3, (position.Y + vertex2.Y + vertex3.Y) / 3);
            top = PointManip.RotatePoint(rotation, centre, top);
            vertex2 = PointManip.RotatePoint(rotation, centre, vertex2);
            vertex3 = PointManip.RotatePoint(rotation, centre, vertex3);
            
            if (lRotate)
            {
                rotation -= rotationMag;
            }
            if (rRotate)
            {
                rotation += rotationMag;
            }
            if (up)
            {
                vectorMagnitude += 0.15f;
                Xdrift = xvel;
                Ydrift = yvel;
                position.X += (int)xvel;
                position.Y += (int)yvel;
            }
            if (!up && Math.Abs(Xdrift) > 0.1 || Math.Abs(Ydrift) > 0.1)
            {
                position.X += (int)Xdrift;
                position.Y += (int)Ydrift;
                Xdrift *= 0.95f;
                Ydrift *= 0.95f;
            }



            g.DrawPolygon(Pens.White, new Point[] { top, vertex2, vertex3 });




            
        }

        public void hypspace(int width, int height)
        {
            Random r = new Random();
            
            position.X = r.Next(width);
            position.Y = r.Next(height);
        }

        public void speedReset()
        {
            vectorMagnitude = 3;
        }

        public void setTop (Point top)
        {
            this.top = top;
        }
        public void setCentre(Point centre)
        {
            this.centre = centre;
        }
        public void setVertex2(Point vertex2)
        {
            this.vertex2 = vertex2;
        }
        public void setVertex3(Point vertex3)
        {
            this.vertex3 = vertex3;
        }
        public void setUnrotatedPosition(Point position)
        {
            this.position = position;
        }

    }
}
