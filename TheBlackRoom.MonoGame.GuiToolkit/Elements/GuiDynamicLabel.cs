using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    ///Dynamic  Label Gui Element
    /// </summary>
    public class GuiDynamicLabel : GuiTextElement
    {
        public GuiDynamicLabel() { }

        public GuiDynamicLabel(SpriteFont font, Func<string> getText)
        {
            this.Font = font;
            this.GetText = getText;
        }

        public GuiDynamicLabel(SpriteFont font, Color foreColour, Func<string> getText)
        {
            this.Font = font;
            this.ForeColour = foreColour;
            this.GetText = getText;
        }

        /// <summary>
        /// Function to get dynamic text
        /// </summary>
        public Func<string> GetText { get; set; }

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
            if (GetText == null)
                return;

            GuiDraw.DrawLabel(spriteBatch, drawBounds, Font,
                GetText(), Alignment, ForeColour);
        }


        /// <summary>
        /// Occurs when the Gui Element Alignment property has changed
        /// </summary>
        protected virtual void OnAlignmentChanged() { }
    }
}
