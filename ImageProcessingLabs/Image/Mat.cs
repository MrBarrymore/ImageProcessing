using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.enums;

namespace ImageProcessingLabs
{
    public class Mat
    {
        protected readonly double [] Data;
        public readonly int Width;
        public readonly int Height;

        public Mat(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new double[width * height];
        }

        public Mat()
        {
        }

        public Mat(int width, int height, double[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        public double[] GetData()
        {
            return Data;
        }

        public double GetAt(int x, int y)
        {
            return Data[GetIndex(x, y)];
        }


        public double GetPixel(int x, int y, BorderWrapType borderHandling)
        {
            switch (borderHandling)
            {
                case BorderWrapType.Black:
                    if (IsCoordinatesOutOfBounds(x, y))
                        return 0;
                    break;
                case BorderWrapType.Copy:
                    x = Math.Max(0, Math.Min(x, Width - 1));
                    y = Math.Max(0, Math.Min(y, Height - 1));
                    break;
                case BorderWrapType.Wrap:
                    x = (x + Width) % Width;
                    y = (y + Height) % Height;
                    break;
                case BorderWrapType.Mirror:
                    if (x < 0 || x >= Width) x = x - x % Width * 2 - 1;
                    if (y < 0 || y >= Height) y = y - y % Height * 2 - 1;
                    break;

                default:
                    throw new ArgumentException("Illegal border wrap type " + borderHandling);
            }

            return GetAt(x, y);
        }

        public void Set(int x, int y, double value)
        {
            Data[GetIndex(x, y)] = value;
        }

        private bool IsCoordinatesOutOfBounds(int x, int y)
        {
            return x < 0 || x >= Width || y < 0 || y >= Height;
        }

        protected int GetIndex(int x, int y)
        {
            if (IsCoordinatesOutOfBounds(x, y))
                throw new ArgumentException("Coordinates out of bounds");
            return y * Width + x;
        }

        public Mat Clone()
        {
            return new Mat(Width, Height, (double[])Data.Clone());
        }
    }
}
