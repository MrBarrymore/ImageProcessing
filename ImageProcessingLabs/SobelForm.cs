using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.Points;
using ImageProcessingLabs.Wrapped;
using ImageProcessingLabs.enums;
using ImageProcessingLabs.Transformation;


namespace ImageProcessingLabs
{
    public partial class SobelForm : Form
    {
        private Mat _image;

        public SobelForm()
        {
            InitializeComponent();
        }

        public SobelForm(Mat image)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            _image = image.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BorderWrapType Edgemode = BorderWrapType.Mirror;
            if (RB_Zero.Checked == true) Edgemode = BorderWrapType.Black;
            if (RB_EdgeCoppy.Checked == true) Edgemode = BorderWrapType.Copy;
            if (RB_EdgeReflection.Checked == true) Edgemode = BorderWrapType.Wrap;
            if (RB_WrapImage.Checked == true) Edgemode = BorderWrapType.Mirror;

            Mat sobelMat = new Mat(_image.Width, _image.Height);
            Mat sobelMatX = new Mat(_image.Width, _image.Height);
            Mat sobelMatY = new Mat(_image.Width, _image.Height);

            if (RB_Sobel.Checked == true)
            {

                sobelMat = SobelHelper.Sobel(_image, sobelMatX, sobelMatY, Edgemode);
            }

            Bitmap picture;
            picture = Transformer.FromUInt32ToBitmap(sobelMatX);
            pictureBox1.Image = picture;

            picture = Transformer.FromUInt32ToBitmap(sobelMatY);
            pictureBox2.Image = picture;

            picture = Transformer.FromUInt32ToBitmap(sobelMat);
            pictureBox3.Image = picture;
        }

    }
}
