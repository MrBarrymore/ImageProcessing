using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using  ImageProcessingLabs.Wrapped;

namespace ImageProcessingLabs.Helper
{
    class CornerDetectionHelper2
    {
        public static List<InterestingPoint> MatToPoints(WrappedImage mat)
        {
            var list = new List<InterestingPoint>();
            for (var y = 0; y < mat.height; y++)
            for (var x = 0; x < mat.width; x++)
                    if (mat.buffer[y,x] > 1e-6)
                    list.Add(new InterestingPoint(x, y, mat.buffer[y,x]));

            return list;
        }


    }
}
