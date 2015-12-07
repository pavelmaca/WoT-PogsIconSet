using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Icons
{
    public class Clear : Pogs
    {
        protected static Dictionary<TankType, Image> headerStripes = new Dictionary<TankType, Image>(5);

        static Clear()
        {
            foreach (TankType type in Enum.GetValues(typeof(TankType)))
            {
                headerStripes[type] = ImageTools.loadFromFile(Properties.Settings.Default.imagesLocation + String.Format(@"\stripes\{0}.png", type));
            }
        }

        public Clear(TankStats tankStats, string iconPath) : base(tankStats, iconPath) { }
        public Clear(TankStats tankStats) : base(tankStats) { }

        protected void DrawHeaderBackground()
        {
            g.DrawImageUnscaled(headerStripes[tankStats.Type], 0, 0);
        }


        protected override void Draw()
        {
            // make transparent background
            g.Clear(Color.Transparent);

            // Name header
            this.DrawHeaderBackground();

            base.Draw();
        }
    }
}
