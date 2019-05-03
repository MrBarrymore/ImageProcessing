using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageProcessingLabs.Descriptor;

namespace ImageProcessingLabs.Transformation
{

    public class Transformer
    {
        //преобразование из Mat to Bitmap
        public static Bitmap FromUInt32ToBitmap(Mat data)
        {
            int Width = data.Width;
            int Height = data.Height;

            double min = double.MaxValue, max = double.MinValue;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y <  Height; y++)
                {
                    min = Math.Min(min, data.GetAt(x,y));
                    max = Math.Max(max, data.GetAt(x, y));
                }

            Bitmap bmp = new Bitmap(Width, Height);
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    double color = ((data.GetAt(x, y) - min) * 255) / (max - min);
                    bmp.SetPixel(x, y, Color.FromArgb((int)color, (int)color, (int)color));
                }

            return bmp;
        }
    }
}   


