using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageProcessingLaba1
{

    class Filters
    {
        public static double[,] matrix_filtration(double[,] pixel, double[,] matryx, int Edgemode)
        {
            int i, j;
            int W = pixel.GetLength(1);
            int H = pixel.GetLength(0);
            int gapX = matryx.GetLength(1);
            int gapY = matryx.GetLength(0);
            
            int tmpH = H + 2 * gapY, tmpW = W + 2 * gapX;
            double[,] tmppixel = new double[tmpH, tmpW];
            double[,] newpixel = new double[H, W];

            //заполнение временного расширенного изображения

            if (Edgemode == 0) tmppixel = OutsideZero(pixel, gapX, gapY);
            else if (Edgemode == 1) tmppixel = EdgeCoppy(pixel, gapX, gapY);
            else if(Edgemode == 2) tmppixel = EdgeReflection(pixel, gapX, gapY);
            else if(Edgemode == 3) tmppixel = WrapImage(pixel, gapX, gapY); 
            else { EdgeCoppy(pixel, gapX, gapY); }

            //применение ядра свертки
            double GrayPixel = 0;
                    
            for (i = gapY; i < tmpH - gapY; i++)
                for (j = gapX; j < tmpW - gapX; j++)
                {
                    GrayPixel = 0;
                    for (int k = 0; k < matryx.GetLength(0); k++)
                        for (int m = 0; m < matryx.GetLength(1); m++)
                        {
                            GrayPixel += Transformations.calculationOfColor(tmppixel[i + k - (int)(gapY/2), j + m - (int)(gapX / 2)], matryx[k, m]);
                        }

                    newpixel[i - gapY, j - gapX] = GrayPixel;
                }

            return newpixel;
        }

        public static double[,] OutsideZero(double [,] pixel, int gapX, int gapY) {
            int i, j;
            int W = pixel.GetLength(1);
            int H = pixel.GetLength(0);

            int tmpH = H + 2 * gapY, tmpW = W + 2 * gapX;
            double[,] tmppixel = new double[tmpH, tmpW];

            for (i = 0; i < gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = 0;
                    tmppixel[i, tmpW - 1 - j] = 0;
                    tmppixel[tmpH - 1 - i, j] = 0;
                    tmppixel[tmpH - 1 - i, tmpW - 1 - j] = 0;
                }

            //крайние левая и правая стороны
            for (i = gapY; i < tmpH - gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = 0;
                    tmppixel[i, tmpW - 1 - j] = 0;
                }

            //крайние верхняя и нижняя стороны
            for (i = 0; i < gapY; i++)
                for (j = gapX; j < tmpW - gapX; j++)
                {
                    tmppixel[i, j] = 0;
                    tmppixel[tmpH - 1 - i, j] = 0;
                }

            //центр
            for (i = 0; i < H; i++)
                for (j = 0; j < W; j++)
                    tmppixel[i + gapY, j + gapX] = pixel[i, j];

            return tmppixel;
        }

        public static double[,] EdgeCoppy(double[,] pixel, int gapX, int gapY)
        {
            int i, j;
            int W = pixel.GetLength(1);
            int H = pixel.GetLength(0);

            int tmpH = H + 2 * gapY, tmpW = W + 2 * gapX;
            double[,] tmppixel = new double[tmpH, tmpW];


            for (i = 0; i < gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = pixel[0, 0];
                    tmppixel[i, tmpW - 1 - j] = pixel[0, W - 1];
                    tmppixel[tmpH - 1 - i, j] = pixel[H - 1, 0];
                    tmppixel[tmpH - 1 - i, tmpW - 1 - j] = pixel[H - 1, W - 1];
                }

            //крайние левая и правая стороны
            for (i = gapY; i < tmpH - gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = pixel[i - gapY, j];
                    tmppixel[i, tmpW - 1 - j] = pixel[i - gapY, W - 1 - j];
                }

            //крайние верхняя и нижняя стороны
            for (i = 0; i < gapY; i++)
                for (j = gapX; j < tmpW - gapX; j++)
                {
                    tmppixel[i, j] = pixel[i, j - gapX];
                    tmppixel[tmpH - 1 - i, j] = pixel[H - 1 - i, j - gapX];
                }

            //центр
            for (i = 0; i < H; i++)
                for (j = 0; j < W; j++)
                    tmppixel[i + gapY, j + gapX] = pixel[i, j];

            return tmppixel;
        }

        public static double[,] EdgeReflection(double[,] pixel, int gapX, int gapY)
        {
            int i, j;
            int W = pixel.GetLength(1);
            int H = pixel.GetLength(0);

            int tmpH = H + 2 * gapY, tmpW = W + 2 * gapX;
            double[,] tmppixel = new double[tmpH, tmpW];


            for (i = 0; i < gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = pixel[0, 0];
                    tmppixel[i, tmpW - 1 - j] = pixel[0, W - 1];
                    tmppixel[tmpH - 1 - i, j] = pixel[H - 1, 0];
                    tmppixel[tmpH - 1 - i, tmpW - 1 - j] = pixel[H - 1, W - 1];
                }

            //крайние левая и правая стороны
            for (i = gapY; i < tmpH - gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = pixel[i - gapY, j];
                    tmppixel[i, tmpW - 1 - j] = pixel[i - gapY, W - 1 - j];
                }

            //крайние верхняя и нижняя стороны
            for (i = 0; i < gapY; i++)
                for (j = gapX; j < tmpW - gapX; j++)
                {
                    tmppixel[i, j] = pixel[i, j - gapX];
                    tmppixel[tmpH - 1 - i, j] = pixel[H - 1 - i, j - gapX];
                }

            //центр
            for (i = 0; i < H; i++)
                for (j = 0; j < W; j++)
                    tmppixel[i + gapY, j + gapX] = pixel[i, j];

            return tmppixel;
        }

        public static double[,] WrapImage(double[,] pixel, int gapX, int gapY)
        {
            int i, j;
            int W = pixel.GetLength(1);
            int H = pixel.GetLength(0);

            int tmpH = H + 2 * gapY, tmpW = W + 2 * gapX;
            double[,] tmppixel = new double[tmpH, tmpW];

            // Углы
            for (i = 0; i < gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = pixel[0, W - 1];
                    tmppixel[i, tmpW - 1 - j] = pixel[0, 0];
                    tmppixel[tmpH - 1 - i, j] = pixel[H - 1, W - 1];
                    tmppixel[tmpH - 1 - i, tmpW - 1 - j] = pixel[H - 1, 0];
                }

            //крайние левая и правая стороны
            for (i = gapY; i < tmpH - gapY; i++)
                for (j = 0; j < gapX; j++)
                {
                    tmppixel[i, j] = pixel[i - gapY, W - gapX];
                    tmppixel[i, tmpW - 1 - j] = pixel[i - gapY, j];
                }

            //крайние верхняя и нижняя стороны
            for (i = 0; i < gapY; i++)
                for (j = gapX; j < tmpW - gapX; j++)
                {
                    tmppixel[i, j] = pixel[H - 1 - i, j - gapX];
                    tmppixel[tmpH - 1 - i, j] = pixel[i, j - gapX];
                }

            //центр
            for (i = 0; i < H; i++)
                for (j = 0; j < W; j++)
                    tmppixel[i + gapY, j + gapX] = pixel[i, j];

            return tmppixel;
        }

    }
}
