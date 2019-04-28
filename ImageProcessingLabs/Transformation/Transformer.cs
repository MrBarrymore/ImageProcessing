using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageProcessingLabs.Transformation
{

    public class Transformer
    {
        public static Bitmap Transform(Bitmap img1, Bitmap img2, List<double> matrixA, List<double> matrixB)
        {
            int minX = 0,
                maxX = img1.Width,
                minY = 0,
                maxY = img1.Height;

            var points = new List<(double, double)>
            {
                Ransac.Transform(matrixB, 0, 0),
                Ransac.Transform(matrixB, img2.Width, 0),
                Ransac.Transform(matrixB, 0, img2.Height),
                Ransac.Transform(matrixB, img2.Width, img2.Height)
            };

            minX = (int) Math.Min(minX, points.Min(x => x.Item1));
            minY = (int) Math.Min(minY, points.Min(x => x.Item2));
            maxX = (int) Math.Max(maxX, points.Max(x => x.Item1));
            maxY = (int) Math.Max(maxY, points.Max(x => x.Item2));

            int width = maxX - minX,
                height = maxY - minY;
            int dx = -minX, dy = -minY;

            var bitmap = new Bitmap(width, height);
            var g = Graphics.FromImage(bitmap);

            g.DrawImage(img1, dx, dy, img1.Width, img1.Height);

            for (var x = 0; x < bitmap.Width; x++)
            for (var y = 0; y < bitmap.Height; y++)
            {
                var (nx, ny) = Ransac.Transform(matrixA, x - dx, y - dy);

                if (nx < 0 || ny < 0 || nx >= img2.Width || ny >= img2.Height) continue;
                bitmap.SetPixel(x, y, img2.GetPixel((int) nx, (int) ny));
            }

            return bitmap;
        }
    
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


