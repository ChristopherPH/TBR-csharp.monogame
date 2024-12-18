using Microsoft.Xna.Framework;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.Extensions;
using TheBlackRoom.MonoGame.GuiToolkit.Interfaces;

namespace TheBlackRoom.MonoGame.GuiToolkit.Borders
{
    /// <summary>
    /// Gui Border that draws a solid border around a Gui Element
    /// </summary>
    public class Gui3DBorder : IGuiBorder
    {
        public Gui3DBorder()
        {
        }

        public Gui3DBorder(Color borderColour)
        {
            BorderColour = borderColour;
        }

        public Gui3DBorder(int thickness)
        {
            Thickness = thickness;
        }

        public Gui3DBorder(Color borderColour, int thickness)
        {
            BorderColour = borderColour;
            Thickness = thickness;
        }

        public int Thickness
        {
            get => _Thickness;
            set
            {
                var tmpValue = MathHelper.Max(value, 1);
                if (_Thickness == tmpValue) return;
                _Thickness = tmpValue;
            }
        }
        private int _Thickness = 1;

        public Color BorderColour
        {
            get => _BorderColour;
            set
            {
                if (_BorderColour == value) return;
                _BorderColour = value;
            }
        }
        private Color _BorderColour = Color.Black;

        Padding IGuiBorder.BorderThickness => new Padding(Thickness);


        void IGuiAdornment.Draw(GameTime gameTime, ExtendedSpriteBatch spriteBatch, Rectangle bounds)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || bounds.IsEmpty)
                return;

            if ((BorderColour == Color.Transparent) || (Thickness <= 0))
                return;

            var borderRect = bounds;
            var stepPercent = 1.0f / Thickness;

            var amount1 = 1.0f;
            var amount2 = 0.0f;

            for (int i = 0; i < Thickness; i++, amount1 -= stepPercent, amount2 += stepPercent)
            {
                var cTop = Color.Lerp(BorderColour, BorderColour.Darken(1.0f), amount1);
                var cLeft = Color.Lerp(BorderColour, BorderColour.Darken(1.0f), amount2);
                var cBottom = Color.Lerp(BorderColour, BorderColour.Lighten(1.0f), amount2);
                var cRight = Color.Lerp(BorderColour, BorderColour.Lighten(1.0f), amount2);

                cTop = cLeft = Color.Lerp(Color.White, Color.LightGray, amount2);
                cBottom = cRight = Color.Lerp(Color.Gray, Color.DimGray, amount1);

                spriteBatch.FillRectangle(new Rectangle(borderRect.Left, borderRect.Top, borderRect.Width, 1), cTop); //top
                spriteBatch.FillRectangle(new Rectangle(borderRect.Left, borderRect.Bottom - 1, borderRect.Width, 1), cBottom); //bottom
                spriteBatch.FillRectangle(new Rectangle(borderRect.Left, borderRect.Top + 1, 1, borderRect.Height - 2), cLeft); //left
                spriteBatch.FillRectangle(new Rectangle(borderRect.Right - 1, borderRect.Top + 1, 1, borderRect.Height - 2), cRight); //right

                borderRect.Shrink(1);
            }
        }

        void IGuiAdornment.Update(GameTime gameTime)
        {
        }
    }
}
