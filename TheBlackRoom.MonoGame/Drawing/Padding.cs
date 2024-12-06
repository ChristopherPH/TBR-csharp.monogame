using Microsoft.Xna.Framework;
using System;

namespace TheBlackRoom.MonoGame.Drawing
{
    public struct Padding : IEquatable<Padding>
    {
        private static readonly Padding _EmptyPadding = new Padding(0);

        /// <summary>
        /// Initializes a Padding with all values set to 0
        /// </summary>
        public Padding() : this(0) { }

        /// <summary>
        /// Initializes a Padding with all values set to all
        /// </summary>
        /// <param name="all">Value to set all values to</param>
        public Padding(int all) : this(all, all, all, all) { }

        /// <summary>
        /// Initializes a Padding with the given values
        /// </summary>
        /// <param name="left">Value for Left side of Padding</param>
        /// <param name="top">Value for Top side of Padding</param>
        /// <param name="right">Value for Right side of Padding</param>
        /// <param name="bottom">Value for Bottom side of Padding</param>
        public Padding(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Value for Left side of Padding
        /// </summary>
        public int Top { get; set; } = 0;

        /// <summary>
        /// Value for Top side of Padding
        /// </summary>
        public int Bottom { get; set; } = 0;

        /// <summary>
        /// Value for Right side of Padding
        /// </summary>
        public int Left { get; set; } = 0;

        /// <summary>
        /// Value for Bottom side of Padding
        /// </summary>
        public int Right { get; set; } = 0;

        /// <summary>
        /// Value of Horizontal Padding
        /// </summary>
        public int Horizontal => Left + Right;

        /// <summary>
        /// Value of Vertical Padding
        /// </summary>
        public int Vertical => Top + Bottom;

        /// <summary>
        /// Value of Horizontal and Vertical Padding returned as a Size
        /// </summary>
        public Vector2 Size => new Vector2(Horizontal, Vertical);

        /// <summary>
        /// Empty Padding Value
        /// </summary>
        public static Padding Empty => _EmptyPadding;

        public static bool operator ==(Padding p1, Padding p2) => p1.Equals(p2);

        public static bool operator !=(Padding p1, Padding p2) => !p1.Equals(p2);

        public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);

        public bool Equals(Padding p) => Left == p.Left && Top == p.Top && Right == p.Right && Bottom == p.Bottom;

        public override bool Equals(object obj) => obj is Padding p && this.Equals(p);

        public override string ToString() => $"L:{Left} T:{Top} R:{Right} B:{Bottom}";
    }
}
