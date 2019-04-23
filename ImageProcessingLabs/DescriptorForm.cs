using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using ImageProcessingLabs.Descriptor;
using ImageProcessingLabs.Wrapped;
using ImageProcessingLabs.Points;

namespace ImageProcessingLabs
{
    public partial class DescriptorForm : Form
    {
        private WrappedImage imageA, imageB;
        private static int POINTS = 30;
        private static double MinValueHarris = 0.1;
        private static int WindowSize = 5;

        public DescriptorForm()
        {
            InitializeComponent();
        }

        public DescriptorForm(WrappedImage image)
        {
            InitializeComponent();
            this.imageA = image;

        }

        public DescriptorForm(WrappedImage imageA, WrappedImage imageB)
        {
            InitializeComponent();
            this.imageA = imageA;
            this.imageB = imageB;

        }

        private void FindPointButton_Click(object sender, EventArgs e)
        {

            int windowSize = Convert.ToInt32(txb_WindowSize.Text);
            double minValue = Convert.ToDouble(txb_minValue.Text);

            processWithSift(imageA, imageB, 16, 4, 8,  MinValueHarris, WindowSize);

        }

        public static List<PointsPair> processWithSift(WrappedImage imageA,
            WrappedImage imageB,
            int gridSize,
            int cellSize,
            int binsCount,
            double MinValueHarris,
            int WindowSize
            )
        {

            WrappedImage xA = CommonMath.GetSobelX(imageA, BorderHandling.Mirror);
            WrappedImage yA = CommonMath.GetSobelY(imageA, BorderHandling.Mirror);
            WrappedImage xB = CommonMath.GetSobelX(imageB, BorderHandling.Mirror);
            WrappedImage yB = CommonMath.GetSobelY(imageB, BorderHandling.Mirror);


            WrappedImage gradientA = WrappedImage.getGradient(xA, yA);
            WrappedImage gradientAngleA = WrappedImage.getGradientAngle(xA, yA);
            WrappedImage gradientB = WrappedImage.getGradient(xB, yB);
            WrappedImage gradientAngleB = WrappedImage.getGradientAngle(xB, yB);

            List<InterestingPoint> pointsA = PointFilter.filterPoints(Harris.DoHarris(MinValueHarris, WindowSize, imageA), POINTS);
            List<InterestingPoint> pointsB = PointFilter.filterPoints(Harris.DoHarris(MinValueHarris, WindowSize, imageB), POINTS);


            List<SIFTDescriptor> descriptorsA = getDescriptors(gradientA,
                gradientAngleA,
                pointsA,
                gridSize,
                cellSize,
                binsCount);

            List<SIFTDescriptor> descriptorsB = getDescriptors(gradientB,
                gradientAngleB,
                pointsB,
                gridSize,
                cellSize,
                binsCount);
            
            var pointsPair = new List<PointsPair>();
            // return DescriptorUtil.match(descriptorsA, descriptorsB);
            //   return PointsPair.from(descriptorsA, descriptorsB);
            return pointsPair;
        }

        private static List<SIFTDescriptor> getDescriptors(WrappedImage gradient,
            WrappedImage gradientAngle,
            List<InterestingPoint> interestingPoints,
            int gridSize,
            int cellSize,
            int binsCount)
        {
            List<SIFTDescriptor> siftDescriptors =
                interestingPoints
                    .Select(interestingPoint => SIFTDescriptor.at(gradient,
                        gradientAngle,
                        interestingPoint,
                        gridSize,
                        cellSize,
                        binsCount)).ToList();

            AbstractDescriptor abs;

          //  siftDescriptors.ForEach(AbstractDescriptor::normalize()); // Переделать нормализацию !!!!!

         //   listNames.GroupBy(v => v).Where(g => g.Count() > 1).Select(g => g.Key)
            return siftDescriptors;
        }

    }
}
