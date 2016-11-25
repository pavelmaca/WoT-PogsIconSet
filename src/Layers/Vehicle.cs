using Phobos.WoT;
using System;
using System.Drawing;
using System.IO;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Layers
{
    public class Vehicle
    {
        // Layers

        public static Layer Shield = (Graphics g, TankStats tankStats) =>
        {
            g.DrawImageUnscaled(shield, 1, 2);
        };

        public static Layer Premium = (Graphics g, TankStats tankStats) =>
        {
            if (tankStats.IsPremium)
            {
                g.DrawImageUnscaled(premiumStar, 4, 14);
            }
        };

        public static Layer Tier = (Graphics g, TankStats tankStats) =>
        {
            TextHelpers.helperDrawFontNumbers(g, tankStats.Tier, Basic.BRUSH_WHITE, 9, 5, FontAlign.Center);
        };

        public static Layer ContourIcon = (Graphics g, TankStats tankStats) =>
        {
            using (Image original = Image.FromFile(Path.Combine(Properties.Settings.getResourcesLocation(), "contour", tankStats.FileName)))
            {
                if (original == null)
                {
                    Console.WriteLine("No contour image for tank:" + tankStats.Name + "\n");
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
                g.DrawImage(original, 18, Icon.HEIGHT - newHeight + 1, newWidth, newHeight);

            }
        };

        public static Layer TankName = (Graphics g, TankStats tankStats) =>
        {
            String vehicleName = TankNameHelper.findShortName(tankStats, 42);

            TextHelpers.helperDrawFontDinamic(g, vehicleName, Basic.BRUSH_WHITE, 18, 2, 42);
        };


        // Setup

        protected static Image premiumStar;
        protected static Image shield;

        static Vehicle()
        {

            premiumStar = Image.FromFile(Path.Combine(Properties.Settings.getResourcesLocation(), @"images\star.png"));
            shield = Image.FromFile(Path.Combine(Properties.Settings.getResourcesLocation(), @"images\sheild.png"));
        }
    }
}
