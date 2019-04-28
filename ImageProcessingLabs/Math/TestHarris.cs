using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs
{

    public enum BorderWrapType
    {
        Default,
        Mirror,
        Constant,
        Wrap
    }

    public static class Harris2
    {
        public static Mat Find(Mat image, int k, double threshold, BorderWrapType borderWrap = BorderWrapType.Default)
        {
            var gauss = Gauss.GetFullKernel(k / 3D);
            var gaussK = gauss.Width / 2;

            var dx = new Mat(image.Width, image.Height);
            var dy = new Mat(image.Width, image.Height);

            SobelHelper.Sobel(image, dx, dy);

            var lambdas = new Mat(image.Width, image.Height);
            for (var x = 0; x < image.Width; x++)
                for (var y = 0; y < image.Height; y++)
                {
                    double a = 0, b = 0, c = 0;

                    for (var u = -k; u <= k; u++)
                        for (var v = -k; v <= k; v++)
                        {
                            var multiplier = gauss.GetAt(u + gaussK, v + gaussK);

                            a += multiplier * MathHelper.Sqr(dx.GetDefault(x + u, y + v, borderWrap));
                            b += multiplier * dx.GetDefault(x + u, y + v, borderWrap) *
                                 dy.GetDefault(x + u, y + v, borderWrap);
                            c += multiplier * MathHelper.Sqr(dy.GetDefault(x + u, y + v, borderWrap));
                        }

                    lambdas.Set(x, y, LambdaMin(a, b, c));
                }

            return CornerDetectionHelper.FindPoints(lambdas, threshold);
        }

        public static double FindAt(Mat image, int radius, int x, int y)
        {
            var gauss = Gauss.GetFullKernel(radius / 3.0);

            double a = 0, b = 0, c = 0;
            for (var u = -radius; u <= radius; u++)
                for (var v = -radius; v <= radius; v++)
                {
                    var ix = ConvolutionHelper.ConvolveCell(image, SobelHelper.SobelKernelX, x + u, y + v);
                    var iy = ConvolutionHelper.ConvolveCell(image, SobelHelper.SobelKernelY, x + u, y + v);
                    var gaussPoint = gauss.GetDefault(u + radius, v + radius);
                    a += gaussPoint * ix * ix;
                    b += gaussPoint * ix * iy;
                    c += gaussPoint * iy * iy;
                }

            return LambdaMin(a, b, c);
        }

        private static double LambdaMin(double varA, double varB, double varC)
        {
            double a = 1;
            var b = -(varA + varC);
            var c = varA * varC - varB * varB;
            var d = b * b - 4 * a * c;

            if (d < 0 && d > -1e-6) d = 0;

            if (d < 0) return 0;

            var a1 = (-b + Math.Sqrt(d)) / (2 * a);
            var a2 = (-b - Math.Sqrt(d)) / (2 * a);

            return Math.Min(a1, a2);
        }
    }


    public class Mat
    {
        protected readonly double[] Data;
        public readonly int Height;
        public readonly int Width;

        public Mat(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new double[width * height];
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

        public double GetDefault(int x, int y, BorderWrapType borderWrap = BorderWrapType.Default)
        {
            switch (borderWrap)
            {
                case BorderWrapType.Constant:
                    if (IsCoordinatesOutOfBounds(x, y))
                        return 0;
                    break;
                case BorderWrapType.Default:
                    x = Math.Max(0, Math.Min(x, Width - 1));
                    y = Math.Max(0, Math.Min(y, Height - 1));
                    break;
                case BorderWrapType.Mirror:
                    if (x < 0 || x >= Width) x = x - x % Width * 2 - 1;
                    if (y < 0 || y >= Height) y = y - y % Height * 2 - 1;
                    break;
                case BorderWrapType.Wrap:
                    x = (x + Width) % Width;
                    y = (y + Height) % Height;
                    break;
                default:
                    throw new ArgumentException("Illegal border wrap type " + borderWrap);
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


    public static class Gauss
    {
        public static Kernel GetKernel(double sigma)
        {
            return GetKernel((int)Math.Round(3 * sigma), sigma);
        }

        private static Kernel GetKernel(int k, double sigma)
        {
            var vector = new double[k * 2 + 1];
            var prefix = 1.0 / (Math.Sqrt(2 * Math.PI) * sigma);

            for (var x = 0; x <= k; x++)
                vector[-x + k] = vector[x + k] =
                    prefix * Math.Exp(-MathHelper.Sqr(x) / (2 * MathHelper.Sqr(sigma)));

            double sum = 0;
            for (var x = -k; x <= k; x++) sum += vector[x + k];

            for (var x = -k; x <= k; x++) vector[x + k] /= sum;

            var mat = new Mat(vector.Length, 1, vector);
            return Kernel.For(mat, mat);
        }

        public static Mat GetFullKernel(double sigma)
        {
            var halfSize = (int)Math.Round(3 * sigma);
            var fullSize = halfSize * 2 + 1;

            var data = new double[fullSize * fullSize];
            var k = 1.0 / (2 * Math.PI * sigma * sigma);

            for (var x = -halfSize; x <= halfSize; x++)
            for (var y = -halfSize; y <= halfSize; y++)
            {
                var value = Math.Exp(-(x * x + y * y) / (2 * sigma * sigma));
                data[(y + halfSize) * fullSize + x + halfSize] = k * value;
            }

            return new Mat(fullSize, fullSize, data);
        }
    }


    public static class SobelHelper
    {
        public static readonly Mat SobelKernelX = new Mat(3, 3, new double[] { 1, 0, -1, 2, 0, -2, 1, 0, -1 });
        public static readonly Mat SobelKernelY = new Mat(3, 3, new double[] { 1, 2, 1, 0, 0, 0, -1, -2, -1 });
        private static readonly Mat SobelA = new Mat(3, 1, new double[] { -1, 0, 1 });
        private static readonly Mat SobelB = new Mat(3, 1, new double[] { 1, 2, 1 });
        private static readonly Kernel SobelKernelA = Kernel.For(SobelA, SobelB);
        private static readonly Kernel SobelKernelB = Kernel.For(SobelB, SobelA);

        public static Mat Sobel(Mat source, Mat dx, Mat dy)
        {
            ConvolutionHelper.Separable(source, dx, SobelKernelA, BorderWrapType.Default);
            ConvolutionHelper.Separable(source, dy, SobelKernelB, BorderWrapType.Default);

            var target = new Mat(source.Width, source.Height);
            for (var x = 0; x < source.Width; x++)
            for (var y = 0; y < source.Height; y++)
                target.Set(x, y, MathHelper.SqrtOfSqrSum(dx.GetAt(x, y), dy.GetAt(x, y)));

            return target;
        }

        public static Mat SobelNonSeparable(Mat source, Mat dx, Mat dy)
        {
            ConvolutionHelper.NonSeparable(source, dx, SobelKernelX, BorderWrapType.Default);
            ConvolutionHelper.NonSeparable(source, dy, SobelKernelY, BorderWrapType.Default);

            var target = new Mat(source.Width, source.Height);
            for (var x = 0; x < source.Width; x++)
            for (var y = 0; y < source.Height; y++)
                target.Set(x, y, MathHelper.SqrtOfSqrSum(dx.GetAt(x, y), dy.GetAt(x, y)));

            return target;
        }
    }

    public static class MathHelper
    {
        public static double Sqr(double value)
        {
            return value * value;
        }

        public static int FindNearestGeomElement(double a, double q, double value)
        {
            var x = (Math.Log(value) - Math.Log(a)) / Math.Log(q);
            return (int)Math.Round(x);
        }

        public static double SqrtOfSqrSum(double a, double b)
        {
            return Math.Sqrt(Sqr(a) + Sqr(b));
        }
    }


    public static class CornerDetectionHelper
    {
        public static Mat FindPoints(Mat mat, double threshold)
        {
            mat = NormalizationHelper.Normalization(mat);
            var result = new Mat(mat.Width, mat.Height);

            for (var x = 0; x < mat.Width; x++)
            for (var y = 0; y < mat.Height; y++)
            {
                if (mat.GetAt(x, y) <= threshold || HasBetterNeighbour(mat, x, y)) continue;
                result.Set(x, y, mat.GetAt(x, y));
            }

            return result;
        }

        private static bool HasBetterNeighbour(Mat mat, int x, int y)
        {
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;
                if (mat.GetDefault(x + dx, y + dy, BorderWrapType.Constant) - mat.GetAt(x, y) > 1e-6)
                    return true;
            }

            return false;
        }

        public static List<PointOfInterest> MatToPoints(Mat mat)
        {
            var list = new List<PointOfInterest>();
            for (var x = 0; x < mat.Width; x++)
            for (var y = 0; y < mat.Height; y++)
                if (mat.GetAt(x, y) > 1e-6)
                    list.Add(new PointOfInterest(x, y, mat.GetAt(x, y)));

            return list;
        }
    }

    public static class ConvolutionHelper
    {
        public static Mat Convolution(Mat source, Kernel kernel, BorderWrapType borderWrap = BorderWrapType.Default)
        {
            var target = new Mat(source.Width, source.Height);
            Separable(source, target, kernel, borderWrap);
            return target;
        }

        public static void Separable(Mat source, Mat target, Kernel kernel, BorderWrapType borderWrap)
        {
            int kx = kernel.GetXVector().Width / 2,
                ky = kernel.GetYVector().Width / 2;

            var tmp = source.Clone();
            for (var x = 0; x < source.Width; x++)
                for (var y = 0; y < source.Height; y++)
                {
                    double value = 0;

                    for (var dx = -kx; dx <= kx; dx++)
                        value += source.GetDefault(x - dx, y, borderWrap) * kernel.GetXVector().GetAt(dx + kx, 0);

                    tmp.Set(x, y, value);
                }

            if (tmp == source)
                tmp = null;

            for (var x = 0; x < source.Width; x++)
                for (var y = 0; y < source.Height; y++)
                {
                    double value = 0;

                    for (var dy = -ky; dy <= ky; dy++)
                        value += tmp.GetDefault(x, y - dy, borderWrap) * kernel.GetYVector().GetAt(dy + ky, 0);

                    target.Set(x, y, value);
                }
        }

        public static void NonSeparable(Mat source, Mat target, Mat kernel, BorderWrapType borderWrap)
        {
            int kx = kernel.Width / 2,
                ky = kernel.Height / 2;

            for (var x = 0; x < source.Width; x++)
                for (var y = 0; y < source.Height; y++)
                {
                    double value = 0;

                    for (var dx = -kx; dx <= kx; dx++)
                        for (var dy = -ky; dy <= ky; dy++)
                            value += source.GetDefault(x - dx, y - dy, borderWrap) * kernel.GetAt(dx + kx, dy + ky);

                    target.Set(x, y, value);
                }
        }

        public static double ConvolveCell(Mat source, Mat kernel, int x, int y)
        {
            double result = 0;
            var w2 = kernel.Width / 2;
            var h2 = kernel.Height / 2;

            for (var kx = 0; kx < kernel.Width; kx++)
                for (var ky = 0; ky < kernel.Height; ky++)
                    result += source.GetDefault(x - kx + w2, y - ky + h2) * kernel.GetDefault(kx, ky);

            return result;
        }
    }

    public class Kernel
    {
        private readonly Mat xVector;
        private readonly Mat yVector;

        private Kernel(Mat xVector, Mat yVector)
        {
            this.xVector = xVector;
            this.yVector = yVector;
        }

        public Mat GetXVector()
        {
            return xVector;
        }

        public Mat GetYVector()
        {
            return yVector;
        }

        public static Kernel For(Mat xVector, Mat yVector)
        {
            return new Kernel(xVector, yVector);
        }
    }


    public static class NormalizationHelper
    {
        public static Mat Normalization(Mat source)
        {
            var target = new Mat(source.Width, source.Height);
            Normalization(source, target);
            return target;
        }

        private static void Normalization(Mat source, Mat target, double newMin = 0, double newMax = 1)
        {
            Normalization(source.GetData(), target.GetData(), newMin, newMax);
        }

        private static void Normalization(double[] source, double[] target, double newMin, double newMax)
        {
            double min = source[0], max = source[0];
            foreach (var value in source)
            {
                if (double.IsNaN(value))
                    continue;

                min = Math.Min(min, value);
                max = Math.Max(max, value);
            }

            for (var i = 0; i < source.Length; i++)
                target[i] = (source[i] - min) * (newMax - newMin) / (max - min) + newMin;
        }
    }


    public class PointOfInterest
    {
        public readonly int Octave;
        public readonly double Radius = -1;
        public readonly int X;
        public readonly int Y;
        public List<double> Angles = new List<double>();
        public double Value;

        public PointOfInterest(int x, int y, double value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public PointOfInterest(int x, int y, double radius, double value, int octave)
        {
            X = x;
            Y = y;
            Radius = radius;
            Value = value;
            Octave = octave;
        }

        public double DistanceTo(PointOfInterest b)
        {
            // квадрат расстояния
            return MathHelper.Sqr(X - b.X) + MathHelper.Sqr(Y - b.Y);
        }
    }

}




