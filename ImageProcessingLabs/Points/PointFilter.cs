using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImageProcessingLabs.Helper;
using ImageProcessingLabs.Wrapped;

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



    public static class NonMaximumSuppression
    {
        private static readonly double C_ROBUST = 0.9;

        public static List<InterestingPoint> FilterA(WrappedImage image, int minAmount)
        {
            var maxRadius = Math.Sqrt(WrappedImage.sqr(image.width) + WrappedImage.sqr(image.height));
            return FilterA(CornerDetectionHelper2.MatToPoints(image), minAmount, maxRadius);
        }

        public static List<InterestingPoint> FilterA(List<InterestingPoint> points, int minAmount, double maxRadius)
        {
            points = points.OrderBy(x => x.probability).ToList();

            var used = new bool[points.Count];

            double left = 0;
            var right = maxRadius;

            while (right - left > 1)
            {
                var middle = (left + right) / 2;
                var radius2 = WrappedImage.sqr(middle);

                if (CountWithRadius(points, radius2, used) < minAmount)
                    right = middle;
                else
                    left = middle;
            }

            CountWithRadius(points, WrappedImage.sqr(left), used);

            return points.Where((t, i) => !used[i]).ToList();
        }

        private static int CountWithRadius(List<InterestingPoint> points, double radiusSquared, bool[] used)
        {
            Array.Clear(used, 0, used.Length);

            for (var i = 0; i < points.Count; i++)
            {
                if (used[i]) continue;

                for (var j = 0; j < points.Count; j++)
                {
                    if (i == j || points[i].Distance(points[j]) > radiusSquared)
                        continue;

                    used[j] |= points[j].probability - points[i].probability < 1e-6;
                }
            }

            var count = 0;
            for (var i = 0; i < points.Count; i++)
                if (!used[i])
                    count++;

            return count;
        }


        public static List<InterestingPoint> FilterB(List<InterestingPoint> points, int amount)
        {
            var rads = new List<ValueTuple<InterestingPoint, double>>(points.Count);

            foreach (var a in points)
            {
                var r = double.MaxValue;

                foreach (var b in points)
                    if (a.probability < C_ROBUST * b.probability)
                        r = Math.Min(r, a.Distance(b));

                rads.Add(new ValueTuple<InterestingPoint, double>(a, r));
            }

            return rads.OrderBy(x => x.Item2)
                .Select(x => x.Item1)
                .Reverse()
                .Take(amount)
                .ToList();
        }


    }

}
