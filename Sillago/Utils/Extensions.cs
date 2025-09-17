namespace Sillago.Utils
{
    using System;
    using System.Reflection;

    public static class Extensions
    {

        public static (int R, int G, int B) UnpackColor(int color)
        {
            int r = (color >> 16) & 0xFF;
            int g = (color >> 8)  & 0xFF;
            int b = color         & 0xFF;
            return (r, g, b);
        }

        public static int PackColor(int r, int g, int b)
            => (Math.Clamp(r, 0, 255)   << 16)
               | (Math.Clamp(g, 0, 255) << 8)
               | Math.Clamp(b, 0, 255);
    }
}