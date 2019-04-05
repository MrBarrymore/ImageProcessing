using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingLabs
{
    public partial class SobelForm : Form
    {
        public SobelForm()
        {
            InitializeComponent();
        }

        Bitmap image;
        double[,] _pixels;
        MainForm _mainForm = new MainForm();

        public SobelForm(double[,] pixels)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            _pixels = pixels;

            this.Load += new EventHandler(button1_Click);
        }

        void СountGradient(double[,] MatrixX1, double[,] MatrixX2, double[,] MatrixY1, double[,] MatrixY2)
        {
            double[,] pixels1;
            double[,] pixels2;
            double[,] pixels3 = new double[_pixels.GetLength(0), _pixels.GetLength(1)];

            int Edgemode = 0;
            if (RB_Zero.Checked == true) Edgemode = 0;
            if (RB_EdgeCoppy.Checked == true) Edgemode = 1;
            if (RB_EdgeReflection.Checked == true) Edgemode = 2;
            if (RB_WrapImage.Checked == true) Edgemode = 3;

            // Считаем частную производную по X (сепарабельно)
            pixels1 = ConvolutionMatrixFactory.processNonSeparable(_pixels, MatrixX1, Edgemode);
            pixels1 = ConvolutionMatrixFactory.processNonSeparable(pixels1, MatrixX2, Edgemode);
            image = Transformations.FromUInt32ToBitmap(pixels1);
            pictureBox1.Image = image;

            // Считаем частную производную по Y (сепарабельно)
            pixels2 = ConvolutionMatrixFactory.processNonSeparable(_pixels, MatrixY1, Edgemode);
            pixels2 = ConvolutionMatrixFactory.processNonSeparable(pixels2, MatrixY2, Edgemode);
            image = Transformations.FromUInt32ToBitmap(pixels2);
            pictureBox2.Image = image;

            // Вычисляем величину градиента
            for (int y = 0; y < _pixels.GetLength(0); y++)
            {
                for (int x = 0; x < _pixels.GetLength(1); x++)
                {
                    pixels3[y, x] = Math.Sqrt(Math.Pow(pixels1[y, x], 2) + Math.Pow(pixels2[y, x], 2));
                }
            }
            image = Transformations.FromUInt32ToBitmap(pixels3);
            pictureBox3.Image = image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RB_Sobel.Checked == true) СountGradient(SubelSepX1, SubelSepX2, SubelSepY1, SubelSepY2);
            if (RB_Pruitt.Checked == true) СountGradient(PruittSepX1, PruittSepX2, PruittSepY1, PruittSepY2);
            if (RB_Shchar.Checked == true) СountGradient(ShcharSepX1, ShcharSepX2, ShcharSepY1, ShcharSepY2);
            MainForm.image = this.image;
            _mainForm.pictureBox2.Image = image;
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
