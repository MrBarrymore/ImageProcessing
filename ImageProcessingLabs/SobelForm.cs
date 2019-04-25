using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcessingLabs.Points;
using  ImageProcessingLabs.Wrapped;

namespace ImageProcessingLabs
{
    public partial class SobelForm : Form
    {
        public SobelForm()
        {
            InitializeComponent();
        }

        Bitmap picture;
        WrappedImage _image;

        public SobelForm(WrappedImage image)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            _image = (WrappedImage)image.Clone();
        }

        void СountGradient(double[,] MatrixX1, double[,] MatrixX2, double[,] MatrixY1, double[,] MatrixY2, BorderHandling Edgemode)
        {
            WrappedImage imageX;
            WrappedImage imageY;
            WrappedImage imageGradient = new WrappedImage(_image.height, _image.width);

            // Считаем частную производную по X (сепарабельно)
            imageX = CommonMath.GetSobelX(_image, Edgemode);
            picture = Transformations.FromUInt32ToBitmap(imageX.buffer);
            pictureBox1.Image = picture;

            // Считаем частную производную по Y (сепарабельно)
            imageY = CommonMath.GetSobelY(_image, Edgemode);
            picture = Transformations.FromUInt32ToBitmap(imageY.buffer);
            pictureBox2.Image = picture;


            // Вычисляем величину градиента
            imageGradient = WrappedImage.getGradient(imageX, imageY);
            picture = Transformations.FromUInt32ToBitmap(imageGradient.buffer);
            pictureBox3.Image = picture;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BorderHandling Edgemode = BorderHandling.Mirror;
            if (RB_Zero.Checked == true) Edgemode = BorderHandling.Black;
            if (RB_EdgeCoppy.Checked == true) Edgemode = BorderHandling.Copy;
            if (RB_EdgeReflection.Checked == true) Edgemode = BorderHandling.Wrap;
            if (RB_WrapImage.Checked == true) Edgemode = BorderHandling.Mirror;

            if (RB_Sobel.Checked == true)  СountGradient(SubelSepX1, SubelSepX2, SubelSepY1, SubelSepY2, Edgemode);
            if (RB_Pruitt.Checked == true) СountGradient(PruittSepX1, PruittSepX2, PruittSepY1, PruittSepY2, Edgemode);
            if (RB_Shchar.Checked == true) СountGradient(ShcharSepX1, ShcharSepX2, ShcharSepY1, ShcharSepY2, Edgemode);
            MainForm.picture = this.picture;
        }

        //Оператор собеля
        public const int NSob = 3;

        // Задание Оператора Собеля для сепорабельных вычислений
        public static double[,] SubelSepX1 = new double[1, NSob] { { 1, 2, 1 } };
        public static double[,] SubelSepX2 = new double[NSob, 1] { { 1 }, { 0 }, { -1 } };
        public static double[,] SubelSepY1 = new double[1, NSob] { { 1, 0, -1 } };
        public static double[,] SubelSepY2 = new double[NSob, 1] { { 1 }, { 2 }, { 1 } };


        // Задание Оператора Прюитта для сепорабельных вычислений
        public static double[,] PruittSepX1 = new double[1, NSob] { { 1, 1, 1 } };
        public static double[,] PruittSepX2 = new double[NSob, 1] { { 1 }, { 0 }, { -1 } };
        public static double[,] PruittSepY1 = new double[1, NSob] { { 1, 0, -1 } };
        public static double[,] PruittSepY2 = new double[NSob, 1] { { 1 }, { 1 }, { 1 } };

        // Задание Оператора Прюитта для сепорабельных вычислений
        public static double[,] ShcharSepX1 = new double[1, NSob] { { 3, 10, 3 } };
        public static double[,] ShcharSepX2 = new double[NSob, 1] { { 1 }, { 0 }, { -1 } };
        public static double[,] ShcharSepY1 = new double[1, NSob] { { 1, 0, -1 } };
        public static double[,] ShcharSepY2 = new double[NSob, 1] { { 3 }, { 10 }, { 3 } };

        private void SobelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  MainForm.Enabled = false;
        }
    }
}
