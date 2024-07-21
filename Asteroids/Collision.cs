using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Collision
    {
        public static bool Hit(Shot shot,asteroid ast)
        {
            //double x = Math.Pow((shot.xAccPos - ast.centre.X), 2);
            if (Math.Pow(shot.xAccPos - ast.centre.X, 2) + Math.Pow(shot.yAccPos - ast.centre.Y, 2) < Math.Pow(ast.rad, 2))
            {
                return true;
            }
            return false;
        }

        public static bool collide(Ship ship, asteroid ast,int width,int height)
        {

            
            if (Math.Pow(ship.top.X - ast.centre.X, 2) + Math.Pow(ship.top.Y - ast.centre.Y, 2) < Math.Pow(ast.rad, 2) && ship.top.X > 0 && ship.top.X < width && ship.top.Y> 0 && ship.top.Y < height)
            {
                 return actualCollision(ship, ast);
            }
            else if (Math.Pow(ship.vertex2.X - ast.centre.X, 2) + Math.Pow(ship.vertex2.Y - ast.centre.Y, 2) < Math.Pow(ast.rad, 2) && ship.vertex2.X > 0 && ship.vertex2.X < width && ship.vertex2.Y> 0 && ship.vertex2.Y < height)
            {
                return actualCollision(ship, ast);
            }
            else if (Math.Pow(ship.vertex3.X - ast.centre.X, 2) + Math.Pow(ship.vertex3.Y - ast.centre.Y, 2) < Math.Pow(ast.rad, 2) && ship.vertex3.X > 0 && ship.vertex3.X < width && ship.vertex3.X > 0 && ship.vertex3.Y < height)
            {
                return actualCollision(ship, ast);
            }
            return false;
        }

        private static bool actualCollision(Ship ship, asteroid ast)
        {
            
            for (int i = 0; i < ast.points.Length-1;i++)
            {
                if (lineIntersection(ship.top.X,ship.top.Y,ship.vertex2.X,ship.vertex2.Y,ast.points[i].X,ast.points[i].Y, ast.points[i+1].X, ast.points[i+1].Y) || lineIntersection(ship.top.X,ship.top.Y,ship.vertex3.X,ship.vertex3.Y, ast.points[i].X, ast.points[i].Y, ast.points[i + 1].X, ast.points[i + 1].Y)|| lineIntersection(ship.vertex2.X,ship.vertex2.Y,ship.vertex3.X,ship.vertex3.Y, ast.points[i].X, ast.points[i].Y, ast.points[i + 1].X, ast.points[i + 1].Y))
                {
                    return true;
                }
            }
            if (lineIntersection(ship.top.X, ship.top.Y, ship.vertex2.X, ship.vertex2.Y, ast.points[ast.points.Length-1].X, ast.points[ast.points.Length - 1].Y, ast.points[0].X, ast.points[0].Y) || lineIntersection(ship.top.X, ship.top.Y, ship.vertex3.X, ship.vertex3.Y, ast.points[ast.points.Length - 1].X, ast.points[ast.points.Length - 1].Y, ast.points[0].X, ast.points[0].Y) || lineIntersection(ship.vertex2.X, ship.vertex2.Y, ship.vertex3.X, ship.vertex3.Y, ast.points[ast.points.Length - 1].X, ast.points[ast.points.Length - 1].Y, ast.points[0].X, ast.points[0].Y))
            {
                return true;
            }
            return false;
        }
        private static bool lineIntersection(float x1l1, float y1l1, float x2l1, float y2l1, float x1l2, float y1l2, float x2l2, float y2l2)
        {
            float m1 = (y1l1 - y2l1) / (x1l1 - x2l2);
            float m2 = (y1l2 - y2l2) / (x1l2 - x2l2);
            if (m1 == m2) return false;
            float x = (m1 * x1l1 - m2 * x1l2 - y1l1 + y2l1) / (m1 - m2);
            float y = m1*(x - x1l1) + y1l1;

            if (x > x1l1 && x < x2l1 && y > y1l1 && y < y2l1 && x1l1 < x2l1 && y1l1 < y2l1 || x > x2l1 && x < x1l1 && y < y1l1 && y > y2l1 && x1l1 > x2l1 && y2l1 < y1l1 || x > x2l1 && x < x1l1 && y > y1l1 && y < y2l1 && x1l1 > x2l1 && y2l1 > y1l1 || x < x2l1 && x > x1l1 && y < y1l1 && y > y2l1 && x1l1 < x2l1 && y2l1 < y1l1)
            {
                return true;
            }
            
            return false;

        }
    }
}
