using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingLabs.Convolution
{
    class Kernel
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
}
