using Microsoft.Xna.Framework;
using System;

namespace TheBlackRoom.MonoGame.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Translate a non-premultipled alpha Microsoft.Xna.Framework.Color to a
        /// Microsoft.Xna.Framework.Color that contains premultiplied alpha.
        /// </summary>
        /// <param name="color">Non premultiplied color to translate</param>
        /// <param name="alpha">Amount of alpha (0f/Trasparent - 1.0f/Opaque)</param>
        public static Color FromNonPremultiplied(this Color color, float alpha)
        {
            return Color.FromNonPremultiplied(color.R, color.G, color.B,
                (int)Math.Round(255 * alpha));
        }

        /// <summary>
        /// Darkens a color by the given percentage
        /// </summary>
        /// <param name="color">Color to darken</param>
        /// <param name="percent">Amount to darken (0f/None - 1.0f/Black)</param>
        public static Color Darken(this Color color, float percent)
        {
            if (percent <= 0)
                return color;

            if (percent >= 1)
            {
                color.R = color.B = color.G = 0; //Black
                return color;
            }

            color.R = (byte)Math.Round((float)color.R * (1.0 - percent));
            color.G = (byte)Math.Round((float)color.G * (1.0 - percent));
            color.B = (byte)Math.Round((float)color.B * (1.0 - percent));

            return color;
        }

        /// <summary>
        /// Lightens a color by the given percentage
        /// </summary>
        /// <param name="color">Color to lighten</param>
        /// <param name="percent">Amount to lighten (0f/None - 1.0f/White)</param>
        public static Color Lighten(this Color color, float percent)
        {
            if (percent <= 0)
                return color;

            if (percent >= 1)
            {
                color.R = color.B = color.G = 255; //White
                return color;
            }

            color.R += (byte)Math.Round((float)(255 - color.R) * (percent));
            color.G += (byte)Math.Round((float)(255 - color.G) * (percent));
            color.B += (byte)Math.Round((float)(255 - color.B) * (percent));

            return color;
        }
    }
}
