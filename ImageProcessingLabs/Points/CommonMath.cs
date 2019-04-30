using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageProcessingLabs.enums;

namespace ImageProcessingLabs.Points
{
    class CommonMath
    {
        public static List<InterestingPoint> getCandidates(Mat responseMatrix, int height, int width, int radius, double minValue)
        {
            List<InterestingPoint> candidates = new List<InterestingPoint>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool ok = true;
                    double currentValue = responseMatrix.GetAt(x,y);

                    if (responseMatrix.GetAt(x, y) >= minValue)
                    {
                        for (int k = -radius; k <= radius && ok; k++)
                        {
                            for (int n = -radius; n <= radius && ok; n++)
                            {
                                if (y + k < 0 ||
                                    y + k >= width ||
                                    x + n < 0 ||
                                    x + n >= height ||
                                    (k == 0 && n == 0)) continue;
                                if (currentValue < responseMatrix.GetPixel( y + k, x + n, BorderWrapType.Mirror))
                                    ok = false;
                            }
                        }

                        if (ok)
                        {
                            candidates.Add(new InterestingPoint(y, x, currentValue));
                        }
                    }

                }
            }
            return candidates;
        }


    }
}
