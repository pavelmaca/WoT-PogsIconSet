using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.IO;
using Phobos.WoT;
using WotPogsIconSet.Utils;
using WotPogsIconSet.Fonts;

namespace WotPogsIconSet
{
	public abstract class IconGenerator : IIConGenerator
	{
		public int Width { get; protected set; }
		public int Height { get; protected set; }

        public static Dictionary<string, Image> Shield = new Dictionary<string, Image>(6, StringComparer.InvariantCultureIgnoreCase);
        protected static Image premiumStar;
        protected static Image stripe;

        static IconGenerator()
        {
            try
            {
                // Load image sources

                foreach (string nation in ItemDatabase.Nations)
                {
                    Shield[nation] = Image.FromFile(PogsIcons.Properties.Settings.Default.imagesLocation + String.Format(@"\sheilds\{0}.png", nation));
                }

                premiumStar = Image.FromFile(PogsIcons.Properties.Settings.Default.imagesLocation + @"\star.png");

                stripe = Image.FromFile(PogsIcons.Properties.Settings.Default.imagesLocation + @"\stripe.png");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error during loading image sources.");
                Console.WriteLine(e.Message);
                throw e;
            }
        }

		public IconGenerator()
		{
			this.Width = 80;
			this.Height = 24;
		}

        protected void DrawString(Graphics g, string text, PogsFont font, Brush brush, int x, int y, int maxWidth, PogsFont smallFont, FontAlign align = FontAlign.Left)
        {
            PogsFontRenderer.drawText(g, text, font, brush, x, y, align, maxWidth, smallFont);
        }

        protected void DrawString(Graphics g, string text, PogsFont font, Brush brush, int x, int y, FontAlign align = FontAlign.Left)
        {
            PogsFontRenderer.drawText(g, text, font, brush, x, y, align);
        }

        public virtual void Generate(string iconPath, TankStats tankStats)
		{
            using (Bitmap bImage = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppRgb))
            {
                bImage.SetResolution(72.0f, 72.0f); // fix DPI

                using (Image image = bImage)
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.PageUnit = GraphicsUnit.Pixel;
                    g.SmoothingMode = SmoothingMode.None;
                    g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    g.TextContrast = 0;

                    this.DrawIcon(g, tankStats);


                    image.Save(Path.Combine(iconPath, tankStats.FileName), ImageFormat.Png);
                }
            }
        }

		protected abstract void DrawIcon(Graphics g, TankStats tankStats);
	}
}