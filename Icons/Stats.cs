using Phobos.WoT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Icons.Layers;

namespace WotPogsIconSet.Icons
{
    public abstract class Stats : Icon
    {
        public Stats(TankStats tankStats, string iconPath) : base(tankStats, iconPath) { }

        protected void DrawRld(int x, int y)
        {
            float reloadTime = tankStats.IsUsingHe ? tankStats.HeGun.ReloadTime : tankStats.ApGun.ReloadTime;
            this.DrawString(String.Format("{0:0.#}", reloadTime), PogsFonts.font4, Pogs.colorGold, x, y, FontAlign.Right);
        }

        protected void DrawHp(int x, int y)
        {
            this.DrawString(String.Format("{0:0.#}", tankStats.Hp), PogsFonts.font4, Pogs.colorOrange, x, y, FontAlign.Right);
        }

        protected void DrawHullArmor(int x, int y, int maxWidth, ArmorOrder order = ArmorOrder.FSR)
        {
            string text;
            if (order == ArmorOrder.FSR)
            {
                text = tankStats.HullFront + "*" + tankStats.HullSides + "*" + tankStats.HullBack;
            }
            else
            {
                text = tankStats.HullBack + "*" + tankStats.HullSides + "*" + tankStats.HullFront;
            }

            this.DrawString(text, PogsFonts.font4, Pogs.colorWhite, x, y, maxWidth, PogsFonts.font3, FontAlign.Right);
        }

        protected void DrawTuretArmor(int x, int y, int maxWidth, ArmorOrder order, bool onlySide = false)
        {
            if (!tankStats.IsTurretInternal)
            {
                string text;
                if(onlySide == true)
                {
                    text = tankStats.TurretSides.ToString();
                }
                else if (order == ArmorOrder.FSR)
                {
                    text = tankStats.TurretFront + "*" + tankStats.TurretSides + "*" + tankStats.TurretBack;
                }
                else
                {
                    text = tankStats.TurretBack + "*" + tankStats.TurretSides + "*" + tankStats.TurretFront;
                }

                this.DrawString(text, PogsFonts.font4, Pogs.colorWhite, x, y, maxWidth, PogsFonts.font3, FontAlign.Right);
            }
        }

        protected void DrawPenDam(int x, int y)
        {
            // TODO: test both, if works ofr arty
            if (tankStats.Type == TankType.Spg)
            {
                DrawDamage(x, y);
            }
            else
            {
                DrawPenetration(x, y);
            }
        }

        protected void DrawPenetration(int x, int y)
        {
            int text = tankStats.IsUsingHe ? tankStats.HeGun.HePenetration : tankStats.ApGun.ApPenetration;
            this.DrawString(text.ToString(), PogsFonts.font4, Pogs.colorOrange, x, y, FontAlign.Right);
        }

        protected void DrawDamage(int x, int y)
        {
            int text = tankStats.IsUsingHe ? tankStats.HeGun.HeDamage : tankStats.ApGun.ApDamage;
            this.DrawString(text.ToString(), PogsFonts.font4, Pogs.colorOrange, x, y, FontAlign.Right);
        }
    }
}
