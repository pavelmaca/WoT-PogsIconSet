using Phobos.WoT;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using WotPogsIconSet.Layers;
using WotPogsIconSet.Utils;
using System.IO;

namespace WotPogsIconSet
{
    public class Icon : IDisposable
    {
        public const int WIDTH = 80;
        public const int HEIGHT = 24;

        protected Graphics g;
        protected Bitmap image;

        public Icon()
        {
            // create new image
            image = new Bitmap(WIDTH, HEIGHT, PixelFormat.Format32bppArgb);
            prepareGraphics();
        }

        public Icon(string parentFile)
        {
            // use existing image
            if (!File.Exists(parentFile))
            {
                throw new Exception("Výchozí vrstva neexistuje.");
            }

            image = new Bitmap(ImageTools.loadFromFile(parentFile));
            prepareGraphics();
        }

        protected void prepareGraphics()
        {
            image.SetResolution(72.0f, 72.0f); // fix DPI

            g = Graphics.FromImage(image);
            g.PageUnit = GraphicsUnit.Pixel;
            g.SmoothingMode = SmoothingMode.None;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            g.TextContrast = 0;
        }

        public void Save(string outputFile)
        {
            g.Flush();
            image.Save(outputFile, ImageFormat.Png);
        }

        public void Apply(Layer layer, TankStats tankStats)
        {
            try
            {
                layer(g, tankStats);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during appling layer: " + layer.Method);
                Console.WriteLine(e.Message + "\n");
            }
        }

        public void Dispose()
        {
            g.Dispose();
            image.Dispose();
        }
    }
}
