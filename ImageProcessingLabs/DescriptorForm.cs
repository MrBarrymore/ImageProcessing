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
        private static int POINTS = 100;
        private static double MinValueHarris = 0.05;
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

        public DescriptorForm(WrappedImage _imageA, WrappedImage _imageB)
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

            int windowSize = Convert.ToInt32(txb_WindowSize.Text);
            double minValue = Convert.ToDouble(txb_minValue.Text);

            int maxPoints;

            if (filter_checkBox.Checked == true) maxPoints = Convert.ToInt32(txb_Filter.Text);
            else maxPoints = 5000;


            List<PointsPair> pointPairs = processWithSift(imageA, imageB, 16, 4, 8, MinValueHarris, WindowSize, maxPoints);

            BuildPicture(pointPairs, imageA, imageB);
        }

        public List<PointsPair> processWithSift(WrappedImage imageA,       
            WrappedImage imageB,
            int gridSize,
            int cellSize,
            int binsCount,
            double MinValueHarris,
            int WindowSize,
            int maxPoints
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

            List<InterestingPoint> pointsA = 
                PointFilter.filterPoints(Harris.DoHarris(MinValueHarris, WindowSize, imageA), maxPoints);
            List<InterestingPoint> pointsB = 
                PointFilter.filterPoints(Harris.DoHarris(MinValueHarris, WindowSize, imageB), maxPoints);


            lbl_findPoints1.Text = "Найдено интересных точек(1): " + pointsA.Count;
            lbl_findPoints2.Text = "Найдено интересных точек(2): " + pointsB.Count;


            DrawPoints(pointsA, imageA, 1);
            DrawPoints(pointsB, imageB, 2);


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
        }

        private static List<AbstractDescriptor> getDescriptors(WrappedImage gradient,
            WrappedImage gradientAngle,
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


           //  siftDescriptors.ForEach(AbstractDescriptor::normalize()); // Переделать нормализацию !!!!!

             //  listNames.GroupBy(v => v).Where(g => g.Count() > 1).Select(g => g.Key)
            return siftDescriptors;
        }


        public void BuildPicture(List<PointsPair> pointPairs, WrappedImage inputImageA, WrappedImage inputImageB)
        {
            Bitmap picture;

            picture = WrappedImage.CombineImages(inputImageA, inputImageB);

            Graphics graph = Graphics.FromImage(picture);
            Color pen = Color.Blue;
            Pen penLine = new Pen(Color.Blue);

            foreach (var pointPair in pointPairs)
            {
                graph.FillEllipse(new SolidBrush(pen), pointPair.pointA.x, pointPair.pointA.y, 3, 3);
                graph.FillEllipse(new SolidBrush(pen), pointPair.pointB.x + (imageB.width + 20), pointPair.pointB.y, 3, 3);
                graph.DrawLine(penLine, pointPair.pointA.x, pointPair.pointA.y, pointPair.pointB.x + (imageB.width + 20), pointPair.pointB.y);
            }

            pictureBox1.Image = picture;

            picture.Save("..\\..\\..\\..\\Output\\OutputPicture.png");

        }


        public void DrawPoints(List<InterestingPoint> point, WrappedImage inputImage, int picture)
        {
            Bitmap image;
            image = Transformations.FromUInt32ToBitmap(inputImage.buffer);
            Graphics graph = Graphics.FromImage(image);
            Color pen = Color.Blue;

            foreach (var interestingPoint in point)
            {
                graph.FillEllipse(new SolidBrush(pen), interestingPoint.x, interestingPoint.y, 3, 3);
            }

            if (picture == 1) pictureBox1.Image = image;

            image.Save("..\\..\\..\\..\\Output\\OutputPicture.png");
        }


    }
}
