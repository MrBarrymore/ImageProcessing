using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs
{
    public class InterestingPoint
    {
        public int x;

        public int y;

        public double probability;


        public InterestingPoint(int x, int y, double probability)
        {
            this.x = x;
            this.y = y;
            this.probability = probability;
        }

    }
}
