using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.Extensions;

namespace TheBlackRoom.MonoGame.GuiFramework
{
    /// <summary>
    /// Provides methods used to draw common portions of Gui Elements
    /// </summary>
    public static class GuiDraw
    {
        /// <summary>
        /// Common method to draw the background of a Gui Element
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="bounds">Gui Element Bounds</param>
        /// <param name="backColour">Gui Element Background Colour</param>
        public static void DrawElementBackground(ExtendedSpriteBatch spriteBatch,
            Rectangle bounds, Color backColour)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || bounds.IsEmpty)
                return;

            if (backColour == Color.Transparent)
                return;

            spriteBatch.FillRectangle(bounds, backColour);
        }

        /// <summary>
        /// Common method to draw a border around a Gui Element
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="bounds">Gui Element Bounds</param>
        /// <param name="borderColour">Gui Element Border Colour</param>
        /// <param name="borderThickness">Gui Element Border Thickness</param>
        public static void DrawElementBorder(ExtendedSpriteBatch spriteBatch,
            Rectangle bounds, Color borderColour, int borderThickness)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || bounds.IsEmpty)
                return;

            if ((borderColour == Color.Transparent) || (borderThickness <= 0))
                return;

            spriteBatch.DrawRectangle(bounds, borderColour, borderThickness, true);
        }

        /// <summary>
        /// Common method to draw a text inside a Gui Element
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="bounds">Gui Element Bounds</param>
        /// <param name="font">Gui Element Font</param>
        /// <param name="text">Gui Element Text</param>
        /// <param name="alignment">Gui Element Alignment within bounds</param>
        /// <param name="foreColour">Gui Element Foreground Colour</param>
        public static void DrawLabel(ExtendedSpriteBatch spriteBatch,
            Rectangle bounds, SpriteFont font, string text,
            ContentAlignment alignment, Color foreColour)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || bounds.IsEmpty)
                return;

            if ((font == null) || string.IsNullOrEmpty(text) || (foreColour == Color.Transparent))
                return;

            var size = font.MeasureString(text);

            var textRect = new Rectangle(Point.Zero, size.ToPoint());

            var textBounds = textRect.AlignInside(bounds, alignment);

            spriteBatch.DrawString(font, text, textBounds.Location.ToVector2(), foreColour,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Common method to draw a picture inside a Gui Element
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="bounds">Gui Element Bounds</param>
        /// <param name="picture">Gui Element Picture</param>
        /// <param name="alignment">Gui Element Alignment within bounds</param>
        public static void DrawPicture(ExtendedSpriteBatch spriteBatch,
            Rectangle bounds, Texture2D picture, ContentAlignment alignment)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || bounds.IsEmpty)
                return;

            if (picture == null)
                return;

            var alignedRect = picture.Bounds.AlignInside(bounds, alignment);

            spriteBatch.Draw(picture, alignedRect);
        }
    }
}
