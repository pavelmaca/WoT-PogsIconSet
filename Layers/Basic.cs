using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Layers
{
    public class Basic
    {
        /* public static Layer TankName = TankNameHandler;
         public static Layer GrandientBackground = TankNameHandler;

         public static Layer Premium = TankNameHandler;
         public static Layer Contour = TankNameHandler;
         public static Layer Tier = TankNameHandler;

         public static Layer TuretArmorFSR = TankNameHandler;
         public static Layer TuretArmorRSF = TankNameHandler;*/

        // Colors
        public readonly static Brush BRUSH_WHITE = Brushes.White;
        public readonly static Brush BRUSH_GOLD = new SolidBrush(Color.FromArgb(235, 215, 5));
        public readonly static Brush BRUSH_ORANGE = new SolidBrush(Color.FromArgb(248, 186, 114));

        public static Layer TankName = (Graphics g, TankStats tankStats) =>
        {
            String vehicleName = TankNameHelper.findShortName(tankStats, 42);
        
            TextHelpers.helperDrawFontDinamic(g, vehicleName, BRUSH_WHITE, 18, 2, 42);
        };

        public static Layer Shield = (Graphics g, TankStats tankStats) =>
        {
            using (Image shield = Image.FromFile(Properties.Settings.Default.imagesLocation + String.Format(@"\sheilds\{0}.png", tankStats.Nation)))
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
            TextHelpers.helperDrawFontNumbers(g, tankStats.Tier, BRUSH_WHITE, 9, 5, FontAlign.Center);
        };

        public static Layer ViewRange = (Graphics g, TankStats tankStats) =>
        {
            //this.DrawString(String.Format("{0:0.#}", tankStats.Hp), PogsFonts.font4, Pogs.colorOrange, x, y, FontAlign.Right);
            TextHelpers.helperDrawFont4px(g, tankStats.ViewRange.ToString(), BRUSH_ORANGE, 37, 17, FontAlign.Right);
        };

        public static Layer HealPoints = (Graphics g, TankStats tankStats) =>
        {
            TextHelpers.helperDrawFont4px(g, tankStats.Hp.ToString(), BRUSH_ORANGE, 37, 17, FontAlign.Right);
        };

        public static Layer ReloadTime = (Graphics g, TankStats tankStats) =>
        {
            float reloadTime = tankStats.IsUsingHe ? tankStats.HeGun.ReloadTime : tankStats.ApGun.ReloadTime;
            TextHelpers.helperDrawFont4px(g, String.Format("{0:0.#}", reloadTime), BRUSH_GOLD, 37, 10, FontAlign.Right);
        };

        public static Layer PenetrationOrDamage = (Graphics g, TankStats tankStats) =>
        {
            if (tankStats.Type == TankType.Spg)
            {
                DamageHelper(g, tankStats, BRUSH_ORANGE, 79, 2, FontAlign.Right);
                
            }
            else
            {
                PenetrationHelper(g, tankStats, BRUSH_ORANGE, 79, 2, FontAlign.Right);
            }
        };


        public static Layer PenetrationAndDamage = (Graphics g, TankStats tankStats) =>
        {
            PenetrationHelper(g, tankStats, BRUSH_ORANGE, 79, 2, FontAlign.Right);
            DamageHelper(g, tankStats, BRUSH_ORANGE, 79, 10, FontAlign.Right);
        };

        public static Layer ContourIcon = (Graphics g, TankStats tankStats) =>
        {
            using (Image original = Image.FromFile(Path.Combine(Properties.Settings.Default.contourLocation, tankStats.FileName)))
            {
                if (original == null)
                {
                    Console.WriteLine("No contour image for tank:" + tankStats.Name+"\n");
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


        // Setup

        protected static Image premiumStar;
        protected static Dictionary<TankType, Color[]> bgColors = new Dictionary<TankType, Color[]>(5);

        static Basic()
        {

            premiumStar = Image.FromFile(Path.Combine(Properties.Settings.Default.imagesLocation, "star.png"));
        }

        protected static void PenetrationHelper(Graphics g, TankStats tankStats, Brush brush,  int x, int y, FontAlign align)
        {

            int text = tankStats.IsUsingHe ? tankStats.HeGun.HePenetration : tankStats.ApGun.ApPenetration; 
            TextHelpers.helperDrawFont4px(g, text.ToString(), brush, x, y, align);
        }

        protected static void DamageHelper(Graphics g, TankStats tankStats, Brush brush, int x, int y,FontAlign align)
        {
            int text = tankStats.IsUsingHe ? tankStats.HeGun.HeDamage : tankStats.ApGun.ApDamage;
            TextHelpers.helperDrawFont4px(g, text.ToString(), brush, x, y, align);
        }

    }
}
