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

namespace ImageProcessingLabs
{
    public partial class GaussForm : Form
    {
        public GaussForm()
        {
            InitializeComponent();
        }

        // Глобальные переменные 
        Bitmap image, bufImage;

        static List<double[,]> picturePiramid = new List<double[,]>();

        static double[,] _pixels;
        double[,] sigmas;
        int Edgemode = 0;

        public GaussForm(double[,] pixels)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            _pixels = pixels;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RB_Zero.Checked == true) Edgemode = 0;
            if (RB_White.Checked == true) Edgemode = 1;
            if (RB_EdgeCoppy.Checked == true) Edgemode = 2;
            if (RB_EdgeReflection.Checked == true) Edgemode = 3;
            if (RB_WrapImage.Checked == true) Edgemode = 4;

            int numOctaves = BuildingPyramid.CountOctava(_pixels);
            double sigmaA = Convert.ToDouble(textBox2.Text);
            double sigma0 = Convert.ToDouble(textBox3.Text);
            int levels = Convert.ToInt32(comboBox2.Text);
            sigmas = new double[numOctaves, levels + 1]; // Массив значений сигм на каждом этапе преобразований

            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;
            DeletePictures();
            image.Save("..\\..\\..\\..\\Output\\" + "Исходное изображение.png");


            buildPiramid(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), BuildingPyramid.CountOctava(_pixels), Convert.ToInt32(comboBox2.Text));

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

        public void buildPiramid( double sigmaA, double sigma0, int numOctaves, int levels)
        {
            double[,] GaussMatrix0 = ConvolutionMatrix.CountGaussMatrix(sigmaA);
            _pixels = ConvolutionMatrixFactory.processNonSeparable(_pixels, GaussMatrix0, Edgemode);
            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox2.Image = image;
            picturePiramid.Add(_pixels);

            // Строим пирамиду изображения
            for (int i = 0; i < numOctaves; i++)
            {
                double[,] Pyramidpixels;
                if (i == 0) Pyramidpixels = picturePiramid[0];
                else Pyramidpixels = BuildingPyramid.BuildNewLevel(picturePiramid[i]);

                double sigma = sigma0;
                for (int j = 0; j <= levels; j++)
                {
                    sigma = Math.Pow(Math.Pow(2d, 1d / levels), j) * sigma0; // Пересчитываем сигму

                    double[,] GaussMatrix;
                    GaussMatrix = ConvolutionMatrix.CountGaussMatrix(sigma);
                    Pyramidpixels = ConvolutionMatrixFactory.processNonSeparable(Pyramidpixels, GaussMatrix, Edgemode);

                    // Cохранение картинки
                    bufImage = Transformations.FromUInt32ToBitmap(Pyramidpixels);
                    bufImage.Save("..\\..\\..\\..\\Output\\" + "Октава " + i + " Уровень " + j + " Знач.сигма " + sigma + ".png");
                    sigmas[i, j] = sigma; // Записываем значения сигм на каждом этапе преобразований
                }
                picturePiramid.Add(Pyramidpixels);
            }

        }


        private void btn_showImage_Click(object sender, EventArgs e)
        {
            showImage();
        }

        private void ShowNewImage(object sender, EventArgs e)
        {
            showImage();
        }

        void DeletePictures()
        {
            DirectoryInfo dirInfo = new DirectoryInfo("..\\..\\..\\..\\Output\\");
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        void showImage()
        {
            if (checkBoxRealSize.Checked == true) pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
            else pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                int i = Convert.ToInt32(comboBox3.Text);
                int j = Convert.ToInt32(comboBox4.Text);

                image = new Bitmap("..\\..\\..\\..\\Output\\" + "Октава " + (i) + " Уровень " + (j) + " Знач.сигма " + sigmas[i, j] + ".png");
                pictureBox2.Image = image;
                lbl_SigmaValue.Text = "Значение сигмы: " + Convert.ToString(sigmas[i, j]);
                lbl_EffectiveSigmaValue.Text = "Эффективное значение сигмы: " + sigmas[0, 0] * Math.Pow(2, i);
            }
            catch
            {
                MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
