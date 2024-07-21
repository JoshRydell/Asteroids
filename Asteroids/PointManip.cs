using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class PointManip
    {
        public static Point RotatePoint(double theta, Point centre, Point rotatedPoint)
        {
            int x = (int)(Math.Cos(theta) * (rotatedPoint.X - centre.X) - Math.Sin(theta) * (rotatedPoint.Y - centre.Y) + centre.X);
            int y = (int)(Math.Sin(theta) * (rotatedPoint.X - centre.X) + Math.Cos(theta) * (rotatedPoint.Y - centre.Y) + centre.Y);

            return new Point(x, y);
        }

        public static Point CartToPol(double theta, Point StartP,int length)
        {
            Point p = new Point((int)(StartP.X + Math.Cos(theta) * length), (int)(StartP.Y + Math.Sin(theta) * length));
            return p;
        }

        
    }
}
