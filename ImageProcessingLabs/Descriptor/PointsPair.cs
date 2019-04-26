using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Descriptor
{
    public class PointsPair
    {
        public InterestingPoint pointA;
        public InterestingPoint pointB;


        public static PointsPair from(InterestingPoint point1, InterestingPoint point2)
        {
            PointsPair point = new PointsPair();

            point.pointA = point1;
            point.pointB = point2;

            return point;
        }

    }
}
