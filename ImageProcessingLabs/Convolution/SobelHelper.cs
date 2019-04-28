using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  ImageProcessingLabs.enums;
using  ImageProcessingLabs.Helper;

namespace ImageProcessingLabs.Convolution
{
    public static class SobelHelper
    {
        public static readonly Mat SobelKernelX = new Mat(3, 3, new double[] { 1, 0, -1, 2, 0, -2, 1, 0, -1 });
        public static readonly Mat SobelKernelY = new Mat(3, 3, new double[] { 1, 2, 1, 0, 0, 0, -1, -2, -1 });

        private static readonly Mat SobelA = new Mat(3, 1, new double[] { -1, 0, 1 });
        private static readonly Mat SobelB = new Mat(3, 1, new double[] { 1, 2, 1 });
        
        private static readonly Kernel SobelKernelA = Kernel.For(SobelA, SobelB);
        private static readonly Kernel SobelKernelB = Kernel.For(SobelB, SobelA);

        public static Mat Sobel(Mat source, Mat dx, Mat dy, BorderWrapType borderWrapType)
        {
            ConvolutionHelper.Separable(source, dx, SobelKernelA, borderWrapType);
            ConvolutionHelper.Separable(source, dy, SobelKernelB, borderWrapType);

            var target = new Mat(source.Width, source.Height);
            for (var x = 0; x < source.Width; x++)
            for (var y = 0; y < source.Height; y++)
                target.Set(x, y, MathHelper.SqrtOfSqrSum(dx.GetAt(x, y), dy.GetAt(x, y)));

            return target;
        }

        public static Mat SobelNonSeparable(Mat source, Mat dx, Mat dy)
        {
            ConvolutionHelper.NonSeparable(source, dx, SobelKernelX, BorderWrapType.Copy);
            ConvolutionHelper.NonSeparable(source, dy, SobelKernelY, BorderWrapType.Copy);

            var target = new Mat(source.Width, source.Height);
            for (var x = 0; x < source.Width; x++)
            for (var y = 0; y < source.Height; y++)
                target.Set(x, y, MathHelper.SqrtOfSqrSum(dx.GetAt(x, y), dy.GetAt(x, y)));

            return target;
        }

        public static Mat GetSobelX(Mat source, Mat dx, Mat dy)
        {
            ConvolutionHelper.NonSeparable(source, dx, SobelKernelX, BorderWrapType.Copy);
            return dx;
        }

        public static Mat GetSobelY(Mat source, Mat dx, Mat dy)
        {
            ConvolutionHelper.NonSeparable(source, dy, SobelKernelY, BorderWrapType.Copy);
            return dy;
        }
    }
}
