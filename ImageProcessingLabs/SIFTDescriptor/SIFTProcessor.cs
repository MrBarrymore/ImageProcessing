using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Descriptor;
using ImageProcessingLabs.ForDescriptor;
using ImageProcessingLabs.Helper;
using ImageProcessingLabs.Points;

namespace ImageProcessingLabs.SIFTDescriptor
{
    public class SIFTProcessor
    {
        private static List<AbstractDescriptor> getDescriptors(Mat gradient,
            Mat gradientAngle,
            List<InterestingPoint> interestingPoints,
            int gridSize,
            int cellSize,
            int binsCount)
        {
            List<AbstractDescriptor> siftDescriptors =
                interestingPoints
                    .Select(interestingPoint => (AbstractDescriptor) Descriptor.SIFTDescriptor.at(gradient,
                        gradientAngle,
                        interestingPoint,
                        gridSize,
                        cellSize,
                        binsCount)).ToList();



            foreach (var siftDescriptor in siftDescriptors)
            {
                double[] descriptor = siftDescriptor.getDescriptor();
                double sum = descriptor.Sum();
                if (Math.Abs(sum) >= 1e-2)
                    siftDescriptor.setDescriptor(descriptor.Select(operand => operand / sum).ToArray());
            }

            return siftDescriptors;
        }


        public List<ValueTuple<ForDescriptor.Descriptor, ForDescriptor.Descriptor>> processWithSift(Mat imageA,
            Mat imageB,
            int gridSize,
            int cellSize,
            int binsCount,
            double MinValueHarris,
            int WindowSize,
            int maxPoints
        )
        {
            List<InterestingPoint> pointsA =
                NonMaximumSuppression.FilterA(imageA, Harris.DoHarris(MinValueHarris, WindowSize, imageA), maxPoints);
            List<InterestingPoint> pointsB =
                NonMaximumSuppression.FilterA(imageB, Harris.DoHarris(MinValueHarris, WindowSize, imageB), maxPoints);

            List<ForDescriptor.Descriptor> descriptorsA = RotationInvariant.Calculate(imageA, pointsA);
            List<ForDescriptor.Descriptor> descriptorsB = RotationInvariant.Calculate(imageB, pointsB);

            List<ValueTuple<ForDescriptor.Descriptor, ForDescriptor.Descriptor>>  match = DescriptorMatcher.Nndr(descriptorsA, descriptorsB);

            var image = DrawHelper.DrawTwoImages(
                DrawHelper.DrawPoints(imageA, pointsA), DrawHelper.DrawPoints(imageB, pointsB), match);

            IOHelper.WriteImageToFile(image, "..\\..\\..\\..\\Output\\OutputPicture.png");


            return match;
        }
    }
}
