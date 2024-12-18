using Microsoft.Xna.Framework;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.GuiToolkit.Interfaces;

namespace TheBlackRoom.MonoGame.GuiToolkit.Borders
{
    /// <summary>
    /// Gui Border that draws a solid border around a Gui Element
    /// </summary>
    public class GuiSolidBorder : IGuiBorder
    {
        public GuiSolidBorder()
        {
        }

        public GuiSolidBorder(Color borderColour)
        {
            BorderColour = borderColour;
        }

        public GuiSolidBorder(int thickness)
        {
            Thickness = thickness;
        }

        public GuiSolidBorder(Color borderColour, int thickness)
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
            GuiDraw.DrawBorder(spriteBatch, bounds, BorderColour, Thickness);
        }

        void IGuiAdornment.Update(GameTime gameTime)
        {
        }
    }
}
