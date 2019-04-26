using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Wrapped
{
    public interface ICloneable<T>
    {
        T Clone();
    }

    public class WrappedImage : ICloneable<WrappedImage>
    {
        private static double BLACK = 0;
        private static double WHITE = 1;

        public int height;
        public int width;
        public double[,] buffer;

        public WrappedImage()
        {

        }

        public WrappedImage(WrappedImage wrappedImage)
        {
            width = wrappedImage.width;
            height = wrappedImage.height;
            buffer = new double[height, width];
            System.Array.Copy(wrappedImage.buffer, 0, buffer, 0, buffer.Length);
        }

        public WrappedImage(int height, int width)
        {
            if (width < 0 || height < 0)
                throw new Exception("Размер не может быть отрицательным");
            this.width = width;
            this.height = height;
            this.buffer = new double[height, width];
        }

        public static WrappedImage of(Bitmap image)
        {
            WrappedImage wrappedImage = new WrappedImage();
            wrappedImage.width = image.Width;
            wrappedImage.height = image.Height;
            wrappedImage.buffer = new double[wrappedImage.height, wrappedImage.width];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color color = image.GetPixel(x, y);
                    double gray = 0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B;
                    wrappedImage.setPixel(y, x, gray / 255);
                }
            }
            return wrappedImage;
        }

        public static WrappedImage of(double[,] buffer)
        {
            WrappedImage wrappedImage = new WrappedImage();
            wrappedImage.height = buffer.GetLength(0);
            wrappedImage.width = buffer.GetLength(1);

            wrappedImage.buffer = new double[wrappedImage.height, wrappedImage.width];

            for (int i = 0; i < wrappedImage.height; i++)
            {
                for (int j = 0; j < wrappedImage.width; j++)
                {
                    wrappedImage.setPixel(i, j, buffer[i, j]);
                }
            }
            return wrappedImage;
        }

        public WrappedImage Clone()
        {
            return new WrappedImage
            {
                width = this.width,
                height = this.height,
                buffer = (double[,])this.buffer.Clone()
            };
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
                    double color = ((pixel[y, x] - min)) / (max - min);
                    bmp.SetPixel(x, y, Color.FromArgb((int)color, (int)color, (int)color));
                }

            return bmp;
        }


        public double getPixel(int x, int y)
        {
            return buffer[x, y];
        }

        public void setPixel(int y, int x, double value)
        {
            buffer[y, x] = value;
        }

        public static double getPixel(WrappedImage image, int y, int x, BorderHandling borderHandling)
        {
            switch (borderHandling)
            {
                case BorderHandling.Black:
                    if (x < 0 || x >= image.width || y < 0 || y >= image.height)
                        return 0;
                    return image.buffer[x, y];
                case BorderHandling.White:
                    if (x < 0 || x >= image.width || y < 0 || y >= image.height)
                        return 1;
                    return image.buffer[y, x];
                case BorderHandling.Copy:
                    x = border(0, x, image.width - 1);
                    y = border(y, 0, image.height - 1);
                    return image.buffer[y, x];
                case BorderHandling.Wrap:
                    x = (x + image.width) % image.width;
                    y = (y + image.height) % image.height;
                    return image.buffer[y, x];
                case BorderHandling.Mirror:
                    x = Math.Abs(x);
                    y = Math.Abs(y);
                    if (x >= image.width) x = image.width - (x - image.width + 1);
                    if (y >= image.height) y = image.height - (y - image.height + 1);
                    return image.buffer[y, x];
                default:
                    return 1;
            }
        }

        private static int border(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        public static double sqr(double value)
        {
            return value * value;
        }

        public void normalize()
        {
            double maxIntensity = 0, minIntensity = 1;
            double resultMaxIntensity = 1, resultMinIntensity = 0;
            foreach (double value in buffer)
            {
                maxIntensity = Math.Max(maxIntensity, value);
                minIntensity = Math.Min(minIntensity, value);
            }
            double coef = (resultMaxIntensity - resultMinIntensity) / (maxIntensity - minIntensity);

            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = (buffer[i, j] - minIntensity) * coef + resultMinIntensity;
                }
            }
        }

        private int getPosition(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                throw new Exception(String.Format("Pixel position out of borders (%d, %d)", x, y));
            return x * height + y;
        }

        public WrappedImage downSample()
        {
            int newWidth = width / 2;
            int newHeight = height / 2;
            WrappedImage result = new WrappedImage(newWidth, newHeight);
            for (int i = 0; i < newWidth; i++)
            {
                for (int j = 0; j < newHeight; j++)
                {
                    result.setPixel(i, j, getPixel(i * 2, j * 2));
                }
            }
            return result;
        }

        public static WrappedImage getGradient(WrappedImage xImage, WrappedImage yImage)
        {
            if (xImage.height != yImage.height || xImage.width != yImage.width)
                throw new Exception("Изображения разного размера");

            WrappedImage gradient = new WrappedImage(xImage.height, xImage.width);

            for (int y = 0; y < gradient.height; y++)
            {
                for (int x = 0; x < gradient.width; x++)
                {
                    gradient.buffer[y, x] = Math.Sqrt(sqr(xImage.buffer[y, x]) + sqr(yImage.buffer[y, x]));
                }
            }

            return gradient;
        }

        public static WrappedImage getGradientAngle(WrappedImage xImage, WrappedImage yImage)
        {
            if (xImage.height != yImage.height || xImage.width != yImage.width)
                throw new Exception("Изображения разного размера");
            WrappedImage gradientAngle = new WrappedImage(xImage.height, xImage.width);

            for (int y = 0; y < gradientAngle.height; y++)
            {
                for (int x = 0; x < gradientAngle.width; x++)
                {
                    gradientAngle.buffer[y, x] = Math.Atan2(yImage.buffer[y, x], xImage.buffer[y, x]);
                }
            }

            return gradientAngle;
        }

    }
}
