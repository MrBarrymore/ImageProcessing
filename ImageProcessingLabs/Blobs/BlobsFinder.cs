using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.enums;
using ImageProcessingLabs.ForDescriptor;
using ImageProcessingLabs.Helper;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Scale;

namespace ImageProcessingLabs.Blobs
{
    public class BlobsFinder
    {
        private const int PointCount = 70;
        private const double InitSigma = 0.5;
        private const double StartSigma = 1.6;
        private const int OctaveSize = 4;

        public static List<ForDescriptor.Descriptor> FindBlobs(Mat image)
        {
            var pyramid = Pyramid.Build(image, OctaveSize, InitSigma, StartSigma);
            var points = NonMaximumSuppression.FilterA(image, FindPoints(pyramid), PointCount);

            var descriptors = new List<ForDescriptor.Descriptor>();

            for (var octave = 0; octave < Pyramid.Depth; octave++)
            {
                var firstImage = pyramid.GetLayer(octave, 0);
                var curPoints = points.Where(x => x.Octave == octave).ToList();
                
                // Сделать вывод точек на картинке
             //   IOHelper.WriteImageToFile(DrawHelper.DrawPoints(pyramid.GetDoG(octave, 2), curPoints), "..\\..\\..\\..\\Output\\" + octave + ".png");

                descriptors.AddRange(RotationInvariant.Calculate(firstImage, curPoints));
            }

            return descriptors;
        }

        private static List<InterestingPoint> FindPoints(Pyramid pyramid)
        {
            var blobs = new List<InterestingPoint>();

            for (var octave = 0; octave < Pyramid.Depth; octave++)
            {
                var firstImage = pyramid.GetLayer(octave, 0);       // Берем первую картинку в октаве 

                for (var layer = 1; layer <= pyramid.OctaveSize; layer++) // 
                {
                    var prev = pyramid.GetDoG(octave, layer - 1);
                    var cur = pyramid.GetDoG(octave, layer);        // Берем Dog на двух соседних урвнях одной октавы 
                    var next = pyramid.GetDoG(octave, layer + 1);

                    IOHelper.WriteImageToFile(DrawHelper.DrawPoints(pyramid.GetDoG(octave, layer), new List<InterestingPoint>()), 
                        "..\\..\\..\\..\\Output\\Октава " + octave + " Уровень " + layer);

                   
                }
            }
            return blobs;
        }


        private static int GetCoordinate(int value, int octave)
        {
            return (int)((value + 0.5) * (1 << octave));
        }

    }
}
