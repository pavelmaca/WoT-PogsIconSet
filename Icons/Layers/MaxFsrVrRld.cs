using System;
using System.Drawing;
using System.IO;
using Phobos.WoT;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Icons.Layers
{
    public class MaxFsrVrRld : Icon
    {
        public MaxFsrVrRld(TankStats tankStats, string iconPath) : base(tankStats, iconPath) { }

        protected override void Draw()
        {
            int[] collumns = new int[] { 37, 79 };
            int[] rows = new int[] {2, 10,17};

            int maxSecundCollumnsSize = this.width - (this.width - collumns[1]) - collumns[0] - 2;

            // reaload time
            float reloadTime = tankStats.IsUsingHe ? tankStats.HeGun.ReloadTime : tankStats.ApGun.ReloadTime;
            this.DrawString(String.Format("{0:0.#}", reloadTime), PogsFonts.font4, Pogs.colorGold, collumns[0], rows[1], FontAlign.Right);

            // view range
            this.DrawString(tankStats.ViewRange.ToString(), PogsFonts.font4, Pogs.colorOrange, collumns[0], 17, FontAlign.Right);

            // hull armor 
            this.DrawString(tankStats.HullFront + "*" + tankStats.HullSides + "*" + tankStats.HullBack, PogsFonts.font4, Pogs.colorWhite, collumns[1], rows[2], maxSecundCollumnsSize, PogsFonts.font3, FontAlign.Right);
          
            // turret armor
            if (!tankStats.IsTurretInternal)
            {
                this.DrawString(tankStats.Turret, PogsFonts.font4, Pogs.colorWhite, collumns[1], rows[1], maxSecundCollumnsSize, PogsFonts.font3, FontAlign.Right);
            }

            // max penetration or dmg (for arty)
            // TODO: test
            string penOrDmg;
            if (tankStats.Type == TankType.Spg)
            {
                //penetration and damage if arty
                penOrDmg = tankStats.IsUsingHe ? tankStats.HeGun.HeDamage.ToString() : tankStats.ApGun.ApDamage.ToString();
            }
            {
                penOrDmg = tankStats.IsUsingHe ? tankStats.HeGun.HePenetration.ToString() : tankStats.ApGun.ApPenetration.ToString();
            }
            this.DrawString(penOrDmg, PogsFonts.font4, Pogs.colorOrange, collumns[1], rows[0], FontAlign.Right);
        }

    }
}

