using Phobos.WoT;
using System.Drawing;
using WotPogsIconSet.Icons.Layers;

namespace WotPogsIconSet.Icons.Configs
{
    enum Part { ViewRange, ViewRangeShort, ViewRangeBonus, RealodTime, Healpoint, Penetration, Damage, PenDam, HullArmor, TuretArmor, TuretArmorSides }

    public abstract class Colors
    {
        public static Brush White = Brushes.White;
        public static Brush Gold = new SolidBrush(Color.FromArgb(235, 215, 5));
        public static Brush Orange = new SolidBrush(Color.FromArgb(248, 186, 114));
    }

    public abstract class Positions
    {
        public static int Row1 = 2;
        public static int Row2 = 10;
        public static int Row3 = 17;

        public static int Col1 = 37;
        public static int Col2 = 79;
    }

    public abstract class Config
    {
        public int x;
        public int y;
        public Brush color;
    }

    public class Hp : Config {}
    public class ViewRange : Config { }

    public class HullArmor : Config {
        public enum Order { FSR, RSF};

        public Order order;
        public string format(TankStats tankStats)
        {
            switch (this.order)
            {
                case Order.FSR: return tankStats.HullFront + "*" + tankStats.HullSides + "*" + tankStats.HullBack;
                case Order.RSF: return tankStats.HullBack + "*" + tankStats.HullSides + "*" + tankStats.HullFront;
                default: return null;
            }
        }
    }

    public class TurretArmor : Config
    {
        public enum Order { FSR, RSF, S };

        public Order order;
        public string format(TankStats tankStats)
        {
            switch (this.order)
            {
                case Order.FSR: return tankStats.TurretFront + "*" + tankStats.TurretSides + "*" + tankStats.TurretBack;
                case Order.RSF: return tankStats.TurretBack + "*" + tankStats.TurretSides + "*" + tankStats.TurretFront;
                case Order.S: return tankStats.TurretSides.ToString();
                default: return null;
            }
        }
    }

    public class PenDam : Config {}

    public class Test
    {
        public static void genConfigs()
        {
            Config[] MAX = new Config[] {
            new Hp {
                color = Colors.White,
                x = Positions.Col1,
                y = Positions.Row3,
            },
            new HullArmor
            {
                color = Colors.White,
                x = Positions.Col2,
                y = Positions.Row3,
                order = HullArmor.Order.RSF,
            },
            new TurretArmor
            {
                color = Colors.White,
                x = Positions.Col2,
                y = Positions.Row2,
                order = TurretArmor.Order.RSF,
            },
            new PenDam
            {
                color = Colors.White,
                x = Positions.Col2,
                y = Positions.Row1,
            }
            };

        }

    }

}

