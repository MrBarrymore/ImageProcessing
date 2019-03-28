using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingLaba1
{
    public partial class GaussForm : Form
    {
        public GaussForm()
        {
            InitializeComponent();
        }

        Bitmap image, bufImage;
        static double[,] _pixels;
        double[,] sigmas;
        int Edgemode = 0;

        public GaussForm(double [,] pixels)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            _pixels = pixels;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (RB_Zero.Checked == true) Edgemode = 0;
            if (RB_EdgeCoppy.Checked == true) Edgemode = 1;
            if (RB_EdgeReflection.Checked == true) Edgemode = 2;
            if (RB_WrapImage.Checked == true) Edgemode = 3;
  
            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;
            int sigma = Convert.ToInt32(textBox1.Text);
            double[,] GaussMatrix = new double[sigma, sigma];
            GaussMatrix = CountGaussMatrix(GaussMatrix, sigma);

            double[,] Gausspixels = Filters.matrix_filtration(_pixels, GaussMatrix, Edgemode);
            image = Transformations.FromUInt32ToBitmap(Gausspixels);
            pictureBox2.Image = image;
        }

        double [,] CountGaussMatrix(double [,] gaussMatrix, double sigma) 
        {
            for (int y = 0; y < (int)sigma; y ++)
            {
                for (int x = 0; x < (int)sigma; x++)
                {
                    int x1 = x - (int)(sigma / 2);
                    int y1 = y - (int)(sigma / 2);
                    gaussMatrix[y, x] = (1 / (Math.PI * 2 * Math.Pow(sigma, 2)) ) * Math.Exp( - (Math.Pow(x1, 2) + Math.Pow(y1, 2)) / (2 * Math.Pow(sigma, 2)));
                }
            }
            return gaussMatrix;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (RB_Zero.Checked == true) Edgemode = 0;
            if (RB_EdgeCoppy.Checked == true) Edgemode = 1;
            if (RB_EdgeReflection.Checked == true) Edgemode = 2;
            if (RB_WrapImage.Checked == true) Edgemode = 3;

            int numOctaves = CountOctava(_pixels);
            double sigma0 = Convert.ToInt32(textBox3.Text);
            int levels = Convert.ToInt32(comboBox2.Text);
            sigmas = new double[numOctaves, levels]; // Массив значений сигм на каждом этапе преобразований

            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;

            image.Save("..\\..\\..\\..\\Output\\" + "Исходное изображение.png");

            // Строим пирамиду изображения
            for (int i = 0; i < numOctaves; i++)
            {
                double[,] Pyramidpixels = BuildNewLevel(_pixels.GetLength(0) / (int)(Math.Pow(2, i)), (int)(_pixels.GetLength(1) / (int)(Math.Pow(2, i))), i);

                double sigma = sigma0;

                for (int j = 0; j < levels; j++)
                {
                    sigma = Math.Pow((double)Math.Pow(2d, 1d / levels), j) * sigma0; // Пересчитываем сигму

                    double[,] GaussMatrix = new double[(int)sigma, (int)sigma];
                    GaussMatrix = CountGaussMatrix(GaussMatrix, sigma);
                    Pyramidpixels = Filters.matrix_filtration(Pyramidpixels, GaussMatrix, 1);

                    bufImage = Transformations.FromUInt32ToBitmap(Pyramidpixels);
                    bufImage.Save("..\\..\\..\\..\\Output\\" + "Октава " + i + " Уровень " + j + " Знач.сигма " + sigma + ".png");
                    sigmas[i, j] = sigma; // Записываем значения сигм на каждом этапе преобразований
                }

                image = Transformations.FromUInt32ToBitmap(Pyramidpixels);
                pictureBox2.Image = image;
            }

            // Задаем новые значения для элементов управления выводом изображений
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox3.Text = Convert.ToString(0);
            comboBox4.Text = Convert.ToString(0);
            for (int i = 0; i < numOctaves; i++) comboBox3.Items.Add(i);           
            for (int i = 0; i < levels; i++) comboBox4.Items.Add(i);

            // Заполняем выходные данные
            lbl_CountOctavas.Text = "Кол-во октав: " + Convert.ToString(numOctaves);
            lbl_LevelsInOctava.Text = "Уровней в октаве: " + Convert.ToString(levels);
        }

        private void btn_showImage_Click(object sender, EventArgs e)
        {
            showImage();
        }

        private void ShowNewImage(object sender, EventArgs e)
        {
            showImage();
        }

        void showImage()
        {
            if (checkBoxRealSize.Checked == true) pictureBox3.SizeMode = PictureBoxSizeMode.Normal;
            else pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                int i = Convert.ToInt32(comboBox3.Text);
                int j = Convert.ToInt32(comboBox4.Text);

                image = new Bitmap("..\\..\\..\\..\\Output\\" + "Октава " + (i) + " Уровень " + (j) + " Знач.сигма " + sigmas[i, j] + ".png");
                pictureBox3.Image = image;
                lbl_SigmaValue.Text = "Значение сигмы: " + Convert.ToString(sigmas[i, j]);
                lbl_EffectiveSigmaValue.Text = "Эффективное значение сигмы: " + sigmas[0, 0]* Math.Pow(2, i);
            }
            catch
            {
                MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int CountOctava(double [,] _pixels)
        {
            int octava = 0;

            while ((_pixels.GetLength(0) / Math.Pow(2, octava)) > 50 && (_pixels.GetLength(1) / Math.Pow(2, octava)) > 50)
            {
                octava++;
            }
            return octava;
        }

        static double [,] BuildNewLevel( int Width, int Height, int octava)
        {
            double[,] Pyramidpixels = new double[Width, Height];

            for (int y = 0; y < Pyramidpixels.GetLength(0); y++)
            {
                for (int x = 0; x < Pyramidpixels.GetLength(1); x++)
                {
                    Pyramidpixels[y, x] = _pixels[(int)(y * (Math.Pow(2, octava) )), (int)(x * (Math.Pow(2, octava) ))];
                }
            }
            return Pyramidpixels;
        }

    }
}
