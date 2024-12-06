using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.GuiFramework
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
            if (Picture == null) return;

            GuiDraw.DrawPicture(spriteBatch, drawBounds, Picture, Alignment);
        }

        /// <summary>
        /// Occurs when the Gui Element Picture property has changed
        /// </summary>
        protected virtual void OnPictureChanged() { }

        /// <summary>
        /// Occurs when the Gui Element Alignment property has changed
        /// </summary>
        protected virtual void OnAlignmentChanged() { }
    }
}
