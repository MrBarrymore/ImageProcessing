using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Wrapped;

namespace ImageProcessingLabs.Descriptor
{
    public abstract class AbstractDescriptor : SIFTDescriptor
    {
        public void normalize()
        {
            double[] descriptor = getDescriptor();
            double sum = descriptor.Sum();
            if (Math.Abs(sum) < 1e-3)
                return;
            setDescriptor(descriptor.Select(operand => operand / sum).ToArray());
        }

        public static double distance(AbstractDescriptor descriptorA, AbstractDescriptor descriptorB)
        {
            double[] descA = descriptorA.getDescriptor();
            double[] descB = descriptorB.getDescriptor();
            if (descA.Length != descB.Length)
                throw new Exception("DESCRIPTORS LENGTH DIFFER!");
            double sum = 0;
            for (int i = 0; i < descA.Length; i++)
            {
                sum += WrappedImage.sqr(descA[i] - descB[i]);
            }
            return Math.Sqrt(sum);
        }


        public abstract double[] getDescriptor();

        public abstract void setDescriptor(double[] descriptor);

        public abstract InterestingPoint getPoint();

    }

}
