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
    class Transformations
    {

        // Преобразование из UInt32 в RGB
        public static RGB[,] FromUInt32ToRGB(UInt32[,] points)
        {
            RGB[,] RGBpixels = new RGB[points.GetLength(0), points.GetLength(1)]; ;

            for (int y = 0; y < points.GetLength(0); y++)
            {
                for (int x = 0; x < points.GetLength(1); x++)
                {
                    RGBpixels[y, x].R = (int)((points[y, x] & 0x00FF0000) >> 16);
                    RGBpixels[y, x].G = (int)((points[y, x] & 0x0000FF00) >> 8);
                    RGBpixels[y, x].B = (int) (points[y, x] & 0x000000FF);
                }
            }
            return RGBpixels;
        }

        public static RGB[,] BitmapToByteRgb(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            RGB[,] res = new RGB[height, width];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Color color = bmp.GetPixel(x, y);
                    res[y, x].R = color.R;
                    res[y, x].G = color.G;
                    res[y, x].B = color.B;
                }
            }
            return res;
        }

        //вычисление нового цвета
        public static RGB calculationOfColor(UInt32 pixel, double coefficient)
        {
            RGB Color = new RGB();
            Color.R = (int)(coefficient * ((pixel & 0x00FF0000) >> 16));
            Color.G = (int)(coefficient * ((pixel & 0x0000FF00) >> 8));
            Color.B = (int)(coefficient * (pixel & 0x000000FF));
            return Color;
        }

        //сборка каналов
        public static UInt32 build(RGB ColorOfPixel)
        {
            UInt32 Color;
            Color = 0xFF000000 | ((UInt32)ColorOfPixel.R << 16) | ((UInt32)ColorOfPixel.G << 8) | ((UInt32)ColorOfPixel.B);
            return Color;
        }

        //преобразование из UINT32 в Bitmap
        public Bitmap FromRgbToBitmap(RGB[,] pixels)
        {
            Bitmap bmp = new Bitmap(pixels.GetLength(1), pixels.GetLength(0));
            int height = pixels.GetLength(1);
            int width = pixels.GetLength(0);
           
            for (int y = 0; y < height; ++y)
            {
                UInt32 point;
                for (int x = 0; x < width; ++x)
                {
                    point = 0xFF000000 | ((UInt32)pixels[y, x].R << 16) | ((UInt32)pixels[y, x].G << 8) | ((UInt32)pixels[y, x].B);

                    bmp.SetPixel(y, x, Color.FromArgb((int)point));
                }
            }

            return bmp;
        }

        //преобразование из UINT32 to Bitmap
        public static Bitmap FromUInt32ToBitmap(UInt32[,] pixel, int Height, int Width)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    bmp.SetPixel(x, y, Color.FromArgb((int)pixel[y, x]));
            return bmp;
        }

        public static void ByteWriteFile(RGB[,] pixel, int H, int W)
        {
            StreamWriter sw = new StreamWriter("..\\..\\..\\..\\pass.txt");
            string s1 = " ";
            for (int y = 0; y < H; y++)
            {
                s1 = "";
                for (int x = 0; x < W; x++)
                {
                    s1 += "(" + pixel[y, x].R + " ";
                    s1 += pixel[y, x].G + " ";
                    s1 += pixel[y, x].B + ") " + " ";
                }
                s1 += "\n";
                sw.Write(s1);
            }
            s1 += "\n";
            sw.Close();
        }


    }
}
