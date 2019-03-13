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

        public MainForm()
        {
            InitializeComponent();
        }

        public static Bitmap image;
        public static string full_name_of_image = "\0";
        public static UInt32[,] pixels;


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region          
            /*
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    full_name_of_image = open_dialog.FileName;

                    image = new Bitmap(open_dialog.FileName);

                    RGB[,] pixels;

                    this.pictureBox1.Size = image.Size;
                    this.pictureBox2.Size = image.Size;

                    this.Height = this.pictureBox2.Height + 75;


                    //получение матрицы с пикселями
                    pixels = new RGB[image.Height, image.Width];
                    pixels = BitmapToByteRgb(image);


                    // Записываем рисунок в текстовый файл 
                    // ByteWriteFile(pixel, Height, Width);


                    pictureBox1.Image = image;
                    // pictureBox1.Invalidate();


                    pixels = ConvertToGray(pixels);


                    // Выводим обработанное изображение
                    for (int y = 0; y < Height; ++y)
                    {
                        UInt32 point;
                        for (int x = 0; x < Width; ++x)
                        {
                            point = 0xFF000000 | ((UInt32)pixels[y, x].R << 16) | ((UInt32)pixels[y, x].G << 8) | ((UInt32)pixels[y, x].B);

                            image.SetPixel(x, y, Color.FromArgb((int)point));
                        }
                    }

                    pictureBox2.Image = image;
                }
                catch
                {
                    full_name_of_image = "\0";
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }  */
            #endregion

            try
            {
                image = new Bitmap("C:\\Users\\Лаборатория\\Desktop\\Универ\\Интелектуальные технологии обработки изображений\\Л. Р. №1\\Figures.jpg");
                full_name_of_image = "******";
                // Выводим исходное изображение
                this.pictureBox1.Size = image.Size;
                this.Height = image.Height + 100;
                pictureBox1.Size = image.Size;
                pictureBox1.Image = image;
                pictureBox1.Invalidate();
                // this.Width = image.Width * 2 + 40;

                //получение матрицы с пикселями
                pixels = new UInt32[image.Height, image.Width];
                for (int y = 0; y < image.Height; y++)
                    for (int x = 0; x < image.Width; x++)
                        pixels[y, x] = (UInt32)(image.GetPixel(x, y).ToArgb());          
            }
            catch
            {
                full_name_of_image = "\0";
                DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void записатьВТекстовыйФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Записываем рисунок в текстовый файл
            RGB[,] RGBp = new RGB[image.Height, image.Width];
            RGBp = Transformations.FromUInt32ToRGB(pixels);

            Transformations.ByteWriteFile(RGBp, image.Height, image.Width);
        }

        /*
        public unsafe static byte[,,] BitmapToByteRgb(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            byte[,,] res = new byte[3, height, width];

            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            try
            {
                byte* curpos;
                for (int h = 0; h < height; h++)
                {
                    curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                    for (int w = 0; w < width; w++)
                    {
                        res[2, h, w] = *(curpos++);
                        res[1, h, w] = *(curpos++);
                        res[0, h, w] = *(curpos++);
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bd);
            }
            return res;
        }
        */

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                //string format = full_name_of_image.Substring(full_name_of_image.Length - 4, 4);
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


        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pixels = Filters.ConvertToGray(pixels);

            Bitmap Outimage2 = new Bitmap(pixels.GetLength(1), pixels.GetLength(0));

            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    Outimage2.SetPixel(x, y, Color.FromArgb((int)pixels[y, x]));
                }

            this.pictureBox2.Size = Outimage2.Size;
            pictureBox2.Image = Outimage2;
        }

        private void увеличитьРезкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (full_name_of_image != "\0")
            {
                pixels = Filters.matrix_filtration(image.Width, image.Height, pixels, Filters.N1, Filters.sharpness);

                image = Transformations.FromUInt32ToBitmap(pixels, image.Height, image.Width);
                pictureBox2.Image = image;
            }

        }

        private void операторСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SobelForm _sobelForm = new SobelForm(pixels);
            _sobelForm.ShowDialog();
        }
    }
}
