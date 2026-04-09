using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坡度基滤波
{
        public static class Neighborhood
        {
            public static List<Point3D> RadiusSearch(
                Point3D center,
                List<Point3D> cloud,
                double radius)
            {
                List<Point3D> neighbors = new List<Point3D>();

                foreach (var p in cloud)
                {
                    double dx = p.X - center.X;
                    double dy = p.Y - center.Y;
                    double dz = p.Z - center.Z;

                    if (Math.Sqrt(dx * dx + dy * dy + dz * dz) <= radius)
                        neighbors.Add(p);
                }

                return neighbors;
            }
        }
    }

