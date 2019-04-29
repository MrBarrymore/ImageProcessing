using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Descriptor
{
    public class AngleBin
    {
        private static double EPS = 1e-6;
        private double[] bin;
        private int size;
        private double step;

        public AngleBin(int size)
        {
            this.size = size;
            this.bin = new double[size];
            step = Math.PI / size;
        }

        public void addAngle(double angle, double value)
        {
            if (angle < 0) angle += Math.PI;
            if (angle >= Math.PI) angle -= Math.PI;

            angle /= step;
            int binIdx = (int)angle;
            double binCenter = binIdx + 0.5;
            double weight = Math.Abs(binCenter - angle);
            int neighbourIdx = (binIdx + 1) % size;
            if (angle <= binCenter)
            {
                neighbourIdx = (binIdx - 1 + size) % size;
            }

            bin[binIdx] += weight * value;
            bin[neighbourIdx] += (1 - weight) * value;
        }

        public double[] GetBin()
        {
            return bin;
        }
    }
}
