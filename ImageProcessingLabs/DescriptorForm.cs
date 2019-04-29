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
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.Descriptor;
using ImageProcessingLabs.enums;
using ImageProcessingLabs.Wrapped;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Scale;
using ImageProcessingLabs.Transformation;
using ImageProcessingLabs.Descriptor;
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

            if (filter_checkBox.Checked == true) maxPoints = Convert.ToInt32(txb_Filter.Text);
            else maxPoints = 5000;


            var pointPairs = processWithSift(imageA, imageB, 16, 4, 8, MinValueHarris, WindowSize, maxPoints);

            //BuildPicture(pointPairs, imageA, imageB);

        }

        //   public List<PointsPair> processWithSift(Mat imageA,
        public List<ValueTuple<Descriptor.Descriptor, Descriptor.Descriptor>> processWithSift(Mat imageA,
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

            double maxRadiusA = Math.Sqrt(WrappedImage.sqr(imageA.Width) + WrappedImage.sqr(imageA.Height));
            double maxRadiusB = Math.Sqrt(WrappedImage.sqr(imageB.Width) + WrappedImage.sqr(imageB.Height));

            List<InterestingPoint> pointsA =
                  NonMaximumSuppression.FilterA(imageA, Harris.DoHarris(MinValueHarris, WindowSize, imageA), maxPoints);
            List<InterestingPoint> pointsB =
                NonMaximumSuppression.FilterA(imageB, Harris.DoHarris(MinValueHarris, WindowSize, imageA), maxPoints);


            lbl_findPoints1.Text = "Найдено интересных точек(1): " + pointsA.Count;
            lbl_findPoints2.Text = "Найдено интересных точек(2): " + pointsB.Count;


            DrawPoints(pointsA, imageA, 1);
            DrawPoints(pointsB, imageB, 2);

            /*
            List<AbstractDescriptor> descriptorsA = getDescriptors(gradientA,
                gradientAngleA,
                pointsA,
                gridSize,
                cellSize,
                binsCount);

            List<AbstractDescriptor> descriptorsB = getDescriptors(gradientB,
             gradientAngleB,
             pointsB,
             gridSize,
             cellSize,
             binsCount);

           return DescriptorUtil.match(descriptorsA, descriptorsB);
           */

            var descriptorsA = HOG.Calculate(imageA, pointsA);
            var descriptorsB = HOG.Calculate(imageB, pointsB);


            var match = DescriptorMatcher.Nndr(descriptorsA, descriptorsB);

            var image = DrawHelper.DrawTwoImages(
                DrawHelper.DrawPoints(imageA, pointsA), DrawHelper.DrawPoints(imageB, pointsB), match);

            IOHelper.WriteImageToFile(image, "..\\..\\..\\..\\Output\\OutputPicture.png");


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

        public void BuildPicture(List<PointsPair> pointPairs, Mat inputImageA, Mat inputImageB)
        {
            Bitmap picture;

            picture = WrappedImage.CombineImages(inputImageA, inputImageB);

            Graphics graph = Graphics.FromImage(picture);
            Color pen = Color.Blue;
            Pen penLine = new Pen(Color.Blue);

            foreach (var pointPair in pointPairs)
            {
                graph.FillEllipse(new SolidBrush(pen), pointPair.pointA.getX(), pointPair.pointA.getY(), 3, 3);
                graph.FillEllipse(new SolidBrush(pen), pointPair.pointB.getX() + (imageB.Width + 20), pointPair.pointB.getY(), 3, 3);
                graph.DrawLine(penLine, pointPair.pointA.getX(), pointPair.pointA.getY(), pointPair.pointB.getX() + (imageB.Width + 20), pointPair.pointB.getY());
            }

            pictureBox1.Image = picture;

            picture.Save("..\\..\\..\\..\\Output\\OutputPicture.png");

        }

        public void DrawPoints(List<InterestingPoint> point, Mat inputImage, int picture)
        {
            Bitmap image;
            image = Transformer.FromUInt32ToBitmap(inputImage);
            Graphics graph = Graphics.FromImage(image);
            Color pen = Color.Blue;

            foreach (var interestingPoint in point)
            {
                graph.FillEllipse(new SolidBrush(pen), interestingPoint.getX(), interestingPoint.getY(), 3, 3);
            }

            if (picture == 1) pictureBox1.Image = image;

            image.Save("..\\..\\..\\..\\Output\\OutputPicture.png");
        }


    }
}
