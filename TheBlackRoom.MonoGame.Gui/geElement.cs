using Microsoft.Xna.Framework;

namespace TheBlackRoom.MonoGame.Gui
{
    public abstract class geElement
    {
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name == value) return;
                _Name = value;
                OnNameChanged();
            }
        }
        private string _Name = string.Empty;

        public Rectangle ElementBounds
        {
            get => _ElementBounds;
            set
            {
                if (_ElementBounds  == value) return;
                _ElementBounds = value;
                OnElementBoundsChanged();
            }
        }
        private Rectangle _ElementBounds = Rectangle.Empty;

        public Color BackColour
        {
            get => _BackColour;
            set
            {
                if (_BackColour  == value) return;
                _BackColour = value;
                OnBackColourChanged();
            }
        }
        private Color _BackColour = Color.Transparent;

        public bool DrawBorder
        {
            get => _DrawBorder;
            set
            {
                if (_DrawBorder  == value) return;
                _DrawBorder = value;
                OnDrawBorderChanged();
            }
        }
        private bool _DrawBorder = false;

        public Color BorderColour
        {
            get => _BorderColour;
            set
            {
                if (_BorderColour  == value) return;
                _BorderColour = value;
                OnBorderColourChanged();
            }
        }
        private Color _BorderColour = Color.Black;

        public int BorderThickness
        {
            get => _BorderThickness;
            set
            {
                if (_BorderThickness  == value) return;
                _BorderThickness = value;
                OnBorderThicknessChanged();
            }
        }
        private int _BorderThickness = 1;

        public static void DrawElementBackground(ExtendedSpriteBatch spriteBatch,
            Rectangle geBounds, Color backColour)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed ||
                geBounds.IsEmpty || (backColour == Color.Transparent))
            {
                return;
            }

            spriteBatch.FillRectangle(geBounds, backColour);
        }

        public static void DrawElementBorder(ExtendedSpriteBatch spriteBatch,
            Rectangle geBounds, Color borderColour, int borderThickness)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed ||
                geBounds.IsEmpty || (borderColour == Color.Transparent) ||
                (borderThickness <= 0))
            {
                return;
            }

            spriteBatch.DrawRectangle(geBounds, borderColour, borderThickness, true);
        }

        public void Draw(ExtendedSpriteBatch spriteBatch)
        {
            //NOTE: This only works when using SpriteSortMode.Immediate
            //      and RasterizerState() { ScissorTestEnable = true }
            //      One can used SpriteSortMode.Deferred, but then this
            //      Draw() call would need to be wrapped in its own
            //      spritebatch.Begin()/End() calls.
            var oldScissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
            spriteBatch.GraphicsDevice.ScissorRectangle = ElementBounds;

            DrawElementBackground(spriteBatch, ElementBounds, BackColour);

            DrawElement(spriteBatch);

            if (DrawBorder)
                DrawElementBorder(spriteBatch, ElementBounds, BorderColour, BorderThickness);

            spriteBatch.GraphicsDevice.ScissorRectangle = oldScissorRectangle;
        }

        protected abstract void DrawElement(ExtendedSpriteBatch spriteBatch);

        protected virtual void OnNameChanged() {}
        protected virtual void OnElementBoundsChanged() {}
        protected virtual void OnBackColourChanged() {}
        protected virtual void OnDrawBorderChanged() {}
        protected virtual void OnBorderColourChanged() {}
        protected virtual void OnBorderThicknessChanged() { }


        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
