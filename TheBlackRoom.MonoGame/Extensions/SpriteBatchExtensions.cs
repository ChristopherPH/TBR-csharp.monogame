using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheBlackRoom.MonoGame.Drawing;

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

        /// <summary>
        /// Draws text, aligned within a rectangle
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="bounds">The drawing location on screen.</param>
        /// <param name="textAlign">Alignment of text within bounds</param>
        /// <param name="color">A color mask.</param>
        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont spriteFont,
            string text, Rectangle bounds, ContentAlignment textAlign, Color color)
        {
            var textSize = spriteFont.MeasureString(text);

            //Convert textSize to a Rectangle and align
            var textRect = new Rectangle(Point.Zero, textSize.ToPoint());
            var alignedRect = textRect.AlignInside(bounds, textAlign);

            //Convert aligned location to vector for drawing
            var position = alignedRect.Location.ToVector2();

            //Do draw
            spriteBatch.DrawString(spriteFont, text, position, color);
        }

        /// <summary>
        /// Draws text, aligned within a rectangle, with scaling
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="bounds">The drawing location on screen.</param>
        /// <param name="textAlign">Alignment of text within bounds</param>
        /// <param name="color">A color mask.</param>
        /// <param name="scale">A scale factor for this string, 1.0f is no scale.</param>
        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont spriteFont,
            string text, Rectangle bounds, ContentAlignment textAlign, Color color, float scale)
        {
            if (scale <= 0f)
                throw new ArgumentException("Scale must be larger than 0", nameof(scale));

            DrawString(spriteBatch, spriteFont, text, bounds, textAlign, color, textAlign, 0f, scale);
        }

        /// <summary>
        /// Draws text, aligned within a rectangle, with rotation and scaling
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="bounds">The drawing location on screen.</param>
        /// <param name="textAlign">Alignment of text within bounds</param>
        /// <param name="color">A color mask.</param>
        /// <param name="originAlign">Alignment of the origin, used as a reference point for scaling and rotating</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="scale">A scale factor for this string, 1.0f is no scale.</param>
        /// <param name="spriteEffects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this string.</param>
        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont spriteFont,
            string text, Rectangle bounds, ContentAlignment textAlign, Color color,
            ContentAlignment originAlign, float rotation, float scale,
            SpriteEffects spriteEffects = SpriteEffects.None, float layerDepth = 0f)
        {
            var textSize = spriteFont.MeasureString(text);

            //Convert textSize to a scaled Rectangle and align
            var textRect = new Rectangle(Point.Zero, textSize.ToPoint());
            var alignedRect = textRect.AlignInside(bounds, textAlign);

            //Using the text rectangle, align an empty point
            //to find the origin position on the rectangle bounding box
            var originRect = Rectangle.Empty.AlignInside(textRect, originAlign);
            var origin = originRect.Location.ToVector2();

            //Using the aligned text rectangle, align an empty point
            //to find the location position on the rectangle bounding box
            //to compensate for adjusting the origin
            var positionRect = Rectangle.Empty.AlignInside(alignedRect, originAlign);
            var position = positionRect.Location.ToVector2();

            //Do Draw
            spriteBatch.DrawString(spriteFont, text, position, color, rotation, origin,
                scale, spriteEffects, layerDepth);
        }
    }
}
