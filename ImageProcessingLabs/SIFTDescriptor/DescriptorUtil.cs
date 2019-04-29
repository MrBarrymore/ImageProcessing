using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessingLabs.Descriptor;

namespace ImageProcessingLabs.Descriptor
{
    public class DescriptorUtil
    {
        public static List<PointsPair> match(List <AbstractDescriptor> descriptorsA,
            List<AbstractDescriptor> descriptorsB)
        {
            List<PointsPair> pointsMatchings = new List<PointsPair>();

            foreach (AbstractDescriptor descriptorA in descriptorsA)
            {
                AbstractDescriptor closest = getClosest(descriptorA, descriptorsB);
                AbstractDescriptor closestB = getClosest(closest, descriptorsA);
                if (closestB != descriptorA) continue;
                pointsMatchings.Add(PointsPair.from(descriptorA.getPoint(), closest.getPoint()));
            }

            return pointsMatchings;
        }

        private static AbstractDescriptor getClosest(AbstractDescriptor descriptor,
            List<AbstractDescriptor> descriptors)
        {
            double min = Double.MaxValue;
            AbstractDescriptor selected = null;
            foreach (AbstractDescriptor patchDescriptor in descriptors)
            {
                double distance = AbstractDescriptor.distance(descriptor, patchDescriptor);
                if (AbstractDescriptor.distance(descriptor, patchDescriptor) < min)
                {
                    min = distance;
                    selected = patchDescriptor;
                }
            }
            return selected;
        }

    }
}
