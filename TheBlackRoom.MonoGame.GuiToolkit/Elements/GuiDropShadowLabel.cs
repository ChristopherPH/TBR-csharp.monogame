using Microsoft.Xna.Framework;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Drop Shadow Label Gui Element
    /// </summary>
    public class GuiDropShadowLabel : GuiLabel
    {
        /// <summary>
        /// Drop Shadow Colour
        /// </summary>
        public Color ShadowColour
        {
            get => _ShadowColour;
            set
            {
                if (_ShadowColour == value) return;
                _ShadowColour = value;
                OnShadowColourChanged();
            }
        }
        private Color _ShadowColour = Color.Black;

        /// <summary>
        /// Drop Shadow Offset
        /// </summary>
        public Vector2 ShadowOffset
        {
            get => _ShadowOffset;
            set
            {
                if (_ShadowOffset == value) return;
                _ShadowOffset = value;
                OnShadowOffsetChanged();
            }
        }
        private Vector2 _ShadowOffset = new Vector2(2, 2);


        protected override void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
        {
            //Draw drop shadow
            drawBounds.Offset(ShadowOffset);

            GuiDraw.DrawLabel(spriteBatch, drawBounds, Font,
                Text, Alignment, ShadowColour);

            //Draw label
            drawBounds.Offset(-ShadowOffset);

            GuiDraw.DrawLabel(spriteBatch, drawBounds, Font,
                Text, Alignment, ForeColour);
        }

        /// <summary>
        /// Occurs when the Gui Drop Shadow Label Element Shadow Colour property has changed
        /// </summary>
        protected virtual void OnShadowColourChanged() { }

        /// <summary>
        /// Occurs when the Gui Drop Shadow Label Element Shadow Offset property has changed
        /// </summary>
        protected virtual void OnShadowOffsetChanged() { }
    }
}
