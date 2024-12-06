using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TheBlackRoom.MonoGame.Extensions
{
    public static class SpriteBatchExtensions
    {
        /// <summary>
        /// Draws a texture to destinationRectangle with no tint
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="destinationRectangle"></param>
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Rectangle destinationRectangle)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.White);
        }

        /// <summary>
        /// Draw texture to position with no tint
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        private static void AlignString(SpriteFont font, string text, Rectangle bounds,
            Alignment align, float scale, out Vector2 position, out Vector2 origin)
        {
            var size = font.MeasureString(text);

            position = new Vector2(bounds.Left + bounds.Width / 2,
                bounds.Top + bounds.Height / 2);

            origin = size * 0.5f;

            if (align.HasFlag(Alignment.Left))
                origin.X += bounds.Width / 2 - (size.X * scale) / 2;
            else if (align.HasFlag(Alignment.Right))
                origin.X -= bounds.Width / 2 - (size.X * scale) / 2;

            if (align.HasFlag(Alignment.Top))
                origin.Y += bounds.Height / 2 - (size.Y * scale) / 2;
            else if (align.HasFlag(Alignment.Bottom))
                origin.Y -= bounds.Height / 2 - (size.Y * scale) / 2;
        }

        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font,
            string text, Rectangle bounds, Alignment align, Color color, float scale = 1.0f)
        {
            AlignString(font, text, bounds, align, scale, out var pos, out var origin);

            spriteBatch.DrawString(font, text, pos, color, 0, origin, scale, SpriteEffects.None, 0);
        }
    }

    [Flags]
    public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }
}
