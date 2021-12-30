using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Sem3
{
    class Outlines
    {
        public static void FillBorders(Bitmap bmp)
        {
            for (int x = 0; x < bmp.Width; ++x)
            {
                bmp.SetPixel(x, 0, Color.White);
                bmp.SetPixel(x, bmp.Height - 1, Color.White);
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                bmp.SetPixel(0, y, Color.White);
                bmp.SetPixel(bmp.Width - 1, y, Color.White);
            }
        }

        public static Bitmap CreateOutlines(Bitmap r)
        {
            int w = r.Width, h = r.Height;
            Bitmap o = new Bitmap(w, h);
            for(int x = 1; x < w - 1; ++x)
                for (int y = 1; y < h - 1; ++y)
                    if (IsOutline(r, x, y, 2))
                        o.SetPixel(x, y, Color.Black);
                    else
                        o.SetPixel(x, y, Color.White);
            FillBorders(o);
            return o;
        }

        private static bool IsDark(Color c)
        {
            return c.GetBrightness() < 0.6F;
        }

        private static bool IsEdge(Bitmap bmp, int x, int y)
        {
            return (IsDark(bmp.GetPixel(x + 1, y + 0)) && IsDark(bmp.GetPixel(x + 0, y + 1)))
                    || (IsDark(bmp.GetPixel(x + 1, y + 0)) && IsDark(bmp.GetPixel(x + 0, y - 1)))
                    || (IsDark(bmp.GetPixel(x - 1, y + 0)) && IsDark(bmp.GetPixel(x + 0, y + 1)))
                    || (IsDark(bmp.GetPixel(x - 1, y + 0)) && IsDark(bmp.GetPixel(x + 0, y - 1)));
        }

        private static bool IsConnection(Bitmap bmp, int x, int y, int depth)
        {
            return (x > 1 && x < bmp.Width - 2 && IsOutline(bmp, x + 1, y, depth - 1) && IsOutline(bmp, x - 1, y, depth - 1))
                    || (y > 1 && y < bmp.Height - 2 && IsOutline(bmp, x, y + 1, depth - 1) && IsOutline(bmp, x, y - 1, depth - 1));
        }

        public static bool IsOutline(Bitmap bmp, int x, int y, int depth)
        {
            if (!IsDark(bmp.GetPixel(x, y))) return false;
            
            byte sp = 0;
            for (int x0 = -1; x0 <= 1; ++x0)
                for (int y0 = -1; y0 <= 1; ++y0)
                    if ((x0 != 0 || y0 != 0) && IsDark(bmp.GetPixel(x + x0, y + y0)))
                        ++sp;

            if (sp == 7 && IsEdge(bmp, x, y))
                return true;

            if (sp == 6 && depth > 0 && IsConnection(bmp, x, y, depth))
                return true;

            return sp >= 3 && sp <= 5;
        }
    }
}
