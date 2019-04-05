using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ImageProcessingLabs
{
    class ConvolutionMatrixFactory
    {
        public static double[,] _pixels;
    
        public List<double[,]> picturePiramid = new List<double[,]>();

        double[,] Pyramidpixels;

        public ConvolutionMatrixFactory()
        {

        }

        public ConvolutionMatrixFactory(double[] pixel)
        {

        }

        public static double[,] processNonSeparable(double[,] pixels, double[,] matryx, int Edgemode)
        {
            double[,] newpixels = new double[pixels.GetLength(0), pixels.GetLength(1)];
            _pixels = pixels;
            //применение ядра свертки                      
            for (int y = 0; y < pixels.GetLength(0); y++)
                for (int x = 0; x < pixels.GetLength(1); x++)
                {
                    newpixels[y, x] = GetNewPixel(y, x, pixels, matryx, Edgemode);
                }

            return newpixels;
        }


        public static double GetNewPixel(int cy, int cx, double[,] pixels, double[,] svMatrix, int Edgemode)
        {
            double newpixel = 0;
           
            int ky = (int)(svMatrix.GetLength(0) / 2); // y 
            int kx = (int)(svMatrix.GetLength(1) / 2); // x

            for (int y = cy - ky, i1 = 0; y <= cy + ky; y++, i1++)
            { 
                int y1 = y;
                for (int x = cx - kx, j1 = 0; x <= cx + kx; x++, j1++)
                {
                    int x1 = x;

                    double bufpixel = 0;
                    // Обработка краевых эффектов
                    if (y < 0 || y >= pixels.GetLength(0)) bufpixel = getOutPixel(y, x, Edgemode);
                    else if (x < 0 || x >= pixels.GetLength(1)) bufpixel = getOutPixel(y, x, Edgemode);
                    else bufpixel = pixels[y1, x1];

                    newpixel += bufpixel * svMatrix[i1, j1];
                }
            }
            return newpixel;
        }

        private static double getOutPixel(int y, int x, int Edgemode)
        {
            switch (Edgemode)
            {
                case 0:
                    if (x < 0 || x >= _pixels.GetLength(1) || y < 0 || y >= _pixels.GetLength(0))
                        return 0;
                    return _pixels[y, x];
                case 1:
                    if (x < 0 || x >= _pixels.GetLength(1) || y < 0 || y >= _pixels.GetLength(1)) 
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

        private static int border(int y, int x, object p)
        {
            if (y == 0) return 0;

            
            return 0;
        }

    }
}
