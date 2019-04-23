using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Wrapped;


namespace ImageProcessingLabs.Descriptor
{
    class SIFTDescriptor
    {
        private double[] descriptor;
        private InterestingPoint point;

        public static SIFTDescriptor at(WrappedImage gradient,
                                   WrappedImage gradientAngle,
                                   InterestingPoint point,
                                   int gridSize,
                                   int cellSize,
                                   int binsCount)
        {
            SIFTDescriptor siftDescriptor = new SIFTDescriptor();
            siftDescriptor.point = point;
            siftDescriptor.descriptor = new double[gridSize * gridSize * binsCount];

            int gridHalfSize = gridSize / 2;

            double [,] gauss = ConvolutionMatrix.CountGaussMatrix(gridHalfSize * cellSize);

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

                            double phi = gradientAngle.getPixel(gradient.buffer, realX, realY, BorderHandling.Mirror);
                            double gradientValue = gradient.getPixel(gradient.buffer, realX, realY, BorderHandling.Mirror);
                            double gaussValue = CommonMath.getPixel(gauss, centerShift + cellX * cellSize + pixelX,
                                                          centerShift + cellY * cellSize + pixelY, 4);

                            bin.addAngle(phi, gradientValue * gaussValue);
                        }
                    }

                    System.Array.Copy(bin.GetBin(), 0, siftDescriptor.descriptor, ptr, binsCount);
                    ptr += binsCount;
                }
            }

            return siftDescriptor;
        }

        void setDescriptor(double[] descriptor)
        {
            this.descriptor = descriptor;
        }

        double[] getDescriptor()
        {
            return descriptor;
        }

        InterestingPoint getPoint()
        {
            return point;
        }

    }
}
