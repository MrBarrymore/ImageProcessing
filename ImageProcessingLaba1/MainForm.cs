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
using System.Drawing.Imaging;

namespace ImageProcessingLaba1
{
    public partial class MainForm : Form
    {
        public static Bitmap image;
        public static string full_name_of_image = "\0";
        public static double[,] pixels;


        public MainForm()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            //....... для тестов
            /*
            image = new Bitmap("..\\..\\..\\..\\cube.jpg");
            pixels = new double[image.Height, image.Width];
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    Color p = image.GetPixel(x, y);
                    pixels[y, x] = p.R * 0.299 + p.G * .587 + p.B * 0.114;
                }

            SobelForm _sobelForm = new SobelForm(pixels);
            _sobelForm.ShowDialog();
            //////////////////////
            */
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region          
            
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    full_name_of_image = open_dialog.FileName;

                    image = new Bitmap(open_dialog.FileName);

                    pictureBox1.Image = image;
                   
                    //получение матрицы с пикселями
                    pixels = new double[image.Height, image.Width];
                    for (int y = 0; y < image.Height; y++)
                        for (int x = 0; x < image.Width; x++)
                        {
                            Color color = image.GetPixel(x, y);
                            pixels[y, x] = color.R * 0.299 + color.G * 0.587 + color.B * 0.114;
                        }
                }
                catch
                {
                    full_name_of_image = "\0";
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion

            #region Code for testing
            /*
            try
            {
                image = new Bitmap("..\\..\\..\\..\\Cat1.jpg");
                full_name_of_image = "******";
                
                // Выводим исходное изображение
                pictureBox1.Image = image;
                pictureBox1.Invalidate();

                //получение матрицы с пикселями
                pixels = new double[image.Height, image.Width];
                for (int y = 0; y < image.Height; y++)
                    for (int x = 0; x < image.Width; x++)
                    {
                        Color color = image.GetPixel(x, y);
                        pixels[y, x] = color.R * 0.299 + color.G * 0.587 + color.B * 0.114;
                    }
            }
            catch
            {
                full_name_of_image = "\0";
                DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            #endregion

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void операторСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SobelForm _sobelForm = new SobelForm(pixels);
            _sobelForm.ShowDialog();
        }

        private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GaussForm _gaussForm = new GaussForm(pixels);
            _gaussForm.ShowDialog();
        }
    }
}
