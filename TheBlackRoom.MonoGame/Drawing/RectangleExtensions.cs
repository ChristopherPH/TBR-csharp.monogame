using Microsoft.Xna.Framework;
using System;

namespace TheBlackRoom.MonoGame.Drawing
{
    public static class RectangleExtensions
    {
        /// <summary>
        /// Scales a rectangle to fit within another rectangle, while maintaining aspect ratio
        /// </summary>
        /// <param name="SrcRect">Rectangle to scale</param>
        /// <param name="Bounds">Bounds to scale into</param>
        /// <param name="scaleFactor">Saved scale factor</param>
        /// <returns>Scaled rectangle</returns>
        public static Rectangle ScaleAspect(this Rectangle SrcRect, Rectangle Bounds, out float scaleFactor)
        {
            var scaleHeight = Bounds.Height / SrcRect.Height;
            var scaleWidth = Bounds.Width / SrcRect.Width;
            scaleFactor = Math.Min(scaleHeight, scaleWidth);

            var height = SrcRect.Height * scaleFactor;
            var width = SrcRect.Width * scaleFactor;

            var scaledRect = new Rectangle(
                    (int)((Bounds.Width / 2) - (width / 2)),
                    (int)((Bounds.Height / 2) - (height / 2)),
                    (int)width,
                    (int)height
                );

            return scaledRect;
        }

        /// <summary>
        /// Scales a rectangle to fit within another rectangle, while maintaining aspect ratio
        /// </summary>
        /// <param name="SrcRect">Rectangle to scale</param>
        /// <param name="Bounds">Bounds to scale into</param>
        /// <returns>Scaled rectangle</returns>
        public static Rectangle ScaleAspect(this Rectangle SrcRect, Rectangle Bounds)
        {
            return ScaleAspect(SrcRect, Bounds, out var scaleFactor);
        }

        /// <summary>
        /// Aligns a rectangle inside another rectangle
        /// </summary>
        /// <param name="SrcRect">Rectangle to align</param>
        /// <param name="Bounds">Bounds to align into</param>
        /// <param name="Alignmment">Location to align to</param>
        /// <returns>Aligned rectangle</returns>
        public static Rectangle AlignInside(this Rectangle SrcRect, Rectangle Bounds, ContentAlignment Alignmment)
        {
            switch (Alignmment)
            {
                default:
                case ContentAlignment.TopLeft:
                    return new Rectangle(
                        Bounds.X,
                        Bounds.Y,
                        SrcRect.Width, SrcRect.Height);

                case ContentAlignment.TopCenter:
                    return new Rectangle(
                        Bounds.X + (Bounds.Width / 2) - (SrcRect.Width / 2),
                        Bounds.Y,
                        SrcRect.Width, SrcRect.Height);

                case ContentAlignment.TopRight:
                    return new Rectangle(
                        Bounds.Right - SrcRect.Width,
                        Bounds.Y,
                        SrcRect.Width, SrcRect.Height);


                case ContentAlignment.MiddleLeft:
                    return new Rectangle(
                        Bounds.X,
                        Bounds.Y + (Bounds.Height / 2) - (SrcRect.Height / 2),
                        SrcRect.Width, SrcRect.Height);

                case ContentAlignment.MiddleCenter:
                    return new Rectangle(
                        Bounds.X + (Bounds.Width / 2) - (SrcRect.Width / 2),
                        Bounds.Y + (Bounds.Height / 2) - (SrcRect.Height / 2),
                        SrcRect.Width, SrcRect.Height);

                case ContentAlignment.MiddleRight:
                    return new Rectangle(
                        Bounds.Right - SrcRect.Width,
                        Bounds.Y + (Bounds.Height / 2) - (SrcRect.Height / 2),
                        SrcRect.Width, SrcRect.Height); ;


                case ContentAlignment.BottomLeft:
                    return new Rectangle(
                        Bounds.X,
                        Bounds.Bottom - SrcRect.Height,
                        SrcRect.Width, SrcRect.Height);

                case ContentAlignment.BottomCenter:
                    return new Rectangle(
                        Bounds.X + (Bounds.Width / 2) - (SrcRect.Width / 2),
                        Bounds.Bottom - SrcRect.Height,
                        SrcRect.Width, SrcRect.Height);

                case ContentAlignment.BottomRight:
                    return new Rectangle(
                        Bounds.Right - SrcRect.Width,
                        Bounds.Bottom - SrcRect.Height,
                        SrcRect.Width, SrcRect.Height);
            }
        }
    }
}
