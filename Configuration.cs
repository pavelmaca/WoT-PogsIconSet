using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotPogsIconSet.Layers;

namespace WotPogsIconSet
{
    public static class Configuration
    {
        public static IList<IconSet> getPogsIconSets()
        {
            IList<IconSet> iconSet = new List<IconSet>();

            IconSet colored = new IconSet("COLOR");
            colored.Layers.Add(Colored.GrandientBackground);
            colored.Layers.Add(Basic.Shield);
            colored.Layers.Add(Basic.TankName);

            IconSet maxFsr = new IconSet("MAX_FSR");
            maxFsr.Layers.Add(Armor.TurretArmorFSR);

            IconSet max = new IconSet("MAX");
            max.Layers.Add(Armor.TurretArmorRSF);

            colored.Versions.Add(max);
            colored.Versions.Add(maxFsr);

            iconSet.Add(colored);
            return iconSet;
        }
    }
}
