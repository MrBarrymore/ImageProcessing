using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Points
{
    class Harris
    {
        private static double[,] _pixels;

        public static List <InterestingPoint> DoHarris(double minValue, int windowSize, double[,] pixels)
        {
            _pixels = (double[,])pixels.Clone();
            _pixels = CommonMath.DoSobelSeparable(_pixels); // Считаем градиент в каждой точке 

            double[,] harrisMat = GetHarrisMatrix(windowSize, (int)_pixels.GetLength(0), (int)_pixels.GetLength(1)); // Полчаем матрицу откликов оператоа Харриса

            List<InterestingPoint> candidates =
               CommonMath.getCandidates(harrisMat, harrisMat.GetLength(0), harrisMat.GetLength(1)); // Находим точки локального максимума отклика оператора Харриса

            candidates = candidates.Where(x => x.probability > minValue).ToList();

            return candidates;
        }


        public static List<InterestingPoint> DoHarris(int windowSize, double[,] pixels, int test)
        {
            _pixels = (double[,])pixels.Clone();
            _pixels = CommonMath.DoSobelSeparable(_pixels); // Считаем градиент в каждой точке 

            double[,] harrisMat = GetHarrisMatrix(windowSize, (int)_pixels.GetLength(0), (int)_pixels.GetLength(1)); // Полчаем матрицу откликов оператоа Харриса
  
            List<InterestingPoint> candidates = new List<InterestingPoint>();

            for (int y = 0; y < harrisMat.GetLength(0); y++)
                for (int x = 0; x < harrisMat.GetLength(1); x++)
                {
                    double currentValue = harrisMat[y, x];
                    candidates.Add(new InterestingPoint(y, x, currentValue));
                }

            if (test == 1)
                candidates =  CommonMath.getCandidates(harrisMat, harrisMat.GetLength(0), harrisMat.GetLength(1));

            return candidates;
        }


        public static double[,] GetHarrisMatrix(int windowSize, int width, int height)
        {
            double[,] harrisMat = new double[width, height];

            double[,] SobelX = CommonMath.GetSobelX(_pixels);
            double[,] SobelY = CommonMath.GetSobelY(_pixels);

            for (int y = 0; y < _pixels.GetLength(0); y++)
            {
                for (int x = 0; x < _pixels.GetLength(1); x++)
                {
                    double[,] mainWindow = GetMainWindow(windowSize, y, x); // Формируем исходное окно                  

                    double[,] gauss = ConvolutionMatrix.CountGaussMatrix(0.6);

                    // Считаем матрицу H
                    double[,] currentMatrix = new double[2, 2];
                    for (int u = -(windowSize - 1); u <= (windowSize - 1); u++)
                    {
                        for (int v = -(windowSize - 1); v <= (windowSize - 1); v++)
                        {
                            double Ix = CommonMath.getPixel(SobelX, y + v, x + u, 3);
                            double Iy = CommonMath.getPixel(SobelY, y + v, x + u, 3);
                        
                            double gaussPoint = CommonMath.getPixel(gauss, u + (windowSize - 1), 0, 3) * CommonMath.getPixel(gauss, u + (windowSize - 1), v + (windowSize - 1), 3);
                        
                            currentMatrix[0, 0] += Math.Pow(Ix, 2) * gaussPoint;
                            currentMatrix[0, 1] += Ix * Iy * gaussPoint;
                            currentMatrix[1, 0] += Ix * Iy * gaussPoint;
                            currentMatrix[1, 1] += Math.Pow(Iy, 2) * gaussPoint;
                        }
                    }

                    double[] eigenvalues = getEigenvalues(currentMatrix);
                    harrisMat[y, x] = Math.Min(eigenvalues[0], eigenvalues[1]);
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

        public static double[,] GetMainWindow(int windowSize, int y, int x)
        {
            double[,] mainWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                    mainWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy, x + wx, 3);

            return mainWindow;
        }

    }
}
