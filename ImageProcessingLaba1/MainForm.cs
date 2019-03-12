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


        public struct RGB
        {
            public int R;
            public int G;
            public int B;
        };

        public RGB[,] BitmapToByteRgbNaive(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            RGB[,] res = new RGB[height, width];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Color color = bmp.GetPixel(x, y);
                    res[y, x].R = color.R;
                    res[y, x].G = color.G;
                    res[y, x].B = color.B;
                }
            }
            return res;
        }

        public Bitmap RgbToBitmap(RGB [,] pixel)
        {
            Bitmap bmp;
            int height = pixel.GetLength(1);
            int width = pixel.GetLength(0);

            RGB[,] res = new RGB[height, width];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    pixel = image.GetPixel(x, y);
                    bmp.SetPixel(x, y, pixel);
                }
            }
            return bmp;
        }


        public void ByteWriteFile(RGB [,] pixel, int H, int W)
        {
            StreamWriter sw = new StreamWriter("pass.txt");
            string s1 = " ";
                for (int y = 0; y < H; y++)
                {
                    s1 = "";
                    for (int x = 0; x < W; x++)
                    {
                        s1 += "(" + pixel[y, x].R + " ";
                        s1 += pixel[y, x].G + " ";
                        s1 += pixel[y, x].B + ") " + " ";
                }
                    s1 += "\n";
                    sw.Write(s1);
                }
                s1 += "\n";      
            sw.Close();
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

        public RGB[,] ConvertToGray(RGB [,] pixel)
        {



            return pixel;
        }



        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    full_name_of_image = open_dialog.FileName;
                    image = new Bitmap(open_dialog.FileName);

                    RGB[,] pixel;

                    this.pictureBox1.Size = image.Size;
                    this.pictureBox2.Size = image.Size;

                    this.Height = this.pictureBox2.Height + 75;


                    //получение матрицы с пикселями
                    pixel = new RGB[image.Height, image.Width];
                    pixel = BitmapToByteRgbNaive(image);


                    // Записываем рисунок в текстовый файл 
                    // ByteWriteFile(pixel, Height, Width);


                    pictureBox1.Image = image;
                    // pictureBox1.Invalidate();


                    pixel = ConvertToGray(pixel);


                    for (int y = 0; y < Height; ++y)
                    {
                        for (int x = 0; x < Width; ++x)
                        {
                             image.SetPixel(x, y, pixel);
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
            }
        }

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
    }
}
