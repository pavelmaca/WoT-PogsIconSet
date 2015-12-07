using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.IO;
using Phobos.WoT;

namespace WotPogsIconSet
{/*
    public abstract class IconGenerator
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public IconGenerator()
        {
            this.Width = 80;
            this.Height = 24;
        }

        public virtual void Generate(string iconPath, TankStats tankStats)
        {
            using (Bitmap bImage = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb))
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

                    g.Flush();

                    string outputFile = Path.Combine(iconPath, tankStats.FileName);
                    image.Save(outputFile, ImageFormat.Png);

                    GenerateChildSets(image, g, tankStats);
                }
            }
        }

        public virtual void GenerateSets(Image image, Graphics g, TankStats tankStats)
        {
            Generators.Icon i = new Colored();
            return;
        }
    }*/
}