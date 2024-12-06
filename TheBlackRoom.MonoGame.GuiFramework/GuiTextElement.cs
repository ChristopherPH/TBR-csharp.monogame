using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheBlackRoom.MonoGame.GuiFramework
{
    /// <summary>
    /// Base class for a Gui Element containing text, defining the font and fore colour
    /// </summary>
    public abstract class GuiTextElement : GuiElement
    {
        /// <summary>
        /// Font of Gui Text Element
        /// </summary>
        public SpriteFont Font
        {
            get => _Font;
            set
            {
                if (_Font == value) return;
                _Font = value;
                OnFontChanged();
            }
        }
        private SpriteFont _Font = null;

        /// <summary>
        /// Foreground Colour of Gui Text Element
        /// </summary>
        public Color ForeColour
        {
            get => _ForeColour;
            set
            {
                if (_ForeColour == value) return;
                _ForeColour = value;
                OnForeColourChanged();
            }
        }
        private Color _ForeColour = Color.Black;

        /// <summary>
        /// Occurs when the Gui Text Element Font property has changed
        /// </summary>
        protected virtual void OnFontChanged() { }

        /// <summary>
        /// Occurs when the Gui Text Element Foreground Colour property has changed
        /// </summary>
        protected virtual void OnForeColourChanged() { }
    }
}
