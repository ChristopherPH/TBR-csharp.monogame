using Microsoft.Xna.Framework;
using System;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.Extensions;
using TheBlackRoom.MonoGame.GuiToolkit.Interfaces;

namespace TheBlackRoom.MonoGame.GuiToolkit.Borders
{
    /// <summary>
    /// Gui Border that draws a raised border around a Gui Element
    /// </summary>
    public class GuiRaisedBorder : IGuiBorder
    {
        public GuiRaisedBorder()
        {
        }

        public GuiRaisedBorder(Color borderColour)
        {
            BorderColour = borderColour;
        }

        public GuiRaisedBorder(int thickness)
        {
            Thickness = thickness;
        }

        public GuiRaisedBorder(Color borderColour, int thickness)
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

            var darkenedColour = BorderColour.Darken(0.5f);
            var borderRect = bounds;

            var darkSteps = Thickness / 2;
            var lightSteps = Thickness - darkSteps;

            var darkStepPercent = 1.0f / darkSteps;
            var lightStepPercent = 1.0f / lightSteps;
            var darkAmount = 1.0f;

            //Draw dark to light
            for (int i = 0; i < darkSteps; i++, darkAmount -= darkStepPercent)
            {
                spriteBatch.DrawRectangle(borderRect, Color.Lerp(BorderColour, darkenedColour, darkAmount), 1, true);
                borderRect.Shrink(1);
            }

            //Draw light to dark
            var lightAmount = 0.0f;
            for (int i = 0; i < lightSteps; i++, lightAmount += lightStepPercent)
            {
                spriteBatch.DrawRectangle(borderRect, Color.Lerp(BorderColour, darkenedColour, lightAmount), 1, true);
                borderRect.Shrink(1);
            }
        }

        void IGuiAdornment.Update(GameTime gameTime)
        {
        }
    }
}
