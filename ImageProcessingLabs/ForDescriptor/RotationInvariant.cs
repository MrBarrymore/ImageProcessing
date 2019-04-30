using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.enums;

namespace ImageProcessingLabs.ForDescriptor
{
    public static class RotationInvariant
    {

        private const int GridSize = 4;
        private const int BlockSize = 4;
        private const int BinsCount = 36;

        public static List<Descriptor> Calculate(Mat image, List<InterestingPoint> points)
        {
            var dx = new Mat(image.Width, image.Height);
            var dy = new Mat(image.Width, image.Height);
            var gradient = SobelHelper.Sobel(image, dx, dy, BorderWrapType.Mirror);

            var descriptors = new List<Descriptor>();
            var gauss = Gauss.GetFullKernel(GridSize * BlockSize / 3D);
            var step = 2 * Math.PI / BinsCount;

            foreach (var point in points)
            {
                var full = FindDescriptor.CalculateForPointWithRotation(gradient, dx, dy, gauss,
                    1, GridSize * BlockSize, BinsCount, point, 0);

                var peaks = FindPeaks(full);

                var angles = new List<double> { 2 * Math.PI - peaks[0] * step };
                if (peaks.Count == 2)
                {
                    if (full.Get(peaks[1]) / full.Get(peaks[0]) > 0.8)
                        angles.Add(2 * Math.PI - peaks[1] * step);
                }

                point.Angles = angles;

                foreach (var angle in angles)
                {
                    descriptors.Add(new Descriptor
                    {
                        Point = point,
                        Vector = FindDescriptor.CalculateForPointWithRotation(gradient, dx, dy, gauss,
                            GridSize, BlockSize, BinsCount, point, angle)
                    });
                }
            }

            return descriptors;
        }

        private static List<int> FindPeaks(Mat vector)
        {
            var a = vector.GetData();

            var maxList = new List<ValueTuple<double, int>>();
            for (var i = 1; i < a.Length - 1; i++)
            {
                if (a[i] > a[i + 1] && a[i] > a[i - 1])
                {
                    maxList.Add(new ValueTuple<double, int>(a[i], i));
                }
            }

            maxList.Sort();
            maxList.Reverse();

            return maxList.Take(2).Select(x => x.Item2).ToList();
        }

    }
}
