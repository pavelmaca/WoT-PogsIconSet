using Phobos.WoT;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace WotPogsIconSet.Layers
{
    public class Colored
    {
        public static Layer GrandientBackground = (Graphics g, TankStats tankStats) =>
        {
            // Grandient background
            for (int y = 0; y < bgColors[tankStats.Type].Length; y++)
            {
                Pen p = new Pen(bgColors[tankStats.Type][y]);
                g.DrawLine(p, 0, y, Icon.WIDTH, y);
            }
        };

        public static Layer TankNameHeader = (Graphics g, TankStats tankStats) =>
        {
            // Name header
            g.DrawImageUnscaled(stripe, 0, 0);
        };


        // Setup

        protected static Image stripe;
        protected static Dictionary<TankType, Color[]> bgColors = new Dictionary<TankType, Color[]>(5);

        static Colored()
        {

            stripe = Image.FromFile(Path.Combine(Properties.Settings.getResourcesLocation(), @"images\stripe.png"));

            bgColors[TankType.Heavy] = new Color[24] {
                Color.FromArgb(123, 72, 36),  // 1
                Color.FromArgb(121, 67, 33),
                Color.FromArgb(117, 65, 32),
                Color.FromArgb(114, 63, 31),
                Color.FromArgb(110, 61, 30),
                Color.FromArgb(108, 60, 29),  // 6
                Color.FromArgb(105, 58, 29),
                Color.FromArgb(102, 57, 28),
                Color.FromArgb(98, 54, 27),
                Color.FromArgb(93, 51, 26),
                Color.FromArgb(90, 50, 25),  // 11
                Color.FromArgb(86, 49, 24),
                Color.FromArgb(81, 46, 23),
                Color.FromArgb(77, 43, 22),
                Color.FromArgb(72, 40, 20),
                Color.FromArgb(68, 38, 18),  //16
                Color.FromArgb(64, 35, 17),
                Color.FromArgb(59, 33, 16),
                Color.FromArgb(55, 31, 15),
                Color.FromArgb(51, 29, 14),
                Color.FromArgb(48, 27, 13),  //21
                Color.FromArgb(43, 24, 12),
                Color.FromArgb(39, 21, 11),
                Color.FromArgb(35, 20, 9)
            };

            bgColors[TankType.Light] = new Color[24] {
                Color.FromArgb(104, 102, 63),  // 1
                Color.FromArgb(104, 102, 63),
                Color.FromArgb(104, 102, 63),
                Color.FromArgb(104, 102, 63),
                Color.FromArgb(104, 102, 63),
                Color.FromArgb(103, 101, 62),  // 6
                Color.FromArgb(101, 99, 61),
                Color.FromArgb(98, 96, 59),
                Color.FromArgb(95, 93, 58),
                Color.FromArgb(92, 90, 56),
                Color.FromArgb(88, 87, 54),  // 11
                Color.FromArgb(85, 83, 51),
                Color.FromArgb(81, 79, 49),
                Color.FromArgb(77, 75, 47),
                Color.FromArgb(73, 71, 44),
                Color.FromArgb(69, 67, 42),  // 16
                Color.FromArgb(65, 63, 40),
                Color.FromArgb(61, 59, 37),
                Color.FromArgb(57, 56, 35),
                Color.FromArgb(53, 52, 33),
                Color.FromArgb(50, 49, 31),  // 21
                Color.FromArgb(47, 46, 29),
                Color.FromArgb(44, 43, 27),
                Color.FromArgb(42, 41, 26)
            };

            bgColors[TankType.Medium] = new Color[24] {
                Color.FromArgb(95, 125, 85),  // 1
                Color.FromArgb(94, 124, 84),
                Color.FromArgb(91, 122, 81),
                Color.FromArgb(88, 120, 78),
                Color.FromArgb(85, 118, 76),
                Color.FromArgb(81, 114, 74),  // 6
                Color.FromArgb(77, 111, 71),
                Color.FromArgb(74, 108, 68),
                Color.FromArgb(70, 105, 64),
                Color.FromArgb(66, 102, 61),
                Color.FromArgb(61, 99, 58),  // 11
                Color.FromArgb(56, 96, 54),
                Color.FromArgb(52, 93, 51),
                Color.FromArgb(47, 88, 46),
                Color.FromArgb(43, 85, 43),
                Color.FromArgb(39, 81, 39),  // 16
                Color.FromArgb(34, 78, 36),
                Color.FromArgb(31, 75, 34),
                Color.FromArgb(26, 72, 30),
                Color.FromArgb(22, 70, 27),
                Color.FromArgb(18, 66, 23),  // 21
                Color.FromArgb(14, 63, 20),
                Color.FromArgb(11, 60, 17),
                Color.FromArgb(6, 57, 14)
            };

            bgColors[TankType.Spg] = new Color[24] {
                Color.FromArgb(177, 34, 55),  // 1
                Color.FromArgb(174, 33, 54),
                Color.FromArgb(171, 33, 53),
                Color.FromArgb(167, 32, 52),
                Color.FromArgb(164, 31, 51),
                Color.FromArgb(158, 31, 49),  // 6
                Color.FromArgb(154, 30, 48),
                Color.FromArgb(149, 29, 47),
                Color.FromArgb(143, 28, 44),
                Color.FromArgb(139, 27, 43),
                Color.FromArgb(132, 25, 41),  // 11
                Color.FromArgb(126, 24, 39),
                Color.FromArgb(120, 23, 38),
                Color.FromArgb(113, 23, 36),
                Color.FromArgb(108, 21, 35),
                Color.FromArgb(101, 20, 32),  // 16
                Color.FromArgb(95, 19, 30),
                Color.FromArgb(90, 18, 28),
                Color.FromArgb(83, 16, 26),
                Color.FromArgb(77, 15, 24),
                Color.FromArgb(71, 14, 22),  // 21
                Color.FromArgb(64, 13, 20),
                Color.FromArgb(60, 12, 19),
                Color.FromArgb(53, 10, 17)
            };

            bgColors[TankType.TankDestroyer] = new Color[24] {
                Color.FromArgb(49, 104, 175),  // 1
                Color.FromArgb(40, 96, 170),
                Color.FromArgb(39, 93, 164),
                Color.FromArgb(38, 92, 163),
                Color.FromArgb(38, 90, 160),
                Color.FromArgb(37, 88, 155),  // 6
                Color.FromArgb(36, 86, 151),
                Color.FromArgb(34, 82, 145),
                Color.FromArgb(33, 79, 139),
                Color.FromArgb(32, 76, 135),
                Color.FromArgb(30, 73, 129),  // 11
                Color.FromArgb(29, 70, 124),
                Color.FromArgb(28, 67, 117),
                Color.FromArgb(27, 63, 111),
                Color.FromArgb(25, 60, 105),
                Color.FromArgb(23, 56, 98),  // 16
                Color.FromArgb(22, 52, 93),
                Color.FromArgb(21, 49, 87),
                Color.FromArgb(20, 46, 81),
                Color.FromArgb(19, 43, 75),
                Color.FromArgb(17, 39, 68),  // 21
                Color.FromArgb(15, 35, 62),
                Color.FromArgb(14, 33, 58),
                Color.FromArgb(12, 29, 52)
            };
        }

    }
}
