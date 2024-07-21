using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Asteroids
{
    class Shot
    {
        public int xpos { get; private set; }
        public int ypos { get; private set; }
        Point Position;
        public int xAccPos
        {
            get { return Position.X; }
            set { Position.X = value; }
        }
        public int yAccPos
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }
        int xvel;
        int yvel;
        int xcent;
        int ycent;
        double rot;
        public Shot(int xStart, int ystart,double rotation)
        {
            Position = new Point(xStart, ystart);
            rot = rotation;
            xcent = xStart;
            ycent = ystart;
            xpos = xStart;
            ypos = ystart;
            xvel = 10;
            yvel = 0;
        }

        public void Update(Graphics g)
        {
            
            xpos += xvel;
            ypos += yvel;
            Position = PointManip.RotatePoint(rot, new Point(xcent, ycent), new Point(xpos, ypos));

            g.FillRectangle(Brushes.White, PointManip.RotatePoint(rot, new Point(xcent, ycent), new Point(xpos, ypos)).X, PointManip.RotatePoint(rot, new Point(xcent, ycent), new Point(xpos, ypos)).Y, 2, 2);
            
        }

        
    }
}
