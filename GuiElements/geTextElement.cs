using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrossfireRPG.GuiElements
{
    public abstract class geTextElement : geElement
    {
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

        protected virtual void OnFontChanged() { }
        protected virtual void OnForeColourChanged() { }
    }
}
