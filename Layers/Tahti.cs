using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotPogsIconSet.Fonts;

namespace WotPogsIconSet.Layers
{
    class Tahti
    {
        public static Layer ReloadTime = (Graphics g, TankStats tankStats) =>
        {
            float reloadTime = tankStats.IsUsingHe ? tankStats.HeGun.ReloadTime : tankStats.ApGun.ReloadTime;
            TextHelpers.helperDrawFont4px(g, String.Format("{0:0.#}", reloadTime), Basic.BRUSH_GOLD, 37, 10, FontAlign.Right);
        };

        public static Layer Penetration = (Graphics g, TankStats tankStats) =>
        {
            int text = tankStats.IsUsingHe ? tankStats.HeGun.HePenetration : tankStats.ApGun.ApPenetration;
            TextHelpers.helperDrawFont4px(g, text.ToString(), Basic.BRUSH_ORANGE, 79, 2, FontAlign.Right);
        };

        public static Layer Damage = (Graphics g, TankStats tankStats) =>
        {
            int text = tankStats.IsUsingHe ? tankStats.HeGun.HeDamage : tankStats.ApGun.ApDamage;
            TextHelpers.helperDrawFont4px(g, text.ToString(), Basic.BRUSH_ORANGE, 37, 17, FontAlign.Right);
        };

        public static Layer ShortViewRange = (Graphics g, TankStats tankStats) =>
        {
            int vr = (int)((400 - tankStats.ViewRange) / 10);
            if (vr > 0)
            {
                TextHelpers.helperDrawFont4px(g, vr.ToString(), new SolidBrush(Color.FromArgb(20, 237, 253)), 65, 2, FontAlign.Right);
            }
        };

        public static Layer BonusViewRange = (Graphics g, TankStats tankStats) =>
        {
            int vr = (int)(-1 * (400 - tankStats.ViewRange) / 10);
            if (vr > 0)
            {
                TextHelpers.helperDrawFont4px(g, vr.ToString(), new SolidBrush(Color.FromArgb(242, 234, 1)), 65, 2, FontAlign.Right);
            }
        };

    }
}
