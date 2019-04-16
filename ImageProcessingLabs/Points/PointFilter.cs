using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Points
{
    class PointFilter
    {

        public static List<InterestingPoint> filterPoints(List<InterestingPoint> interestingPoints, int maxSize)
        {
            if (maxSize >= interestingPoints.Count)
                return interestingPoints;
            if (maxSize < 0)
                return new List<InterestingPoint>();

            double l = 0, r = 1e9;
            int cnt = 60;
            while (cnt-- > 0)
            {
                double middle = (l + r) / 2;
                if (filter(interestingPoints, middle).Count() > maxSize)
                {
                    l = middle;
                }
                else
                {
                    r = middle;
                }
            }
            List<InterestingPoint> Filter = filter(interestingPoints, l);
            return Filter.GetRange(0, Math.Min(Filter.Count, maxSize));
        }

        private static List<InterestingPoint> filter(List<InterestingPoint> interestingPoints, double radius)
        {
            List<InterestingPoint> filtered = new List<InterestingPoint>();
            foreach (InterestingPoint point in interestingPoints)
            {
                bool ok = true;
                foreach (InterestingPoint anotherPoint in interestingPoints)
                {
                    if (point.Equals(anotherPoint)) continue;
                    if (InterestingPoint.Distance(point, anotherPoint) < radius)
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    filtered.Add(point);
                }
            }
            return filtered;
        }


    }
}
