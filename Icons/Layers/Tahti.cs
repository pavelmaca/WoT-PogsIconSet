using System;
using System.Drawing;
using System.IO;
using Phobos.WoT;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Icons.Layers
{
    public class Tahti : Stats
    {
        public Tahti(TankStats tankStats, string iconPath) : base(tankStats, iconPath) { }

        protected override void Draw()
        {
            int[] collumns = new int[] { 37, 79 };
            int[] rows = new int[] { 2, 10, 17 };

            int maxSecundCollumnsSize = this.width - (this.width - collumns[1]) - collumns[0] - 2;

            DrawDamage(37, 17);
            DrawPenetration(79, 2);

            // reaload time
            DrawRld(35,10);

            // turret armor
            DrawTuretArmor(79, 10, maxSecundCollumnsSize, ArmorOrder.FSR);

            // hull armor 
            DrawHullArmor(79, 17, maxSecundCollumnsSize, ArmorOrder.FSR);

            DrawShortVr(65, 2);
            DrawBonusVr(65, 2);
        }

        protected void DrawShortVr(int x, int y)
        {
            int vr = (int) ((400 - tankStats.ViewRange) / 10); 
            if(vr > 0) this.DrawString(vr.ToString(), PogsFonts.font4, new SolidBrush(Color.FromArgb(20, 237, 253)), x, y, 0, PogsFonts.font3, FontAlign.Right);

        }

        protected void DrawBonusVr(int x, int y)
        {
            int vr = (int)(-1 * (400 - tankStats.ViewRange) / 10);
            if (vr > 0)  this.DrawString(vr.ToString(), PogsFonts.font4, new SolidBrush(Color.FromArgb(242, 234, 1)), x, y, 0, PogsFonts.font3, FontAlign.Right);

        }

    }
}

