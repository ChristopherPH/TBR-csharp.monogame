using Microsoft.Xna.Framework;

namespace GameStateEngine.Drawing
{
    public static class RectangleSliceExtensions
    {
        public static void Inflate(this ref Rectangle srcRect, int leftAmount,
            int topAmount, int rightAmount, int bottomAmount)
        {
            if (leftAmount != 0)
            {
                srcRect.X -= leftAmount;
                srcRect.Width += leftAmount;
            }

            if (topAmount != 0)
            {
                srcRect.Y -= topAmount;
                srcRect.Height += topAmount;
            }

            srcRect.Width += rightAmount;
            srcRect.Height += bottomAmount;
        }

        public static void Inflate(this ref Rectangle srcRect, int amount)
        {
            srcRect.Inflate(amount, amount);
        }

        public static void Shrink(this ref Rectangle srcRect, int leftAmount,
            int topAmount, int rightAmount, int bottomAmount)
        {
            srcRect.Inflate(-leftAmount, -topAmount, -rightAmount, -bottomAmount);
        }

        public static void Shrink(this ref Rectangle srcRect, int amount)
        {
            srcRect.Inflate(-amount, -amount);
        }

        public static void Shrink(this ref Rectangle srcRect, int x, int y)
        {
            srcRect.Inflate(-x, -y);
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceLeft(this Rectangle SrcRect, int Amount, out Rectangle Remainder)
        {
            if (SrcRect.IsEmpty || (Amount < 0))
            {
                Remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (Amount >= SrcRect.Width)
            {
                Remainder = Rectangle.Empty;
                return SrcRect;
            }

            //Rectangles are structs so we can just copy them
            Remainder = SrcRect;
            Remainder.X += Amount;
            Remainder.Width -= Amount;

            var rc = SrcRect;
            rc.Width = Amount;

            return rc;
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Percent">0f - 1f</param>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceLeftPercent(this Rectangle SrcRect, float Percent, out Rectangle Remainder)
        {
            return SrcRect.SliceLeft((int)(SrcRect.Width * Percent), out Remainder);
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceRight(this Rectangle SrcRect, int Amount, out Rectangle Remainder)
        {
            if (SrcRect.IsEmpty || (Amount < 0))
            {
                Remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (Amount >= SrcRect.Width)
            {
                Remainder = Rectangle.Empty;
                return SrcRect;
            }

            //Rectangles are structs so we can just copy them
            Remainder = SrcRect;
            Remainder.Width -= Amount;

            var rc = SrcRect;
            rc.X += SrcRect.Width - Amount;
            rc.Width = Amount;

            return rc;
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Percent">0f - 1f</param>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceRightPercent(this Rectangle SrcRect, float Percent, out Rectangle Remainder)
        {
            return SrcRect.SliceRight((int)(SrcRect.Width * Percent), out Remainder);
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceTop(this Rectangle SrcRect, int Amount, out Rectangle Remainder)
        {
            if (SrcRect.IsEmpty || (Amount < 0))
            {
                Remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (Amount >= SrcRect.Height)
            {
                Remainder = Rectangle.Empty;
                return SrcRect;
            }

            //Rectangles are structs so we can just copy them
            Remainder = SrcRect;
            Remainder.Y += Amount;
            Remainder.Height -= Amount;

            var rc = SrcRect;
            rc.Height = Amount;

            return rc;
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Percent">0f - 1f</param>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceTopPercent(this Rectangle SrcRect, float Percent, out Rectangle Remainder)
        {
            return SrcRect.SliceTop((int)(SrcRect.Height * Percent), out Remainder);
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceBottom(this Rectangle SrcRect, int Amount, out Rectangle Remainder)
        {
            if (SrcRect.IsEmpty || (Amount < 0))
            {
                Remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (Amount >= SrcRect.Height)
            {
                Remainder = Rectangle.Empty;
                return SrcRect;
            }

            //Rectangles are structs so we can just copy them
            Remainder = SrcRect;
            Remainder.Height -= Amount;

            var rc = SrcRect;
            rc.Y += SrcRect.Height - Amount;
            rc.Height = Amount;

            return rc;
        }

        /// <summary>
        /// Removes a slice of a rectangle
        /// </summary>
        /// <param name="Percent">0f - 1f</param>
        /// <param name="Remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceBottomPercent(this Rectangle SrcRect, float Percent, out Rectangle Remainder)
        {
            return SrcRect.SliceBottom((int)(SrcRect.Height * Percent), out Remainder);
        }
    }
}
