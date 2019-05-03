using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageProcessingLabs.Converter;

namespace ImageProcessingLabs.Helper
{
    class IOHelper
    {
        public static Mat ImageToMat(Bitmap image)
        {
            var mat = new Mat(image.Width, image.Height);
            for (var x = 0; x < mat.Width; x++)
            for (var y = 0; y < mat.Height; y++)
                mat.Set(x, y, ColorsConverter.RgbToGreyscale(image.GetPixel(x, y)));

            return mat;
        }

        public static Bitmap MatToImage(Mat img)
        {
            img.Normalize();

            var image = new Bitmap(img.Width, img.Height);

            for (var x = 0; x < image.Width; x++)
            for (var y = 0; y < image.Height; y++)
                image.SetPixel(x, y, ColorsConverter.GreyscaleToRgb(img.GetAt(x, y)));

            return image;
        }

        public static void WriteMatToFile(Mat mat, string filePath)
        {
            WriteImageToFile(MatToImage(mat), filePath);
        }

        public static void WriteImageToFile(System.Drawing.Image image, string filePath)
        {
            filePath = filePath + ".jpeg";
            image.Save(filePath, ImageFormat.Jpeg);
        }

        public static void DeletePictures()
        {
            DirectoryInfo dirInfo = new DirectoryInfo("..\\..\\..\\..\\Output\\");
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }


    }
}
