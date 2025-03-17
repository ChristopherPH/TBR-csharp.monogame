using Microsoft.Xna.Framework;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
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

        /// <summary>
        /// Text padding within bounds
        /// </summary>
        public Padding Padding
        {
            get => _Padding;
            set
            {
                if (_Padding == value) return;
                _Padding = value;
                OnPaddingChanged();
            }
        }
        private Padding _Padding = Padding.Empty;

        protected override void UpdateGuiElement(GameTime gameTime) { }

        protected override void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
        {
            //Shrink the bounds by padding
            drawBounds.Shrink(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);

            if (drawBounds.Width <= 0 || drawBounds.Height <= 0)
                return;

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

        /// <summary>
        /// Occurs when the Gui Element Padding property has changed
        /// </summary>
        protected virtual void OnPaddingChanged() { }
    }
}
