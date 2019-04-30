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
using System.Text.RegularExpressions;
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.Descriptor;
using ImageProcessingLabs.ForDescriptor;
using ImageProcessingLabs.enums;
using ImageProcessingLabs.ForDescriptor;
using ImageProcessingLabs.Wrapped;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Scale;
using ImageProcessingLabs.Transformation;
using ImageProcessingLabs.Helper;


namespace ImageProcessingLabs
{
    public partial class DescriptorForm : Form
    {
        private Mat imageA, imageB;
        private static int POINTS = 100;
        private static double MinValueHarris = 0.05;
        private static int WindowSize = 5;

        public DescriptorForm()
        {
            InitializeComponent();
        }

        public DescriptorForm(Mat image)
        {
            InitializeComponent();
            this.imageA = image;

        }

        public DescriptorForm(Mat _imageA, Mat _imageB)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.imageA = _imageA.Clone();
            this.imageB = _imageB.Clone();

        }

        private void FindPointButton_Click(object sender, EventArgs e)
        {
            MinValueHarris = Convert.ToDouble(txb_minValue.Text);
            WindowSize = Convert.ToInt32(txb_WindowSize.Text);
            int maxPoints;
            int gridSize = Convert.ToInt32(txb_gridSize.Text);
            int cellSize = Convert.ToInt32(txb_cellSize.Text);
            int binsCount = Convert.ToInt32(txb_binsCount.Text);

            if (filter_checkBox.Checked == true) maxPoints = Convert.ToInt32(txb_Filter.Text);
            else maxPoints = 5000;

            var pointPairs = processWithSift(imageA, imageB, gridSize, cellSize, binsCount, MinValueHarris, WindowSize, maxPoints);

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

            Mat xA = SobelHelper.GetSobelX(imageA, BorderWrapType.Mirror);
            Mat yA = SobelHelper.GetSobelY(imageA, BorderWrapType.Mirror);
            Mat xB = SobelHelper.GetSobelX(imageB, BorderWrapType.Mirror);
            Mat yB = SobelHelper.GetSobelY(imageB, BorderWrapType.Mirror);

            Mat gradientA = SobelHelper.getGradient(xA, yA);
            Mat gradientAngleA = SobelHelper.getGradientAngle(xA, yA);
            Mat gradientB = SobelHelper.getGradient(xB, yB);
            Mat gradientAngleB = SobelHelper.getGradientAngle(xB, yB);


            List<InterestingPoint> pointsA =
                  NonMaximumSuppression.FilterA(imageA, Harris.DoHarris(MinValueHarris, WindowSize, imageA), maxPoints);
            List<InterestingPoint> pointsB =
                NonMaximumSuppression.FilterA(imageB, Harris.DoHarris(MinValueHarris, WindowSize, imageB), maxPoints);

            List<ForDescriptor.Descriptor> descriptorsA = FindDescriptor.Calculate(imageA, pointsA);
            List<ForDescriptor.Descriptor> descriptorsB = FindDescriptor.Calculate(imageB, pointsB);

            List<ValueTuple<ForDescriptor.Descriptor, ForDescriptor.Descriptor>> match;
            if (rbt_usual.Checked == true) match = DescriptorMatcher.Match(descriptorsA, descriptorsB);
            else if (rbt_NNDR.Checked == true) match = DescriptorMatcher.Nndr(descriptorsA, descriptorsB);
            else match = DescriptorMatcher.Match(descriptorsA, descriptorsB);

            lbl_findPoints1.Text = "Найдено интересных точек(1): " + pointsA.Count;
            lbl_findPoints2.Text = "Найдено интересных точек(2): " + pointsB.Count;
            lbl_PairCount.Text = "Найдено пар точек: " + match.Count;

            var image = DrawHelper.DrawTwoImages(
               DrawHelper.DrawPoints(imageA, pointsA), DrawHelper.DrawPoints(imageB, pointsB), match);

            IOHelper.WriteImageToFile(image, "..\\..\\..\\..\\Output\\OutputPicture.png");

            pictureBox1.Image = image;
            
            return match;
            
        }




        private static List<AbstractDescriptor> getDescriptors(Mat gradient,
            Mat gradientAngle,
            List<InterestingPoint> interestingPoints,
            int gridSize,
            int cellSize,
            int binsCount)
        {
            List<AbstractDescriptor> siftDescriptors =
                interestingPoints
                    .Select(interestingPoint => (AbstractDescriptor)SIFTDescriptor.at(gradient,
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

    }
}
