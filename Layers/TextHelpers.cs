using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotPogsIconSet.Fonts;

namespace WotPogsIconSet.Layers
{
    public abstract class TextHelpers
    {
        public static void helperDrawFont4px(Graphics g, string text, Brush brush, int x, int y, FontAlign align = FontAlign.Left)
        {
            helperDrawFont(g, PogsFonts.font4, text, brush, x, y, align);
        }

        public static void helperDrawFont3px(Graphics g, string text, Brush brush, int x, int y, FontAlign align = FontAlign.Left)
        {
            helperDrawFont(g, PogsFonts.font3, text, brush, x, y, align);
        }

        public static void helperDrawFontNumbers(Graphics g, int number, Brush brush, int x, int y, FontAlign align = FontAlign.Left)
        {
            helperDrawFont(g, PogsFonts.fontNumbers, number.ToString(), brush, x, y, align);
        }

        public static void helperDrawFontDinamic(Graphics g, string text, Brush brush, int x, int y, int widthLimit, FontAlign align = FontAlign.Left)
        {
            helperDrawFont(g, PogsFonts.font4, text, brush, x, y, align, widthLimit, PogsFonts.font3);
        }

        public static void helperDrawFont(Graphics g, PogsFont mainFont, string text, Brush brush, int x, int y, FontAlign align = FontAlign.Left, int widthLimit = 0, PogsFont smallFont = null)
        {
            PogsFontRenderer.drawText(g, text, mainFont, brush, x, y, align, widthLimit, smallFont);
        }
    }
}
