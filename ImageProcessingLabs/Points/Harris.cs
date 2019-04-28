using System;
using System.Collections.Generic;
using System.Linq;
using ImageProcessingLabs.Wrapped;

namespace ImageProcessingLabs.Points
{
    class Harris
    {
        private static WrappedImage _image;

        public static List<InterestingPoint> DoHarris(double minValue, int windowSize, WrappedImage image)
        {
            int radius = (int)(windowSize / 2);

            _image = image.Clone();

            // Полчаем матрицу откликов оператоа Харриса
            /*
            var harrisMat = GetHarrisMatrix(windowSize,
                radius,
                image.width,
                image.height
            );
            */

            Mat testMat = ConvertImageToMat(image);

            var harrisMat = Harris2.Find(testMat, windowSize, minValue);


            // Находим точки локального максимума отклика оператора Харриса
            var candidates = CommonMath.getCandidates(harrisMat,
                harrisMat.Height,
                harrisMat.Width,
                radius,
                minValue
            );

            return candidates;
        }


        public static Mat ConvertImageToMat(WrappedImage image)
        {
           Mat mat = new Mat(image.width, image.height);


           for (var x = 0; x < image.height; x++)
           for (var y = 0; y < image.width; y++)
                    mat.Set(y, x,image.buffer[x,y]); 

           return mat;
        }


        private static WrappedImage Normalization(WrappedImage source, double newMin, double newMax)
        {
            var result = new WrappedImage(source.height, source.width);

            double min = source.buffer[0, 0], max = source.buffer[0, 0];
            foreach (var value in source.buffer)
            {
                if (double.IsNaN(value))
                    continue;

                min = Math.Min(min, value);
                max = Math.Max(max, value);
            }

            for (var i = 0; i < source.height; i++)
                for (var j = 0; j < source.width; j++)
                {
                    result.buffer[i, j] = (source.buffer[i, j] - min) * (newMax - newMin) / (max - min) + newMin;
                }

            return result;
        }

        public static WrappedImage GetHarrisMatrix(int windowSize, int windowRadius, int width, int height)
        {
            WrappedImage harrisMat = new WrappedImage(height, width);

            WrappedImage SobelX = CommonMath.GetSobelX(_image, BorderHandling.Mirror);
            WrappedImage SobelY = CommonMath.GetSobelY(_image, BorderHandling.Mirror);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double[,] gauss = ConvolutionMatrix.CountGaussMatrix(windowSize);

                    // Считаем матрицу H
                    double[,] currentMatrix = new double[2, 2];
                    for (int u = -windowRadius; u <= windowRadius; u++)
                    {
                        for (int v = -windowRadius; v <= windowRadius; v++)
                        {
                            double Ix = WrappedImage.getPixel(SobelX, y + u, x + v, BorderHandling.Mirror);
                            double Iy = WrappedImage.getPixel(SobelY, y + u, x + v, BorderHandling.Mirror);

                            double gaussPoint = CommonMath.getPixel(gauss, u + windowRadius, v + windowRadius, 3);

                            currentMatrix[0, 0] += Math.Pow(Ix, 2) * gaussPoint;
                            currentMatrix[0, 1] += Ix * Iy * gaussPoint;
                            currentMatrix[1, 0] += Ix * Iy * gaussPoint;
                            currentMatrix[1, 1] += Math.Pow(Iy, 2) * gaussPoint;
                        }
                    }

                    double[] eigenvalues = getEigenvalues(currentMatrix);
                    harrisMat.buffer[y, x] = Math.Min(eigenvalues[0], eigenvalues[1]);
                }
            }

            return harrisMat;
        }

        static public double[] getEigenvalues(double[,] matrix) // Считаем собственные числа 
        {
            double[] eigenvalues = new double[2];

            double a = 1;
            double b = -matrix[0, 0] - matrix[1, 1];
            double c = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            double d = Math.Pow(b, 2) - 4 * a * c;
            if (Math.Abs(d) < 1e-4)
                d = 0;
            if (d < 0)
            {
                return eigenvalues;
            }

            eigenvalues[0] = (-b + Math.Sqrt(d)) / (2 * a);
            eigenvalues[1] = (-b - Math.Sqrt(d)) / (2 * a);

            return eigenvalues;
        }
    }

}
