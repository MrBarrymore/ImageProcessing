using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.enums;
using ImageProcessingLabs.Points;


namespace ImageProcessingLabs.Descriptor
{
   public class SIFTDescriptor : AbstractDescriptor
    {
        public double[] descriptor;
        public InterestingPoint point;

        public static SIFTDescriptor at(Mat gradient,
                                     Mat gradientAngle,
                                   InterestingPoint point,
                                   int gridSize,
                                   int cellSize,
                                   int binsCount)
        {
            SIFTDescriptor siftDescriptor = new SIFTDescriptor();
            siftDescriptor.point = point;
            siftDescriptor.descriptor = new double[gridSize * gridSize * binsCount];

            int gridHalfSize = gridSize / 2;

            Mat gauss = ConvolutionHelper.Convolution(gradient, Gauss.GetKernel(0.6));

            Gauss.GetKernel(gridHalfSize * cellSize / 3);

            int ptr = 0;
            int centerShift = gridHalfSize * cellSize;

            for (int cellX = -gridHalfSize; cellX < gridSize - gridHalfSize; cellX++)
            {
                for (int cellY = -gridHalfSize; cellY < gridSize - gridHalfSize; cellY++)
                {

                    AngleBin bin = new AngleBin(binsCount);

                    for (int pixelX = 0; pixelX < cellSize; pixelX++)
                    {
                        for (int pixelY = 0; pixelY < cellSize; pixelY++)
                        {
                            int realX = point.getX() + cellX * cellSize + pixelX;
                            int realY = point.getY() + cellY * cellSize + pixelY;

                            double phi = gradient.GetPixel(realX, realY, BorderWrapType.Mirror);
                            double gradientValue = gradientAngle.GetPixel(realX, realY, BorderWrapType.Mirror);
                            double gaussValue = gauss.GetPixel(centerShift + cellX * cellSize + pixelX,
                                                          centerShift + cellY * cellSize + pixelY, BorderWrapType.Mirror);

                            bin.addAngle(phi, gradientValue * gaussValue);
                        }
                    }

                    System.Array.Copy(bin.GetBin(), 0, siftDescriptor.descriptor, ptr, binsCount);
                    ptr += binsCount;
                }
            }

            return siftDescriptor;
        }

        public override void setDescriptor(double[] descriptor)
        {
            this.descriptor = descriptor;
        }

        public override double[] getDescriptor()
        {
            return descriptor;
        }

        public override InterestingPoint getPoint()
        {
            return point;
        }

    }
}
