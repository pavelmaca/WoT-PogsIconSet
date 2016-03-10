using System.Collections.Generic;
using WotPogsIconSet.Layers;

namespace WotPogsIconSet
{
    public static class Configuration
    {
        public static IList<IconSet> getSet()
        {
            IList<IconSet> iconSet = new List<IconSet>();

            // RLD
            IconSet rld = new IconSet("RLD");
            rld.addLayer(Basic.ReloadTime);

            // HP
            IconSet hp = new IconSet("HP");
            hp.addLayer(Basic.HealPoints);
            hp.addVersion(rld);

            // VR
            IconSet vr = new IconSet("VR");
            vr.addLayer(Basic.ViewRange);
            vr.addVersion(rld);

            // RSF
            IconSet rsf = new IconSet("RSF");
            rsf.addLayer(Armor.TurretArmorRSF);
            rsf.addLayer(Armor.HullArmorRSF);
            rsf.addVersion(hp);
            rsf.addVersion(vr);

            // FSR
            IconSet fsr = new IconSet("FSR");
            fsr.addLayer(Armor.TurretArmorFSR);
            fsr.addLayer(Armor.HullArmorFSR);
            fsr.addVersion(hp);
            fsr.addVersion(vr);

            // MAX
            IconSet max = new IconSet("MAX");
            max.addLayer(Basic.PenetrationOrDamage);
            max.addVersion(rsf);
            max.addVersion(fsr);


            // ART DMG
            IconSet artDmg = getArtSet();
            artDmg.addVersion(hp);
            artDmg.addVersion(vr);

            // TAHTI
            //IconSet tahti = getTahti();

            // COLORED
            IconSet colored = new IconSet("COLOR");
            colored.addLayer(Colored.GrandientBackground);
            colored.addLayer(Colored.TankNameHeader);
            colored.addLayer(Vehicle.Shield);
            colored.addLayer(Vehicle.Premium);
            colored.addLayer(Vehicle.Tier);
            colored.addLayer(Vehicle.TankName);
            colored.addLayer(Vehicle.ContourIcon);
            colored.addVersion(max);
            colored.addVersion(artDmg);
            //colored.addVersion(tahti);

            // CLEAR
            IconSet clear = new IconSet("CLEAR");
            clear.addLayer(Clear.TransaprentBackground);
            clear.addLayer(Clear.ColoredTankNameHeader);
            clear.addLayer(Vehicle.Shield);
            clear.addLayer(Vehicle.Premium);
            clear.addLayer(Vehicle.Tier);
            clear.addLayer(Vehicle.TankName);
            clear.addLayer(Vehicle.ContourIcon);
            clear.addVersion(max);
            clear.addVersion(artDmg);
            //clear.addVersion(tahti);


            iconSet.Add(colored);
            iconSet.Add(clear);

            return iconSet;
        }

        public static IList<IconSet> getPogsMaxFSRVRRld()
        {
            IList<IconSet> iconSet = new List<IconSet>();

            IconSet colored = new IconSet("COLOR");
            colored.addLayer(Colored.GrandientBackground);
            colored.addLayer(Colored.TankNameHeader);
            colored.addLayer(Vehicle.Shield);
            colored.addLayer(Vehicle.Premium);
            colored.addLayer(Vehicle.Tier);
            colored.addLayer(Vehicle.TankName);
            colored.addLayer(Vehicle.ContourIcon);
            colored.addLayer(Basic.PenetrationOrDamage);
            colored.addLayer(Basic.ViewRange);
            colored.addLayer(Armor.TurretArmorFSR);
            colored.addLayer(Armor.HullArmorFSR);
            colored.addLayer(Basic.ReloadTime);
            iconSet.Add(colored);
            return iconSet;
        }


        static IconSet getArtSet()
        {
            IconSet dmgArt = new IconSet("DMG_ART");

            // Turret FRONT
            dmgArt.addLayer(Armor.TurretArmorFront);

            // PEN AND DAM
            dmgArt.addLayer(Basic.PenetrationAndDamage);

            // HULL FSR
            dmgArt.addLayer(Armor.HullArmorFSR);

            return dmgArt;
        }


        static IconSet getTahti()
        {
            // IList<IconSet> iconSet = new List<IconSet>();

            /*
-addStat "reload time"      35 10 235 215 5   4
addStat "short view range" 65 2  20  237 253 3
addStat "bonus view range" 65 2  242 234 1
-addStat "hull"             80 17 255 255 255 4
-addStat "turret"           80 10 255 255 255 4
-addStat "penetration" 80 2 248 186 114 4
-addStat "damage" 37 17 248 186 114 4
*/
            IconSet tahti = new IconSet("TAHTI");
            tahti.addLayer(Tahti.ReloadTime);
            tahti.addLayer(Armor.TurretArmorFSR);
            tahti.addLayer(Armor.HullArmorFSR);

            tahti.addLayer(Tahti.Damage);
            tahti.addLayer(Tahti.Penetration);

            tahti.addLayer(Tahti.ShortViewRange);
            tahti.addLayer(Tahti.BonusViewRange);

            return tahti;

            // iconSet.Add(tahti);

            // return iconSet;
        }

    }
}
