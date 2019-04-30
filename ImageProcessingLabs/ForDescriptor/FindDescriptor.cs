using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.enums;
using ImageProcessingLabs.Points;

namespace ImageProcessingLabs.ForDescriptor
{
    public class FindDescriptor
    {

        private static int BINS_COUNT = 8;
        private static int GRID_SIZE = 4;
        private static int BLOCK_SIZE = 4;

        public static List<Descriptor> Calculate(Mat image, List<InterestingPoint> points)
        {
            var dx = new Mat(image.Width, image.Height);
            var dy = new Mat(image.Width, image.Height);
            var gradient = SobelHelper.Sobel(image, dx, dy, BorderWrapType.Mirror);

            var descriptors = new List<Descriptor>();
            var step = 2 * Math.PI / BINS_COUNT;

            foreach (var center in points)
            {
                var bins = new double[BINS_COUNT];

                int from = GRID_SIZE * BLOCK_SIZE / 2,
                    to = GRID_SIZE * BLOCK_SIZE - from;

                for (var u = -from; u < to; u++)
                {
                    for (var v = -from; v < to; v++)
                    {
                        int x = center.getX() + u,
                            y = center.getY() + v;

                        var theta = Math.Atan2(dy.GetPixel(x, y, BorderWrapType.Mirror), dx.GetPixel(x, y, BorderWrapType.Mirror)) + Math.PI;
                        var magnitude = gradient.GetPixel(x, y, BorderWrapType.Mirror);

                        var leftBin = Math.Min((int)Math.Floor(theta / step), BINS_COUNT - 1);
                        var rightBin = (leftBin + 1) % BINS_COUNT;

                        var ratio = theta % step / step;
                        bins[leftBin] += magnitude * (1 - ratio);
                        bins[rightBin] += magnitude * ratio;
                    }
                }

                var vector = new Vector(BINS_COUNT, bins).Normalize();

                descriptors.Add(new Descriptor
                {
                    Point = center,
                    Vector = vector
                });
            }

            return descriptors;
        }

        public static Vector CalculateForPointWithRotation(Mat gradient, Mat dx, Mat dy, Mat gauss,
            int gridSize, int blockSize, int binsCount, InterestingPoint center, double alpha)
        {
            var gaussK = gauss.Width / 2;
            var bins = new Dictionary<ValueTuple<int, int>, List<double>>();
            var step = 2 * Math.PI / binsCount;
            int from = gridSize * blockSize / 2, to = gridSize * blockSize - from;

            double cos = Math.Cos(-alpha),
                sin = Math.Sin(-alpha);

            for (var u = -from; u < to; u++)
            {
                for (var v = -from; v < to; v++)
                {
                    int x = center.getX() + u,
                        y = center.getY() + v;

                    var magnitude = gradient.GetPixel(x, y, BorderWrapType.Mirror);
                    var theta = Math.Atan2(dy.GetPixel(x, y, BorderWrapType.Mirror), dx.GetPixel(x, y, BorderWrapType.Mirror));
                    if (theta < 0) theta += Math.PI * 2;

                    var rotatedU = (int)Math.Round(u * cos + v * sin);
                    var rotatedV = (int)Math.Round(v * cos - u * sin);

                    var row = (rotatedV + from) / blockSize;
                    var column = (rotatedU + from) / blockSize;

                    if (column < 0 || column >= gridSize || row < 0 || row >= gridSize) continue;

                    magnitude *= gauss.GetAt(rotatedU + gaussK, rotatedV + gaussK);

                    var rotatedTheta = theta + alpha;
                    if (rotatedTheta > Math.PI * 2) rotatedTheta -= Math.PI * 2;

                    var ratio = rotatedTheta % step / step;
                    var leftBin = Math.Min((int)Math.Floor(rotatedTheta / step), binsCount - 1);
                    var rightBin = (leftBin + 1) % binsCount;

                    if (!bins.ContainsKey((row, column)))
                        bins.Add((row, column), Enumerable.Repeat(0D, binsCount).ToList());

                    bins[(row, column)][leftBin] += magnitude * (1 - ratio);
                    bins[(row, column)][rightBin] += magnitude * ratio;
                }
            }

            var result = new Vector(gridSize * gridSize * binsCount);

            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    for (var k = 0; k < binsCount; k++)
                    {
                        result.Set(i * gridSize * binsCount + j * binsCount + k, bins[(i, j)][k]);
                    }
                }
            }

            return result.Normalize();
        }

    }
}
