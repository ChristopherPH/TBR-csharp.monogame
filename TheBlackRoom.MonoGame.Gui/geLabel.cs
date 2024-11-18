using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.Gui
{
    public class geLabel : geTextElement
    {
        public string Text { get; set; } = string.Empty;
        public ContentAlignment Alignment { get; set; } = ContentAlignment.MiddleCenter;

        public static void DrawLabel(ExtendedSpriteBatch spriteBatch,
            Rectangle geBounds, SpriteFont font, string text,
            ContentAlignment alignment, Color foreColour)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || geBounds.IsEmpty)
                return;

            if ((font == null) || string.IsNullOrEmpty(text) || (foreColour == Color.Transparent))
                return;

            var size = font.MeasureString(text);
            var textRect = new Rectangle(Point.Zero, size.ToPoint());

            var textBounds = textRect.AlignInside(geBounds, alignment);
            spriteBatch.DrawRectangle(textBounds, Color.Purple);

            spriteBatch.DrawString(font, text, textBounds.Location.ToVector2(), foreColour,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);

            return;

            spriteBatch.DrawString(font, text, Vector2.Zero, foreColour,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            spriteBatch.DrawRectangle(new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.Teal);
        }

        protected override void DrawElement(ExtendedSpriteBatch spriteBatch)
        {
            DrawLabel(spriteBatch, ElementBounds, Font, Text, Alignment, ForeColour);
        }
    }
}
