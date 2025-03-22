using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Label Gui Element
    /// </summary>
    public class GuiLabel : GuiTextElement
    {
        public GuiLabel() { }

        public GuiLabel(SpriteFont font, string text)
        {
            this.Font = font;
            this.Text = text;
        }

        public GuiLabel(SpriteFont font, Color foreColour, string text)
        {
            this.Font = font;
            this.ForeColour = foreColour;
            this.Text = text;
        }

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

        protected override void UpdateGuiElement(GameTime gameTime) { }

        protected override void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
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
