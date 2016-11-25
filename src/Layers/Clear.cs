using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
                string stripePath = Path.Combine(Properties.Settings.Default.srcLocation, @"images\stripes", String.Format(@"{0}.png", type));
                headerStripes[type] = Image.FromFile(stripePath);
            }
        }

    }
}
