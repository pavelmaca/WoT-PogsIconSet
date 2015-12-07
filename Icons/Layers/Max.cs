using System;
using System.Drawing;
using System.IO;
using Phobos.WoT;
using WotPogsIconSet.Fonts;
using WotPogsIconSet.Utils;

namespace WotPogsIconSet.Icons.Layers
{
    public enum ArmorOrder { FSR, RSF }

    public enum Configs { FSR, RLD, VR, DMG, ART, MAX }

    public class Max : Stats
    {
        public Max(TankStats tankStats, string iconPath, Configs[] config) : base(tankStats, iconPath) {

            this.armorOrder = Array.LastIndexOf(config, Configs.FSR) != -1 ? ArmorOrder.FSR : ArmorOrder.RSF;

            this.vr = Array.LastIndexOf(config, Configs.VR) != -1;
            this.hp = ! this.vr;

            this.rld = Array.LastIndexOf(config, Configs.RLD) != -1;

            this.damage = this.penetration = Array.LastIndexOf(config, Configs.DMG) != -1; ;
            this.penDam = !this.damage;

            this.turetSideOnly = Array.LastIndexOf(config, Configs.DMG) != -1; ;
        }

        public bool hp = false;
        public ArmorOrder armorOrder = ArmorOrder.FSR;
        public bool penDam = false;
        public bool damage = false;
        public bool penetration = false;
        public bool rld = false;
        public bool vr = false;
        public bool turetSideOnly = false;

        protected override void Draw()
        {
            int[] collumns = new int[] { 37, 79 };
            int[] rows = new int[] { 2, 10, 17 };

            int maxSecundCollumnsSize = this.width - (this.width - collumns[1]) - collumns[0] - 2;

            //  penetration / max dmg for arty
            if (penDam) DrawPenDam(collumns[1], rows[0]);
            else if (damage && penetration)
            {
                DrawDamage(collumns[1], rows[0]);
                DrawPenetration(collumns[1], rows[1]);
            }


            // reaload time
            if (rld) DrawRld(collumns[0], rows[1]);

            // turret armor
            if(!turetSideOnly) DrawTuretArmor(collumns[1], rows[1], maxSecundCollumnsSize, this.armorOrder);
            else DrawTuretArmor(59, rows[1], 0, this.armorOrder, turetSideOnly);


            // HP
            if (hp) DrawHp(collumns[0], rows[2]);

            // view range
            if(vr) this.DrawString(tankStats.ViewRange.ToString(), PogsFonts.font4, Pogs.colorOrange, collumns[0], rows[2], FontAlign.Right);

            // hull armor 
            DrawHullArmor(collumns[1], rows[2], maxSecundCollumnsSize, this.armorOrder);
        }


    }
}

