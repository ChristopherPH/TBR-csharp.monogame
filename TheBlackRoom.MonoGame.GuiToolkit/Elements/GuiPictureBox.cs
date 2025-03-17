using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// PictureBox Gui Element
    /// </summary>
    public class GuiPictureBox : GuiTextElement
    {
        /// <summary>
        /// Picture Texture
        /// </summary>
        public Texture2D Picture
        {
            get => _Picture;
            set
            {
                if (_Picture == value) return;
                _Picture = value;
                OnPictureChanged();
            }
        }
        private Texture2D _Picture = null;

        /// <summary>
        /// Picture alignment within bounds
        /// </summary>
        public ScaleModes ScaleMode
        {
            get => _ScaleMode;
            set
            {
                if (_ScaleMode == value) return;
                _ScaleMode = value;
                OnScaleModeChanged();
            }
        }
        private ScaleModes _ScaleMode = ScaleModes.Crop;

        /// <summary>
        /// Picture alignment within bounds
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
            if (Picture == null) return;

            //Shrink the bounds by padding
            drawBounds.Shrink(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);

            if (drawBounds.Width <= 0 || drawBounds.Height <= 0)
                return;

            GuiDraw.DrawPicture(spriteBatch, drawBounds, Picture,
                ScaleMode, Alignment);
        }

        /// <summary>
        /// Occurs when the Gui Element Picture property has changed
        /// </summary>
        protected virtual void OnPictureChanged() { }

        /// <summary>
        /// Occurs when the Gui Element ScaleMode property has changed
        /// </summary>
        protected virtual void OnScaleModeChanged() { }

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
