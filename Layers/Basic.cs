using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            TextHelpers.helperDrawFont4px(g, ShortNames.findShortName(tankStats), BRUSH_WHITE, 19, 2);
        };

        public static Layer Shield = (Graphics g, TankStats tankStat) =>
        {
            using (Image shield = ImageTools.loadFromFile(Properties.Settings.Default.imagesLocation + String.Format(@"\sheilds\{0}.png", tankStat.Nation)))
                g.DrawImageUnscaled(shield, 1, 2);
        };

    }
}
