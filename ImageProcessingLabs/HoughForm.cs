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
using ImageProcessingLabs.Helper;
using ImageProcessingLabs.Properties;
using ImageProcessingLabs.Transformation;

namespace ImageProcessingLabs
{
    public partial class HoughForm : Form
    {
        public const string PathToReadImage = "..\\..\\..\\input\\";
        private const string PathToWriteImage = "..\\..\\..\\output\\";

        private readonly List<Tuple<string, string>> picSuit = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("milk1.jpg", "milk.jpg"),
            new Tuple<string, string>("hough-3-sample.jpg", "hough-3-full.jpg"),
            new Tuple<string, string>("hough-1-sample.jpg", "hough-1-full-1.jpg"),
            new Tuple<string, string>("hough-sample.jpg", "hough-full.jpg"),
        };

        private Bitmap pictureSample, pictureFull;

        public HoughForm()
        {
            InitializeComponent();

            InputSample_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            InputFull_PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            OutputPictureBoxMatches.SizeMode = PictureBoxSizeMode.Zoom;
            OutputPictureBoxVotes.SizeMode = PictureBoxSizeMode.Zoom;
            OutputPictureBoxResult.SizeMode = PictureBoxSizeMode.Zoom;

            pictureSample = new Bitmap(PathToReadImage + "hough-4-sample.jpg");
            pictureFull = new Bitmap(PathToReadImage + "hough-4-full.jpg");

            InputSample_pictureBox.Image = pictureSample;
            InputFull_PictureBox.Image = pictureFull;
        }

        private void FindPointButton_Click(object sender, EventArgs e)
        {
            var result = new Bitmap(PathToReadImage + "hough-4-full.jpg");
            string imageName = "hough-full";

            var imageSample = IOHelper.ImageToMat(pictureSample);
            var imageFull = IOHelper.ImageToMat(pictureFull);


            var hough = new Hough(imageSample, imageFull);
            var objects = hough.Find();


            foreach (var obj in objects)
            {
                DrawHelper.DrawPolygon(result, obj);
            }

            var matchImage = DrawHelper.DrawTwoImages(pictureSample, pictureFull, hough.ReverseMatches);

            matchImage.Save(PathToWriteImage + $"{imageName}_1matches.jpeg", ImageFormat.Jpeg);
            hough.VotesImage.Save(PathToWriteImage + $"{imageName}_2votes.jpeg", ImageFormat.Jpeg);
            result.Save(PathToWriteImage + $"{imageName}_3result.jpeg", ImageFormat.Jpeg);

            OutputPictureBoxMatches.Image = matchImage;
            OutputPictureBoxVotes.Image = hough.VotesImage;
            OutputPictureBoxResult.Image = result;
        }

    }
}
