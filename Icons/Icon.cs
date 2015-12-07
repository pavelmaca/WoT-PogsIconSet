using Phobos.WoT;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Icons.Layers;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Icons
{
    public abstract class Icon : IDisposable
    {
        public int width { get; protected set; }
        public int height { get; protected set; }

        protected TankStats tankStats;
        protected Image image;
        protected Graphics g;

        abstract protected void Draw();

        public Icon(TankStats tankStats)
        {
            Initialize(tankStats);

            Bitmap bImage = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);

            bImage.SetResolution(72.0f, 72.0f); // fix DPI

            image = bImage;
            g = Graphics.FromImage(image);

            g.PageUnit = GraphicsUnit.Pixel;
            g.SmoothingMode = SmoothingMode.None;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            g.TextContrast = 0;

        }

        public Icon(TankStats tankStats, string iconPath)
        {
            this.image = ImageTools.loadFromFile(iconPath);
            this.g = Graphics.FromImage(image);

            Initialize(tankStats);
        }

        protected void Initialize(TankStats tankStats)
        {
            this.tankStats = tankStats;
            this.width = 80;
            this.height = 24;
        }

        public string Save(string iconPath)
        {
            try
            {
                this.Draw();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            g.Flush();

            string outputFile = Path.Combine(iconPath, tankStats.FileName);
            image.Save(outputFile, ImageFormat.Png);

            return outputFile;
        }

        public void Dispose()
        {
            this.g.Dispose();
            this.image.Dispose();
        }

        protected void DrawString(string text, PogsFont font, Brush brush, int x, int y, int maxWidth, PogsFont smallFont, FontAlign align = FontAlign.Left)
        {
            PogsFontRenderer.drawText(g, text, font, brush, x, y, align, maxWidth, smallFont);
        }

        protected void DrawString(string text, PogsFont font, Brush brush, int x, int y, FontAlign align = FontAlign.Left)
        {
            PogsFontRenderer.drawText(g, text, font, brush, x, y, align);
        }

    }
}
