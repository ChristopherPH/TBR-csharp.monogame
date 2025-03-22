using Microsoft.Xna.Framework;
using System;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    [Flags]
    public enum GuiElementAnchorStyles
    {
        None = 0,

        Left = 0x01,
        Top = 0x02,
        Right = 0x04,
        Bottom = 0x08,

        TopLeft = Top | Left,
        TopRight = Top | Right,
        BottomLeft = Bottom | Left,
        BottomRight = Bottom | Right,

        LeftTopBottom = Left | Top | Bottom,
        RightTopBottom = Right | Top | Bottom,
        TopLeftRight = Top | Left | Right,
        BottomLeftRight = Bottom | Left | Right,

        All = Left | Top | Right | Bottom
    }

    public static class GuiElementAnchorStyleExtensions
    {
        public static Rectangle ApplyAnchor(this Rectangle elementRect,
            GuiElementAnchorStyles anchorStyle, Rectangle anchorRect)
        {
            if (anchorStyle == GuiElementAnchorStyles.None)
                return elementRect;

            if (anchorStyle.HasFlag(GuiElementAnchorStyles.Left))
            {
                elementRect.X = anchorRect.X;
            }

            if (anchorStyle.HasFlag(GuiElementAnchorStyles.Right))
            {
                elementRect.X = anchorRect.Right - elementRect.Width;
            }

            return elementRect;
        }
    }
}
