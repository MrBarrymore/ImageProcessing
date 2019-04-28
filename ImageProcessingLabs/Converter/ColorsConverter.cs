using System.Drawing;

namespace ImageProcessingLabs.Converter
{
    class ColorsConverter
    {
        public static double RgbToGreyscale(Color color)
        {
            return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255.0;
        }

        public static Color GreyscaleToRgb(double greyscale)
        {
            var value = (int)(greyscale * 255);
            return Color.FromArgb(value, value, value);
        }

    }
}
