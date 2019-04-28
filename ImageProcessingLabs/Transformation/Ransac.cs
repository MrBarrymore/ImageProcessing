using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Transformation
{
    class Ransac
    {

        public static ValueTuple<double, double> Transform(List<double> h, int x, int y)
        {
            var newX = (h[0] * x + h[1] * y + h[2]) / (h[6] * x + h[7] * y + h[8]);
            var newY = (h[3] * x + h[4] * y + h[5]) / (h[6] * x + h[7] * y + h[8]);

            return new ValueTuple<double, double>(newX, newY);
        }


    }
}
