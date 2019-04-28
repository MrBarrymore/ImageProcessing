using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Helper
{
    class NormalizationHelper
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
}
