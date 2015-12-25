using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Layers
{
    public class Clear
    {
        public static Layer TransaprentBackground = (Graphics g, TankStats tankStats) =>
        {
            // make transparent background
            g.Clear(Color.Transparent);
        };

        public static Layer ColoredTankNameHeader = (Graphics g, TankStats tankStats) =>
        {
            g.DrawImageUnscaled(headerStripes[tankStats.Type], 0, 0);
        };


        // Setup

        protected static Dictionary<TankType, Image> headerStripes = new Dictionary<TankType, Image>(5);

        static Clear()
        {
            foreach (TankType type in Enum.GetValues(typeof(TankType)))
            {
                headerStripes[type] = Image.FromFile(Properties.Settings.Default.imagesLocation + String.Format(@"\stripes\{0}.png", type));
            }
        }

    }
}
