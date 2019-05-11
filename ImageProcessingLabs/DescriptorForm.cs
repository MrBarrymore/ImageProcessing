using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using ImageProcessingLabs.Blobs;
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.ForDescriptor;
using ImageProcessingLabs.enums;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Scale;
using ImageProcessingLabs.Helper;
using ImageProcessingLabs.Properties;
using ImageProcessingLabs.Transformation;


namespace ImageProcessingLabs
{
    public partial class DescriptorForm : Form
    {
        private Mat imageA, imageB;
        private static int POINTS = 100;
        private static double MinValueHarris = 0.05;
        private static int WindowSize = 5;

        public const string PathToReadImage = "..\\..\\..\\input\\";
        public const string PathToWriteImage = "..\\..\\..\\output\\";

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

            List<InterestingPoint> pointsA =
                NonMaximumSuppression.FilterA(imageA, Harris.DoHarris(MinValueHarris, WindowSize, imageA), maxPoints);
            List<InterestingPoint> pointsB =
                NonMaximumSuppression.FilterA(imageB, Harris.DoHarris(MinValueHarris, WindowSize, imageB), maxPoints);

            List<ForDescriptor.Descriptor> descriptorsA = RotationInvariant.Calculate(imageA, pointsA);
            List<ForDescriptor.Descriptor> descriptorsB = RotationInvariant.Calculate(imageB, pointsB);

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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IOHelper.DeletePictures();
            MinValueHarris = Convert.ToDouble(txb_minValue.Text);
            WindowSize = Convert.ToInt32(txb_WindowSize.Text);
            int maxPoints = Convert.ToInt32(txb_Filter.Text);
            int gridSize = Convert.ToInt32(txb_gridSize.Text);
            int cellSize = Convert.ToInt32(txb_cellSize.Text);
            int binsCount = Convert.ToInt32(txb_binsCount.Text);

            if (filter_checkBox.Checked == true) maxPoints = Convert.ToInt32(txb_Filter.Text);
            else maxPoints = 5000;

            List<ForDescriptor.Descriptor> descriptorsA = BlobsFinder.FindBlobs(imageA, maxPoints);
            List<ForDescriptor.Descriptor> descriptorsB = BlobsFinder.FindBlobs(imageB, maxPoints);

            List<ValueTuple<ForDescriptor.Descriptor, ForDescriptor.Descriptor>> match;

            if (rbt_usual.Checked == true) match = DescriptorMatcher.Match(descriptorsA, descriptorsB);
            else if (rbt_NNDR.Checked == true) match = DescriptorMatcher.Nndr(descriptorsA, descriptorsB);
            else match = DescriptorMatcher.Match(descriptorsA, descriptorsB);

            lbl_findPoints1.Text = "Найдено интересных точек(1): " + descriptorsA.Count;
            lbl_findPoints2.Text = "Найдено интересных точек(2): " + descriptorsB.Count;
            lbl_PairCount.Text = "Найдено пар точек: " + match.Count;

             var image = DrawHelper.DrawTwoImages(
              DrawHelper.DrawPoints(imageA, descriptorsA), DrawHelper.DrawPoints(imageB, descriptorsB), match);

          //  var image = DrawHelper.DrawPoints(imageA, descriptorsA);

            IOHelper.WriteImageToFile(image, "..\\..\\..\\..\\Output\\OutputPicture.png");

            pictureBox1.Image = image;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int maxPoints = Convert.ToInt32(txb_Filter.Text);

            string name = PathToWriteImage + "resultPicture.jpg";

            Bitmap bmp1 = new Bitmap(PathToReadImage + "bridge1.jpg");
            Bitmap bmp2 = new Bitmap(PathToReadImage + "bridge2.jpg");

            var descriptorsABlobs = BlobsFinder.FindBlobs(IOHelper.ImageToMat(bmp1), maxPoints);
            var descriptorsBBlobs = BlobsFinder.FindBlobs(IOHelper.ImageToMat(bmp2), maxPoints);
            var match = DescriptorMatcher.Nndr(descriptorsABlobs, descriptorsBBlobs);

            lbl_findPoints1.Text = "Найдено интересных точек(1): " + descriptorsABlobs.Count;
            lbl_findPoints2.Text = "Найдено интересных точек(2): " + descriptorsBBlobs.Count;
            lbl_PairCount.Text = "Найдено пар точек: " + match.Count;

            var (matrixA, matrixB) = Ransac.CalculateTransform(match);
            var result = Transformer.Transform(bmp1, bmp2, matrixA, matrixB);

            result.Save(name, ImageFormat.Jpeg);

            pictureBox1.Image = result;
        }


    }
}
