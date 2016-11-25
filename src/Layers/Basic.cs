using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Layers
{
    public class Basic
    {
        // Colors
        public readonly static Brush BRUSH_WHITE = Brushes.White;
        public readonly static Brush BRUSH_GOLD = new SolidBrush(Color.FromArgb(235, 215, 5));
        public readonly static Brush BRUSH_ORANGE = new SolidBrush(Color.FromArgb(248, 186, 114));


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

       

        // Setup

        protected static Dictionary<TankType, Color[]> bgColors = new Dictionary<TankType, Color[]>(5);

        protected static void PenetrationHelper(Graphics g, TankStats tankStats, Brush brush, int x, int y, FontAlign align)
        {

            int text = tankStats.IsUsingHe ? tankStats.HeGun.HePenetration : tankStats.ApGun.ApPenetration;
            TextHelpers.helperDrawFont4px(g, text.ToString(), brush, x, y, align);
        }

        protected static void DamageHelper(Graphics g, TankStats tankStats, Brush brush, int x, int y, FontAlign align)
        {
            int text = tankStats.IsUsingHe ? tankStats.HeGun.HeDamage : tankStats.ApGun.ApDamage;
            TextHelpers.helperDrawFont4px(g, text.ToString(), brush, x, y, align);
        }

    }
}
