using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageProcessingLabs.Helper
{
    class CornerDetectionHelper
    {
        public static List<InterestingPoint> MatToPoints(Mat mat)
        {
            var list = new List<InterestingPoint>();
            for (var x = 0; x < mat.Width; x++)
                for (var y = 0; y < mat.Height; y++)
                    if (mat.GetAt(x,y) > 1e-6)
                    list.Add(new InterestingPoint(x, y, mat.GetAt(x, y)));

            return list;
        }


    }
}
