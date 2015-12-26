using Phobos.WoT;
using System.Drawing;
using WotPogsIconSet.Fonts;

namespace WotPogsIconSet.Layers
{
    public class Armor
    {
        const int MAX_WIDTH = 41;

        public static Layer TurretArmorFSR = (Graphics g, TankStats tankStats) =>
        {
            if (!tankStats.IsTurretInternal)
            {
                string text = tankStats.TurretFront + "*" + tankStats.TurretSides + "*" + tankStats.TurretBack;

                TextHelpers.helperDrawFontDinamic(g, text, Basic.BRUSH_WHITE, 79, 10, MAX_WIDTH, FontAlign.Right);
            }
        };

        public static Layer TurretArmorRSF = (Graphics g, TankStats tankStats) =>
        {
            if (!tankStats.IsTurretInternal)
            {
                string text = tankStats.TurretBack + "*" + tankStats.TurretSides + "*" + tankStats.TurretFront;

                TextHelpers.helperDrawFontDinamic(g, text, Basic.BRUSH_WHITE, 79, 10, MAX_WIDTH, FontAlign.Right);
            }
        };

        public static Layer HullArmorFSR = (Graphics g, TankStats tankStats) =>
        {

            string text = tankStats.HullFront + "*" + tankStats.HullSides + "*" + tankStats.HullBack;
            TextHelpers.helperDrawFontDinamic(g, text, Basic.BRUSH_WHITE, 79, 17, MAX_WIDTH, FontAlign.Right);
        };

        public static Layer HullArmorRSF = (Graphics g, TankStats tankStats) =>
        {
            string text = tankStats.HullBack + "*" + tankStats.HullSides + "*" + tankStats.HullFront;
            TextHelpers.helperDrawFontDinamic(g, text, Basic.BRUSH_WHITE, 79, 17, MAX_WIDTH, FontAlign.Right);
        };

        // turret[·1·]" 59 10 255 255 255 4
        public static Layer TurretArmorFront = (Graphics g, TankStats tankStats) =>
        {
            if (!tankStats.IsTurretInternal)
            {
                string text = tankStats.TurretFront.ToString();
                TextHelpers.helperDrawFont4px(g, text, Basic.BRUSH_WHITE, 59, 10, FontAlign.Right);
            }
        };


    }
}
