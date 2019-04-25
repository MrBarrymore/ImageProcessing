using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs
{
    class ConvolutionMatrix
    {
        private static double[,] CountGaussMatrix(int radius, double sigma)
        {
            double[,] gaussMatrix = new double[2 * radius + 1, 2 * radius + 1];

            double coef = 1 / (2 * Math.PI * sigma * sigma);
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    gaussMatrix[x + radius, y + radius] = coef * Math.Exp(-(x * x + y * y) / (2 * sigma * sigma));
                }
            }
            return gaussMatrix;
        }

        public static double[,] CountGaussMatrix(double sigma)
        {
            return CountGaussMatrix((int)Math.Ceiling(sigma) * 3, sigma);
        }

        public static double[,] CountGaussMatrix(int radius)
        {
            return CountGaussMatrix(radius, radius / 3);
        }

    }
}
