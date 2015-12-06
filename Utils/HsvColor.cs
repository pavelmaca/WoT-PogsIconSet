using System;
using System.Drawing;

namespace WotPogsIconSet.Utils
{
    public class HsvColor
    {
        struct RgbColor
        {
            public int r;
            public int g;
            public int b;
        }

        protected int h;
        protected int s;
        protected int v;

        public HsvColor(int h, int s, int v)
        {
            this.h = h;
            this.s = s;
            this.v = v;
        }


        public Color ToRgb()
        {

            RgbColor rgb;
            int region, remainder, p, q, t;

            if (this.s == 0)
            {
                rgb.r = this.v;
                rgb.g = this.v;
                rgb.b = this.v;
                return Color.FromArgb(rgb.r, rgb.g, rgb.b);
            }

            region = this.h / 43;
            remainder = (this.h - (region * 43)) * 6;

            p = (this.v * (255 - this.s)) >> 8;
            q = (this.v * (255 - ((this.s * remainder) >> 8))) >> 8;
            t = (this.v * (255 - ((this.s * (255 - remainder)) >> 8))) >> 8;

            switch (region)
            {
                case 0:
                    rgb.r = this.v; rgb.g = t; rgb.b = p;
                    break;
                case 1:
                    rgb.r = q; rgb.g = this.v; rgb.b = p;
                    break;
                case 2:
                    rgb.r = p; rgb.g = this.v; rgb.b = t;
                    break;
                case 3:
                    rgb.r = p; rgb.g = q; rgb.b = this.v;
                    break;
                case 4:
                    rgb.r = t; rgb.g = p; rgb.b = this.v;
                    break;
                default:
                    rgb.r = this.v; rgb.g = p; rgb.b = q;
                    break;
            }

            return Color.FromArgb(rgb.r, rgb.g, rgb.b);
        }
            

    }
}