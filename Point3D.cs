using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坡度基滤波
{
   
        public class Point3D
        {
            public double X, Y, Z;
            public bool IsGround = false;

            public Point3D(double x, double y, double z)
            {
                X = x; Y = y; Z = z;
            }

            public static Point3D operator -(Point3D a, Point3D b)
            {
                return new Point3D(
                    a.X - b.X,
                    a.Y - b.Y,
                    a.Z - b.Z
                );
            }
        }
    }
