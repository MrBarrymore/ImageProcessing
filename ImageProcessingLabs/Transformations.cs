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

namespace ImageProcessingLabs
{
    class Transformations
    {
        //вычисление нового цвета
        public static double calculationOfColor(double pixel, double coefficient)
        {
            return coefficient * pixel;
        }

        //преобразование из UINT32 to Bitmap
        public static Bitmap FromUInt32ToBitmap(double[,] pixel)
        {
            int Height = pixel.GetLength(0);
            int Width = pixel.GetLength(1);

            double min = double.MaxValue, max = double.MinValue; 
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    min = Math.Min(min, pixel[y, x]);
                    max = Math.Max(max, pixel[y, x]);
                }

            Bitmap bmp = new Bitmap(Width, Height);
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    double color = ((pixel[y, x] - min) * 255) / (max - min);
                    bmp.SetPixel(x, y, Color.FromArgb((int)color, (int)color, (int)color));
                }

            return bmp;
        }

    }
}
