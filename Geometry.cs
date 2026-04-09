using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坡度基滤波
{
   
        public static class Geometry
        {
            public static Point3D Cross(Point3D a, Point3D b)
            {
                return new Point3D(
                    a.Y * b.Z - a.Z * b.Y,
                    a.Z * b.X - a.X * b.Z,
                    a.X * b.Y - a.Y * b.X
                );
            }

            public static double Norm(Point3D v)
            {
                return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            }

            // 三角面片坡度角（degree）
            public static double ComputeSlope(Point3D p0, Point3D p1, Point3D p2)
            {
                var v1 = p1 - p0;
                var v2 = p2 - p0;

                var n = Cross(v1, v2);
                double len = Norm(n);

                if (len == 0) return 90.0;

                double cosA = Math.Abs(n.Z) / len;
                return Math.Acos(cosA) * 180.0 / Math.PI;
            }
        }
    }
