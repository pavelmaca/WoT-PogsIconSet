using System;
using System.Drawing;

namespace WotPogsIconSet.Utils
{
    public class ImageTools
    {

        public static void darkenImg(Image img, float factor)
        {
            if (factor == 1) return;

            if (factor > 1 || factor < 0)
            {
                throw new ArgumentOutOfRangeException("Valid factor is from 0 to 1");
            }


            using (Graphics g = Graphics.FromImage(img))
            using (Bitmap bImg = new Bitmap(img))
            {
                for (int x = 0; x < img.Width; x++)
                {
                    for (int y = 0; y < img.Height; y++)
                    {
                        Color c = bImg.GetPixel(x, y);
                        if (c.R != 0 || c.G != 0 || c.B != 0)
                        {
                            int dR = Convert.ToInt32(c.R * factor);
                            int dG = Convert.ToInt32(c.G * factor);
                            int dB = Convert.ToInt32(c.B * factor);
                            Color darker = Color.FromArgb(c.A, dR, dG, dB);
                            Brush br = new SolidBrush(darker);
                            g.FillRectangle(br, x, y, 1, 1);
                        }
                    }
                }
            }
        }

        public static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap target = new Bitmap(cropArea.Width, cropArea.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(img, new Rectangle(0, 0, target.Width, target.Height),
                                 cropArea,
                                 GraphicsUnit.Pixel);
            }

            return target;
        }
    }
}
