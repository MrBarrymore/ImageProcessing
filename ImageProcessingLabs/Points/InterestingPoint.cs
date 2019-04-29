using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImageProcessingLabs.Wrapped;
namespace ImageProcessingLabs
{
    public class InterestingPoint
    {
        private int y;
        private int x;
        public double probability;
        public readonly int Octave;
        public readonly int Radius;
        public List<double> Angles = new List<double>();

        public InterestingPoint(int y, int x, double probability)
        {
            this.y = y;
            this.x = x;
            this.probability = probability;
        }

        public static double Distance(InterestingPoint a, InterestingPoint b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.y, 2) + Math.Pow(a.x - b.y, 2));
        }

        public double Distance(InterestingPoint b)
        {
            // квадрат расстояния
            return WrappedImage.sqr(x - b.x) + WrappedImage.sqr(y - b.y);
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
    }
}
