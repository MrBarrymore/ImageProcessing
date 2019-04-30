using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Wrapped;


namespace ImageProcessingLabs
{
    public enum BorderHandling
    {
        Black, White, Copy, Wrap, Mirror
    }

    class ConvolutionMatrixFactory
    {
        public static double[,] _pixels;

        public List<double[,]> picturePiramid = new List<double[,]>();


        public static WrappedImage processNonSeparable(WrappedImage image, double[,] matryx, BorderHandling borderHandling)
        {
            WrappedImage newImage = new WrappedImage(image);
            //применение ядра свертки                      
            for (int y = 0; y < image.height; y++)
                for (int x = 0; x < image.width; x++)
                {
                    newImage.setPixel(y, x, GetNewPixel(y, x, image, matryx, borderHandling));
                }

            return newImage;
        }

        public static double GetNewPixel(int cy, int cx, WrappedImage source, double[,] svMatrix, BorderHandling borderHandling)
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
                    if (y < 0 || y >= source.height) bufpixel = WrappedImage.getPixel(source, y, x, borderHandling);
                    else if (x < 0 || x >= source.width) bufpixel = WrappedImage.getPixel(source, y, x, borderHandling);
                    else bufpixel = source.buffer[y1, x1];

                    newpixel += bufpixel * svMatrix[i1, j1];
                }
            }
            return newpixel;
        }

    }
}
