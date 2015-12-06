using System;
using System.Drawing;
using System.IO;
using Phobos.WoT;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Generators
{
    public class MaxFsrVrRld : Colored
    {

        public MaxFsrVrRld(bool useTransparentBackground = false)
        {
        }

        protected override void DrawIcon(Graphics g, TankStats tank)
        {
            // Background.
            this.DrawBackground(g, tank);

            this.DrawContour(g, tank);

            // Tier
            this.DrawString(g, tank.Tier.ToString(), PogsFonts.fontNumbers, colorWhite, 9, 5, FontAlign.Center);
            //PogsFontRenderer.drawText(PogsFonts.fontNumbers, g, 9, 5, tank.Tier.ToString(), colorWhite, FontAlign.Center);


            // tank name
            this.DrawString(g, ShortNames.findShortName(tank), PogsFonts.font4, colorWhite, 19, 2);
            //PogsFontRenderer.drawText(PogsFonts.font4, g, 19, 2, ShortNames.findShortName(tank), colorWhite, FontAlign.Left);

            DrawStats(g, tank);
        }

        protected void DrawStats(Graphics g, TankStats tank)
        {
            int[] collumns = new int[] { 37, 79 };
            int[] rows = new int[] {2, 10,17};

            int maxSecundCollumnsSize = this.Width - (this.Width - collumns[1]) - collumns[0] - 2;

            // reaload time
            float reloadTime = tank.IsUsingHe ? tank.HeGun.ReloadTime : tank.ApGun.ReloadTime;
            this.DrawString(g, String.Format("{0:0.#}", reloadTime), PogsFonts.font4, colorGold, collumns[0], rows[1], FontAlign.Right);

            // view range
            this.DrawString(g, tank.ViewRange.ToString(), PogsFonts.font4, colorOrange, collumns[0], 17, FontAlign.Right);

            // hull armor 
            this.DrawString(g, tank.HullFront + "*" + tank.HullSides + "*" + tank.HullBack, PogsFonts.font4, colorWhite, collumns[1], rows[2], maxSecundCollumnsSize, PogsFonts.font3, FontAlign.Right);
          
            // turret armor
            if (!tank.IsTurretInternal)
            {
                this.DrawString(g, tank.Turret, PogsFonts.font4, colorWhite, collumns[1], rows[1], maxSecundCollumnsSize, PogsFonts.font3, FontAlign.Right);
            }

            // max penetration or dmg (for arty)
            // TODO: test
            string penOrDmg;
            if (tank.Type == TankType.Spg)
            {
                //penetration and damage if arty
                penOrDmg = tank.IsUsingHe ? tank.HeGun.HeDamage.ToString() : tank.ApGun.ApDamage.ToString();
            }
            {
                penOrDmg = tank.IsUsingHe ? tank.HeGun.HePenetration.ToString() : tank.ApGun.ApPenetration.ToString();
            }
            this.DrawString(g, penOrDmg, PogsFonts.font4, colorOrange, collumns[1], rows[0], FontAlign.Right);
        }

        protected virtual void DrawBackground(Graphics g, TankStats tank)
        {
            // Grandient background
            for (int y = 0; y < bgColors[tank.Type].Length; y++)
            {
                Pen p = new Pen(bgColors[tank.Type][y]);
                g.DrawLine(p, 0, y, this.Width, y);
            }

            // Name header
            g.DrawImageUnscaled(stripe, 0, 0);

            // Shield
            g.DrawImageUnscaled(Shield[tank.Nation], 1, 2);

            // Premium
            if (tank.IsPremium)
            {
                g.DrawImageUnscaled(premiumStar, 4, 14);
            }
        }

        protected virtual void DrawContour(Graphics g, TankStats tank)
        {
            try
            {
                using (Image original = Image.FromFile(Path.Combine(PogsIcons.Properties.Settings.Default.contourLocation, tank.FileName)))
                {
                    // make original image darker
                    ImageTools.darkenImg(original, 0.2f);

                    // resize original image, when its bigger then availible space
                    float scale = original.Width / (float) original.Height; // s = w / h

                    int maxWidth = 62;
                    int maxHeight = 15;

                    int newWidth = original.Width;
                    int newHeight = original.Height;

                    if (newWidth > maxWidth) {
                        newWidth = maxWidth;
                        newHeight = (int) (newWidth / scale); // h = w / s
                    }

                    if (newHeight > maxHeight) {
                        newHeight = maxHeight;
                        newWidth = (int)(newHeight * scale);  // w = h * s
                    }
                            

                    // https://msdn.microsoft.com/en-us/library/system.drawing.image.getthumbnailimage%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                    /*Func<bool> ThumbnailCallback = () => { return false;  };
                    Image.GetThumbnailImageAbort myCallback =  new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    Image thumbnail = original.GetThumbnailImage(maxWidth, maxHeight, myCallback, IntPtr.Zero);
                    */

                    // put scaled conture image at "x = 18" and "y = y.MAX - newHeight + 1" (+1 for cut out bottom transparent pixels)
                    g.DrawImage(original, 18, this.Height - newHeight + 1, newWidth, newHeight);

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Contoure icon not found, error: " + e.Message);
            }
        }

    }
}

