using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phobos.WoT;
using WotPogsIconSet;
using WotPogsIconSet.Utils;
using WotPogsIconSet.Fonts;
using System.IO;
using WotPogsIconSet.Icons.Layers;

namespace WotPogsIconSet.Icons
{
    public abstract class Pogs : Icon
    {
        protected static Dictionary<string, Image> shield = new Dictionary<string, Image>(6, StringComparer.InvariantCultureIgnoreCase);
        protected static Image premiumStar;

        public static Brush colorWhite = Brushes.White;
        public static Brush colorGold = new SolidBrush(Color.FromArgb(235, 215, 5));
        public static Brush colorOrange = new SolidBrush(Color.FromArgb(248, 186, 114));

        static Pogs()
        {
            foreach (string nation in ItemDatabase.Nations)
            {
                shield[nation] = ImageTools.loadFromFile(Properties.Settings.Default.imagesLocation + String.Format(@"\sheilds\{0}.png", nation));
            }
            premiumStar = ImageTools.loadFromFile(Properties.Settings.Default.imagesLocation + @"\star.png");
        }

        public Pogs(TankStats tankStats, string iconPath) : base(tankStats, iconPath) { }
        public Pogs(TankStats tankStats) : base(tankStats) { }

        protected override void Draw()
        {
            // Shield
            this.DrawShield();

            // Premium
            this.DrawPremium();

            // Contour
            this.DrawContour();

            // Tier
            this.DrawString(tankStats.Tier.ToString(), PogsFonts.fontNumbers, colorWhite, 9, 5, FontAlign.Center);

            // Tank name
            this.DrawString(ShortNames.findShortName(tankStats), PogsFonts.font4, colorWhite, 19, 2);
        }

        protected void DrawShield()
        {
            g.DrawImageUnscaled(shield[tankStats.Nation], 1, 2);
        }

        protected void DrawPremium()
        {
            if (tankStats.IsPremium)
            {
                g.DrawImageUnscaled(premiumStar, 4, 14);
            }
        }

        protected void DrawContour()
        {
            using (Image original = ImageTools.loadFromFile(Path.Combine(Properties.Settings.Default.contourLocation, tankStats.FileName)))
            {
                if(original == null)
                {
                    Console.WriteLine("No contour image for tank:" + tankStats.Name);
                    return;
                }

                // make original image darker
                ImageTools.darkenImg(original, 0.2f);

                // resize original image, when its bigger then availible space
                float scale = original.Width / (float)original.Height; // s = w / h

                int maxWidth = 62;
                int maxHeight = 15;

                int newWidth = original.Width;
                int newHeight = original.Height;

                if (newWidth > maxWidth)
                {
                    newWidth = maxWidth;
                    newHeight = (int)(newWidth / scale); // h = w / s
                }

                if (newHeight > maxHeight)
                {
                    newHeight = maxHeight;
                    newWidth = (int)(newHeight * scale);  // w = h * s
                }

                // put scaled conture image at "x = 18" and "y = y.MAX - newHeight + 1" (+1 for cut out bottom transparent pixels)
                g.DrawImage(original, 18, this.height - newHeight + 1, newWidth, newHeight);

            }
        }
    }
}
