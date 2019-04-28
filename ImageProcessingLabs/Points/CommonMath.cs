using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageProcessingLabs.Wrapped;
using ImageProcessingLabs.enums;

namespace ImageProcessingLabs.Points
{
    class CommonMath
    {
        // Задание Оператора Собеля для сепорабельных вычислений
        public static double[,] SubelSepX1 = new double[1, 3] { { 1, 2, 1 } };
        public static double[,] SubelSepX2 = new double[3, 1] { { 1 }, { 0 }, { -1 } };
        public static double[,] SubelSepY1 = new double[1, 3] { { 1, 0, -1 } };
        public static double[,] SubelSepY2 = new double[3, 1] { { 1 }, { 2 }, { 1 } };

        public static WrappedImage GetSobelX(WrappedImage image, BorderHandling borderHandling)
        {
            // Считаем частную производную по X (сепарабельно)
            WrappedImage pixelX = ConvolutionMatrixFactory.processNonSeparable(image, SubelSepX1, borderHandling);
            pixelX = ConvolutionMatrixFactory.processNonSeparable(pixelX, SubelSepX2, borderHandling);
            return pixelX;
        }

        public static WrappedImage GetSobelY(WrappedImage image, BorderHandling borderHandling)
        {
            // Считаем частную производную по Y (сепарабельно)
            WrappedImage pixelY = ConvolutionMatrixFactory.processNonSeparable(image, SubelSepY1, borderHandling);
            pixelY = ConvolutionMatrixFactory.processNonSeparable(pixelY, SubelSepY2, borderHandling);
            return pixelY;
        }

        public static WrappedImage DoSobelSeparable(WrappedImage image)
        {
            // Считаем частную производную по X (сепарабельно)
            WrappedImage pixelX = ConvolutionMatrixFactory.processNonSeparable(image, SubelSepX1, BorderHandling.Mirror);
            pixelX = ConvolutionMatrixFactory.processNonSeparable(pixelX, SubelSepX2, BorderHandling.Mirror);
            // Считаем частную производную по Y (сепарабельно)
            WrappedImage pixelY = ConvolutionMatrixFactory.processNonSeparable(image, SubelSepY1, BorderHandling.Mirror);
            pixelY = ConvolutionMatrixFactory.processNonSeparable(pixelY, SubelSepY2, BorderHandling.Mirror);

            // Вычисляем величину градиента
            for (int y = 0; y < image.height; y++)
            {
                for (int x = 0; x < image.width; x++)
                {
                    image.buffer[y, x] = Math.Sqrt(Math.Pow(pixelX.buffer[y, x], 2) + Math.Pow(pixelY.buffer[y, x], 2));
                }
            }
            return image;
        }


        public static double getPixel(double[,] _pixels, int y, int x, int borderHandling)
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


        public static List<InterestingPoint> getCandidates(WrappedImage responseMatrix, int height, int width, int radius, double minValue)
        {
            List<InterestingPoint> candidates = new List<InterestingPoint>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool ok = true;
                    double currentValue = responseMatrix.buffer[y, x];

                    if (responseMatrix.buffer[y, x] >= minValue)
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
                                if (currentValue < WrappedImage.getPixel(responseMatrix, y + k, x + n, BorderHandling.Mirror))
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


        public static List<InterestingPoint> getCandidates(Mat responseMatrix, int height, int width, int radius, double minValue)
        {
            List<InterestingPoint> candidates = new List<InterestingPoint>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool ok = true;
                    double currentValue = responseMatrix.GetAt(x, y);

                    if (responseMatrix.GetAt(x,y) >= minValue)
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
                                if (currentValue < WrappedImage.getPixel(responseMatrix, y + k, x + n, BorderHandling.Mirror))
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
