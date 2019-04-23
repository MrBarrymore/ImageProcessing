using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs
{
    public class InterestingPoint
    {
        public int y;
        public int x;

        public double probability;

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
