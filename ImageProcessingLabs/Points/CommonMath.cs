using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageProcessingLabs.Points
{
    class CommonMath
    {
        // Задание Оператора Собеля для сепорабельных вычислений
        public static double[,] SubelSepX1 = new double[1, 3] { { 1, 2, 1 } };
        public static double[,] SubelSepX2 = new double[3, 1] { { 1 }, { 0 }, { -1 } };
        public static double[,] SubelSepY1 = new double[1, 3] { { 1, 0, -1 } };
        public static double[,] SubelSepY2 = new double[3, 1] { { 1 }, { 2 }, { 1 } };


        private static int[] dx = { -1, 0, 1, -1, 1, -1, 0, -1 };
        private static int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

        public static double[,] DoSobelSeparable(double[,] pixels)
        {
            // Считаем частную производную по X (сепарабельно)
            double[,] pixelX = ConvolutionMatrixFactory.processNonSeparable(pixels, SubelSepX1, 3);
            pixelX = ConvolutionMatrixFactory.processNonSeparable(pixelX, SubelSepX2, 3);
            // Считаем частную производную по Y (сепарабельно)
            double[,] pixelY = ConvolutionMatrixFactory.processNonSeparable(pixels, SubelSepY1, 3);
            pixelY = ConvolutionMatrixFactory.processNonSeparable(pixelY, SubelSepY2, 3);

            // Вычисляем величину градиента
            for (int y = 0; y < pixels.GetLength(0); y++)
            {
                for (int x = 0; x < pixels.GetLength(1); x++)
                {
                    pixels[y, x] = Math.Sqrt(Math.Pow(pixelX[y, x], 2) + Math.Pow(pixelY[y, x], 2));
                }
            }
            return pixels;
        }

        public static double[,] GetSobelX(double[,] pixels)
        {
            // Считаем частную производную по X (сепарабельно)
            double[,] pixelX = ConvolutionMatrixFactory.processNonSeparable(pixels, SubelSepX1, 3);
            pixelX = ConvolutionMatrixFactory.processNonSeparable(pixelX, SubelSepX2, 3);
            return pixelX;
        }

        public static double[,] GetSobelY(double[,] pixels)
        {
            // Считаем частную производную по Y (сепарабельно)
            double[,] pixelY = ConvolutionMatrixFactory.processNonSeparable(pixels, SubelSepY1, 3);
            pixelY = ConvolutionMatrixFactory.processNonSeparable(pixelY, SubelSepY2, 3);
            return pixelY;
        }

        public static double getPixel(double [,] _pixels, int y, int x, int borderHandling)
        {

            switch (borderHandling)
            {
                case 0:
                    if (x < 0 || x >= _pixels.GetLength(1) || y < 0 || y >= _pixels.GetLength(0))
                        return 0;
                    return _pixels[y, x];
                case 1:
                    if (x < 0 || x >= _pixels.GetLength(1) || y < 0 || y >= _pixels.GetLength(0))
                        return 255;
                    return _pixels[y, x];
                case 2:
                    x = border(0, x, _pixels.GetLength(1) - 1);
                    y = border(y, 0, _pixels.GetLength(0) - 1);
                    return _pixels[y, x];
                case 3:
                    x = (x + _pixels.GetLength(1)) % _pixels.GetLength(1);
                    y = (y + _pixels.GetLength(0)) % _pixels.GetLength(0);
                    return _pixels[y, x];
                case 4:
                    x = Math.Abs(x);
                    y = Math.Abs(y);
                    if (x >= _pixels.GetLength(1)) x = _pixels.GetLength(1) - (x - _pixels.GetLength(1) + 1);
                    if (y >= _pixels.GetLength(0)) y = _pixels.GetLength(0) - (y - _pixels.GetLength(0) + 1);
                    return _pixels[y, x];
                default:
                    return 255;
            }
        }

        private static int border(int y, int x, int Length)
        {
            int res = 0;

            if (x > Length) res = Length;
            else if (x < 0) res = 0;
            else if (y > Length) res = Length;
            else if (y < 0) res = 0;
            else
            {
                if (x != 0) res = x;
                if (y != 0) res = y;
            }
            return res;
        }

        public static List<InterestingPoint> getCandidates(double[,] harrisMatrix, int width, int height)
        {
            List<InterestingPoint> candidates = new List<InterestingPoint>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bool ok = true;
                    double currentValue = harrisMatrix[i, j];
                    for (int k = 0; k < dx.GetLength(0) && ok; k++)
                    {
                        if (i + dx[k] < 0 ||
                            i + dx[k] >= width ||
                            j + dy[k] < 0 ||
                            j + dy[k] >= height) continue;
                        if (currentValue < CommonMath.getPixel(harrisMatrix, i + dx[k], j + dy[k], 3))
                            ok = false;
                    }
                    if (ok)
                    {
                        candidates.Add(new InterestingPoint(i, j, currentValue));
                    }
                }
            }
            return candidates;
        }

    }
}
