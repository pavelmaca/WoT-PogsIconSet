using System;
using System.Drawing;

namespace WotPogsIconSet.Fonts
{
    public enum FontAlign { Left, Center, Right }

    public abstract class PogsFontRenderer
    {

        public static void drawText(Graphics g, string text, PogsFont bigFont, Brush color, int xPos, int yPos, FontAlign align, int maxWidth = 0, PogsFont smallFont = null)
        {
            char[] chars = convertToChars(text);

            int currentX = xPos;
            int width = getTextWidth(bigFont, text);

            bool combine = false;

            // use smaller font if width is to big
            if (maxWidth > 0 && width > maxWidth && smallFont != null)
            {
                combine = true;
                width = getTextWidth(smallFont, bigFont, text);
            }


            if (align == FontAlign.Right)
            {
                currentX = xPos - width;
            }
            else if (align == FontAlign.Center)
            {
                currentX = xPos - width / 2;
            }

            for (int i = 0; i < chars.Length; i++)
            {
                PogsFont font = (combine && smallFont.ContainsKey(chars[i])) ? smallFont : bigFont;

                drawChar(font, g, currentX, yPos, chars[i], color);
                currentX += getCharPixelLenght(font, chars[i]) + 1;
            }
        }


        protected static char[] convertToChars(string text)
        {
            return text.ToLower().ToCharArray();
        }

        public static int getTextWidth(PogsFont smallFont, PogsFont bigFont, string text)
        {
            char[] chars = convertToChars(text);

            int width = 0;
            foreach (char character in chars)
            {
                int charWidth = getCharPixelLenght(smallFont, character);
                if (charWidth == 0)
                {
                    // if small font dosnt contain char, use big font
                    charWidth = getCharPixelLenght(bigFont, character);
                }

                width += charWidth + 1;
            }

            if (width > 0)
            {
                width -= 1; // remove last space
            }

            return width;
        }

        public static int getTextWidth(PogsFont font, string text)
        {
            return getTextWidth(font, font, text);
        }

        protected static int getCharPixelLenght(PogsFont font, char character)
        {
            if (font.ContainsKey(character))
            {
                return font[character][0].Length; // number of pixels for width
            }
            return 0;
        }

        protected static void drawChar(PogsFont font, Graphics g, int xPos, int yPos, char character, Brush color)
        {
            if (font.ContainsKey(character))
            {
                for (int y = 0; y < font[character].Length; y++)
                {
                    for (int x = 0; x < font[character][y].Length; x++)
                    {
                        if (font[character][y][x] == 1)
                        {
                            g.FillRectangle(color, xPos + x, yPos + y, 1, 1);

                            //Shadow
                            g.FillRectangle(Brushes.Black, xPos + x + 1, yPos + y + 1, 1, 1);
                        }

                    }
                }
            }
            else
            {
                throw new Exception("Character " + character + " is not definnied in font class");
            }

        }
    }
}
