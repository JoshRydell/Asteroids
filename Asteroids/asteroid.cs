using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class asteroid
    {
        Point velocity;
        public Point centre;
        
        public Point[] points;
        public int rad { get; set; }
        public asteroid(int xStart,int yStart,int seed,int max, Size maxSize, int score)
        {
            Random r = new Random(seed);
            centre = new Point(xStart, yStart);
            int velocityScalar = 1000;
            score = (int)Math.Pow(score, 0.75);
            if (centre.Y > 0 && centre.Y < maxSize.Height && centre.X > 0 && centre.X < maxSize.Width)
            {
                velocity.X = r.Next(-2, 2);
                velocity.Y = CalcOtherVel(velocity.X);
            }
            else if (centre.Y < 0)
            {
                velocity.Y = r.Next(1, 3);
                velocity.X = CalcOtherVel(velocity.Y);
            }
            else if (centre.Y > maxSize.Height)
            {
                velocity.Y = r.Next(-2, 0);
                velocity.X = CalcOtherVel(velocity.Y);
            }
            else if (centre.X < 0) 
            {
                velocity.X = r.Next(0, 2);
                velocity.Y = CalcOtherVel(velocity.X);
            }
            else if (centre.X > maxSize.Width)
            {
                velocity.X = r.Next(-2, 0);
                velocity.Y = CalcOtherVel(velocity.X);
            }

            velocity.X *= (score + velocityScalar) / velocityScalar;
            velocity.Y *= (score+velocityScalar)/ velocityScalar;

            rad = r.Next(10, max);
            int sides = r.Next(5, 10);
            points = new Point[sides];
            double oneAngle = (Math.PI * 2) / sides;
            
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = PointManip.CartToPol(i * oneAngle, centre, rad);
            }
            


        }
        
        public void update(Graphics g)
        {
            
            

            centre.X += velocity.X;
            centre.Y += velocity.Y;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X += velocity.X;
                points[i].Y += velocity.Y;
            }


            g.DrawPolygon(Pens.White, points);

           
        }


        private int CalcOtherVel(int vel)
        {
            int NewVel = (int)((Math.Pow(-1, 1)) * (1 - Math.Pow(vel, 2)));
            return NewVel;
        }
        
    }
}