using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坡度基滤波
{
   
        public static class SlopeFilter
        {
            public static void Apply(
                List<Point3D> cloud,
                double radius,
                double slopeThreshold)
            {
                foreach (var seed in cloud)
                {
                    var neighbors = Neighborhood.RadiusSearch(seed, cloud, radius);

                    if (neighbors.Count < 3) continue;

                    // 取两个最低点（比随便取更稳定）
                    neighbors.Sort((a, b) => a.Z.CompareTo(b.Z));

                    var p1 = neighbors[1];
                    var p2 = neighbors[2];

                    double slope = Geometry.ComputeSlope(seed, p1, p2);

                    if (slope <= slopeThreshold)
                        seed.IsGround = true;
                }
            }
        }
    }
