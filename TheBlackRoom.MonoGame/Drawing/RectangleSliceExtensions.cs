using Microsoft.Xna.Framework;

namespace TheBlackRoom.MonoGame.Drawing
{
    public static class RectangleSliceExtensions
    {
        /// <summary>
        /// Increases the edges of a rectangle by the given amounts
        /// </summary>
        /// <param name="srcRect">Rectangle to inflate</param>
        /// <param name="leftAmount">Amount to inflate left edge by</param>
        /// <param name="topAmount">Amount to inflate top edge by</param>
        /// <param name="rightAmount">Amount to inflate right edge by</param>
        /// <param name="bottomAmount">Amount to inflate bottom edge by</param>
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

        /// <summary>
        /// Increases all edges of a rectangle by the given amount
        /// </summary>
        /// <param name="srcRect">Rectangle to inflate</param>
        /// <param name="amount">Amount to inflate each edge by</param>
        public static void Inflate(this ref Rectangle srcRect, int amount)
        {
            srcRect.Inflate(amount, amount);
        }

        /// <summary>
        /// Increases the edges of a rectangle by the given padding
        /// </summary>
        /// <param name="srcRect">Rectangle to inflate</param>
        /// <param name="padding">Amount to inflate each edge by</param>
        public static void Inflate(this ref Rectangle srcRect, Padding padding)
        {
            srcRect.Inflate(padding.Left, padding.Top, padding.Right, padding.Bottom);
        }

        /// <summary>
        /// Shrinks the edges of a rectangle by the given amounts
        /// </summary>
        /// <param name="srcRect">Rectangle to shrink</param>
        /// <param name="leftAmount">Amount to shrink left edge by</param>
        /// <param name="topAmount">Amount to shrink top edge by</param>
        /// <param name="rightAmount">Amount to shrink right edge by</param>
        /// <param name="bottomAmount">Amount to shrink bottom edge by</param>
        public static void Shrink(this ref Rectangle srcRect, int leftAmount,
            int topAmount, int rightAmount, int bottomAmount)
        {
            srcRect.Inflate(-leftAmount, -topAmount, -rightAmount, -bottomAmount);
        }

        /// <summary>
        /// Shrinks all edges of a rectangle by the given amount
        /// </summary>
        /// <param name="srcRect">Rectangle to shrink</param>
        /// <param name="amount">Amount to shrink each edge by</param>
        public static void Shrink(this ref Rectangle srcRect, int amount)
        {
            srcRect.Inflate(-amount, -amount);
        }

        /// <summary>
        /// Shrinks the horizontal and vertical edges by the given amounts
        /// </summary>
        /// <param name="srcRect">Rectangle to shrink</param>
        /// <param name="horizontal">Amount to shrink each horizontal edge by</param>
        /// <param name="vertical">Amount to shrink each vertical edge by</param>
        public static void Shrink(this ref Rectangle srcRect, int horizontal, int vertical)
        {
            srcRect.Inflate(-horizontal, -vertical);
        }

        /// <summary>
        /// Shrinks the edges of a rectangle by the given padding
        /// </summary>
        /// <param name="srcRect">Rectangle to shrink</param>
        /// <param name="padding">Amount to shrink rectangle by</param>
        public static void Shrink(this ref Rectangle srcRect, Padding padding)
        {
            srcRect.Inflate(-padding.Left, -padding.Top, -padding.Right, -padding.Bottom);
        }

        /// <summary>
        /// Removes a left slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="amount">Amount to slice</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceLeft(this Rectangle srcRect, int amount, out Rectangle remainder)
        {
            if (srcRect.IsEmpty || (amount < 0))
            {
                remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (amount >= srcRect.Width)
            {
                remainder = Rectangle.Empty;
                return srcRect;
            }

            //Rectangles are structs so we can just copy them
            remainder = srcRect;
            remainder.X += amount;
            remainder.Width -= amount;

            var rc = srcRect;
            rc.Width = amount;

            return rc;
        }

        /// <summary>
        /// Removes a left slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="percent">0f - 1f</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceLeftPercent(this Rectangle srcRect, float percent, out Rectangle remainder)
        {
            return srcRect.SliceLeft((int)(srcRect.Width * percent), out remainder);
        }

        /// <summary>
        /// Removes a right slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="amount">Amount to slice</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceRight(this Rectangle srcRect, int amount, out Rectangle remainder)
        {
            if (srcRect.IsEmpty || (amount < 0))
            {
                remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (amount >= srcRect.Width)
            {
                remainder = Rectangle.Empty;
                return srcRect;
            }

            //Rectangles are structs so we can just copy them
            remainder = srcRect;
            remainder.Width -= amount;

            var rc = srcRect;
            rc.X += srcRect.Width - amount;
            rc.Width = amount;

            return rc;
        }

        /// <summary>
        /// Removes a right slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="percent">0f - 1f</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceRightPercent(this Rectangle srcRect, float percent, out Rectangle remainder)
        {
            return srcRect.SliceRight((int)(srcRect.Width * percent), out remainder);
        }

        /// <summary>
        /// Removes a top slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="amount">Amount to slice</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceTop(this Rectangle srcRect, int amount, out Rectangle remainder)
        {
            if (srcRect.IsEmpty || (amount < 0))
            {
                remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (amount >= srcRect.Height)
            {
                remainder = Rectangle.Empty;
                return srcRect;
            }

            //Rectangles are structs so we can just copy them
            remainder = srcRect;
            remainder.Y += amount;
            remainder.Height -= amount;

            var rc = srcRect;
            rc.Height = amount;

            return rc;
        }

        /// <summary>
        /// Removes a top slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="percent">0f - 1f</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceTopPercent(this Rectangle srcRect, float percent, out Rectangle remainder)
        {
            return srcRect.SliceTop((int)(srcRect.Height * percent), out remainder);
        }

        /// <summary>
        /// Removes a bottom slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="amount">Amount to slice</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceBottom(this Rectangle srcRect, int amount, out Rectangle remainder)
        {
            if (srcRect.IsEmpty || (amount < 0))
            {
                remainder = Rectangle.Empty;
                return Rectangle.Empty;
            }

            if (amount >= srcRect.Height)
            {
                remainder = Rectangle.Empty;
                return srcRect;
            }

            //Rectangles are structs so we can just copy them
            remainder = srcRect;
            remainder.Height -= amount;

            var rc = srcRect;
            rc.Y += srcRect.Height - amount;
            rc.Height = amount;

            return rc;
        }

        /// <summary>
        /// Removes a bottom slice of a rectangle
        /// </summary>
        /// <param name="srcRect">Rectangle to slice</param>
        /// <param name="percent">0f - 1f</param>
        /// <param name="remainder">Leftover rectangle after removing the slice</param>
        /// <returns>Sliced rectangle</returns>
        public static Rectangle SliceBottomPercent(this Rectangle srcRect, float percent, out Rectangle remainder)
        {
            return srcRect.SliceBottom((int)(srcRect.Height * percent), out remainder);
        }
    }
}
