using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheBlackRoom.MonoGame
{
    /// <summary>
    /// SpriteBatch that allows Lines and Rectangles to be drawn directly
    /// </summary>
    public class ExtendedSpriteBatch : SpriteBatch
    {
        /// <summary>
        /// The texture used when drawing rectangles, lines and other
        /// primitives. This is a 1x1 white texture created at runtime.
        /// </summary>
        protected Texture2D WhiteTexture { get; private set; }

        public ExtendedSpriteBatch(GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            this.WhiteTexture = new Texture2D(this.GraphicsDevice, 1, 1);
            this.WhiteTexture.SetData(new Color[] { Color.White });
        }

        /// <summary>
        /// Draws a pixel at the given position
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="color">The draw color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Scale"></param>
        public void DrawPixel(Vector2 Position, Color color, float Scale = 1.0f)
        {
            this.Draw(this.WhiteTexture, Position, null, color,
                0f, new Vector2(0, 0), new Vector2(Scale, Scale), SpriteEffects.None, 0);
        }


        /// <summary>
        /// Draw a line between the two supplied points.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="end">End point.</param>
        /// <param name="color">The draw color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Thickness">Thickness of line"</param>
        public void DrawLine(Vector2 start, Vector2 end, Color color, float Thickness = 1.0f)
        {
            float length = (end - start).Length();
            float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            this.Draw(this.WhiteTexture, start, null,color, rotation,
                new Vector2(0, (float)this.WhiteTexture.Height / 2),
                new Vector2(length, Thickness),
                SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to draw.</param>
        /// <param name="color">The draw color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Border">thickness of lines</param>
        /// <param name="Inset">Rectangle is inside bounding box</param>
        public void DrawRectangle(Rectangle rectangle, Color color, int Border = 1, bool Inset = false)
        {
            if (Inset)
            {
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, Border), color); //top
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Left, rectangle.Bottom - Border, rectangle.Width, Border), color); //bottom
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Left, rectangle.Top + Border, Border, rectangle.Height - (Border * 2)), color); //left
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Right - Border, rectangle.Top + Border, Border, rectangle.Height - (Border * 2)), color); //right
            }
            else
            {
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Left - Border, rectangle.Top - Border, rectangle.Width + (Border * 2), Border), color); //top
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Left - Border, rectangle.Bottom, rectangle.Width + (Border * 2), Border), color); //bottom
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Left - Border, rectangle.Top, Border, rectangle.Height), color); //left
                this.Draw(this.WhiteTexture, new Rectangle(rectangle.Right, rectangle.Top, Border, rectangle.Height), color); //right
            }
        }

        /// <summary>
        /// Fill a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to fill.</param>
        /// <param name="color">The fill color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        public void FillRectangle(Rectangle rectangle, Color color)
        {
            this.Draw(this.WhiteTexture, rectangle, color);
        }

        /// <summary>
        /// Fill a rectangle with alpha
        /// </summary>
        /// <param name="rectangle">The rectangle to fill.</param>
        /// <param name="color">The fill color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Alpha">0.0f to 1.0f</param>
        public void FillRectangle(Rectangle rectangle, Color color, float Alpha)
        {
            Alpha = Math.Min(1.0f, Alpha);
            Alpha = Math.Max(0.0f, Alpha);
            this.Draw(this.WhiteTexture, rectangle, color * Alpha);
        }

        /// <summary>
        /// Draws a polygon
        /// </summary>
        /// <param name="Verticies"></param>
        /// <param name="color">The draw color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Thickness"></param>
        public void DrawPoly(Vector2[] Verticies, Color color, float Thickness = 1.0f)
        {
            if ((Verticies == null) || (Verticies.Length <= 1))
                return;

            int i = 0;
            for (i = 1; i < Verticies.Length; i++)
                DrawLine(Verticies[i - 1], Verticies[i], color, Thickness);

            DrawLine(Verticies[i - 1], Verticies[0], color, Thickness);
        }

        /// <summary>
        /// Draws a equilateral triangle
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="Radius"></param>
        /// <param name="color">The draw color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Angle"></param>
        /// <param name="Thickness"></param>
        public void DrawEquilateralTriangle(Vector2 Location, float Radius, Color color, float Angle, float Thickness = 1.0f)
        {
            Angle = (float)(Math.PI / 180) * Angle;

            //https://qph.fs.quoracdn.net/main-qimg-92aee96dac9fb6ba3730d9f01a085022.webp
            var root3xRadiusDiv2 = (float)Math.Sqrt(3) * Radius / 2;
            var radiusx15 = (float)(Radius * 1.5);

            //start point is top center
            var top = new Vector2(Location.X + Radius, Location.Y);

            //bottom point y is 1.5 * radius below top center
            //distance between left and right bottom points are root 3 * radius
            var bl = new Vector2(top.X - root3xRadiusDiv2, Location.Y + radiusx15);
            var br = new Vector2(top.X + root3xRadiusDiv2, Location.Y + radiusx15);

            //find center and rotate points before drawing
            if (Angle != 0)
            {
                var center = new Vector2(Location.X + Radius, Location.Y + Radius);
                bl = RotateAboutOrigin(bl, center, Angle);
                br = RotateAboutOrigin(br, center, Angle);
                top = RotateAboutOrigin(top, center, Angle);
            }

            this.DrawLine(bl, br, color, Thickness);
            this.DrawLine(br, top, color, Thickness);
            this.DrawLine(top, bl, color, Thickness);
        }

        /// <summary>
        /// Draws a triangle
        /// </summary>
        /// <param name="Rectangle"></param>
        /// <param name="color">The draw color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Angle"></param>
        /// <param name="Thickness"></param>
        public void DrawTriangle(Rectangle Rectangle, Color color, float Angle, float Thickness = 1.0f)
        {
            Angle = (float)(Math.PI / 180) * Angle;

            var len = Math.Min(Rectangle.Width, Rectangle.Height);

            var bl = new Vector2(Rectangle.Left, Rectangle.Bottom);
            var br = new Vector2(Rectangle.Right, Rectangle.Bottom);
            var top = new Vector2(Rectangle.Left + len / 2, Rectangle.Top);

            /*
            this.DrawLine(bl, br, color, Thickness);
            this.DrawLine(br, top, color, Thickness);
            this.DrawLine(top, bl, color, Thickness);
            */

            bl = RotateAboutOrigin(bl, Rectangle.Center.ToVector2(), Angle);
            br = RotateAboutOrigin(br, Rectangle.Center.ToVector2(), Angle);
            top = RotateAboutOrigin(top, Rectangle.Center.ToVector2(), Angle);

            this.DrawLine(bl, br, color, Thickness);
            this.DrawLine(br, top, color, Thickness);
            this.DrawLine(top, bl, color, Thickness);
        }

        private Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        }

        private Texture2D CreateCircle(int radius, int thickness = 10, int steps = 3)
        {
            double tmpStep = 1 / (double)steps;

            thickness = Math.Max(thickness, 1);
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = new Color(0, 0, 0, 0);

            for (int i = 0; i < thickness * steps; i++)
            {
                double tmpRad = (double)radius - (i * tmpStep);

                // Work out the minimum step necessary using trigonometry + sine approximation.
                double angleStep = 1f / tmpRad;

                for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
                {
                    // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                    int x = (int)(Math.Round(tmpRad + tmpRad * Math.Cos(angle)) + (tmpStep * i));
                    int y = (int)(Math.Round(tmpRad + tmpRad * Math.Sin(angle)) + (tmpStep * i));

                    data[y * outerRadius + x + 1] = Color.White;
                }
            }

            texture.SetData(data);
            return texture;
        }

        /// <summary>
        /// Dras a circle
        /// </summary>
        /// <param name="Radius"></param>
        /// <param name="Location"></param>
        /// <param name="color">The draw color.
        /// If using alpha channel, use BlendState.NonPremultiplied
        /// in spriteBatch.Begin() or use Color.FromNonPremultiplied()</param>
        /// <param name="Thickness"></param>
        public void DrawCircle(int Radius, Vector2 Location, Color color, int Thickness = 1)
        {
            Texture2D circle = CreateCircle(Radius, Thickness);

            // Change Color.Red to the colour you want
            this.Draw(circle, Location, color);
        }
    }
}
