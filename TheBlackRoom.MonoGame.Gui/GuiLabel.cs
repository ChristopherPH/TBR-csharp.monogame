using Microsoft.Xna.Framework;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.Gui
{
    /// <summary>
    /// Label Gui Element
    /// </summary>
    public class GuiLabel : GuiTextElement
    {
        /// <summary>
        /// Label Text
        /// </summary>
        public string Text
        {
            get => _Text;
            set
            {
                if (_Text == value) return;
                _Text = value;
                OnTextChanged();
            }
        }
        private string _Text = string.Empty;

        /// <summary>
        /// Label alignment within bounds
        /// </summary>
        public ContentAlignment Alignment
        {
            get => _Alignment;
            set
            {
                if (_Alignment == value) return;
                _Alignment = value;
                OnAlignmentChanged();
            }
        }
        private ContentAlignment _Alignment = ContentAlignment.MiddleCenter;


        protected override void DrawGuiElement(ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
        {
            GuiDraw.DrawLabel(spriteBatch, drawBounds, Font,
                Text, Alignment, ForeColour);
        }

        /// <summary>
        /// Occurs when the Gui Element Text property has changed
        /// </summary>
        protected virtual void OnTextChanged() { }

        /// <summary>
        /// Occurs when the Gui Element Alignment property has changed
        /// </summary>
        protected virtual void OnAlignmentChanged() { }
    }
}
