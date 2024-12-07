using Microsoft.Xna.Framework;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.GuiFramework
{
    /// <summary>
    /// Base class for a Gui Element, defining the element bounds and background colour
    /// </summary>
    public abstract class GuiElement
    {
        /// <summary>
        /// Name of Gui Element
        /// </summary>
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

        /// <summary>
        /// Gui Element Bounds
        /// </summary>
        public Rectangle Bounds
        {
            get => _Bounds;
            set
            {
                var tmpValue = value;
                if ((tmpValue.Width <= 0) || (tmpValue.Height <= 0))
                    tmpValue = Rectangle.Empty;

                if (_Bounds == tmpValue) return;
                _Bounds = tmpValue;
                OnBoundsChanged();
            }
        }
        private Rectangle _Bounds = Rectangle.Empty;

        /// <summary>
        /// Background Colour of Gui Element
        /// </summary>
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

        /// <summary>
        /// Flag to indicate a border is drawn around the Gui Element
        /// </summary>
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

        /// <summary>
        /// Colour of border around Gui Element
        /// </summary>
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

        /// <summary>
        /// Thickness of border around Gui Element
        /// </summary>
        public int BorderThickness
        {
            get => _BorderThickness;
            set
            {
                var tmpValue = MathHelper.Max(value, 0);
                if (_BorderThickness  == tmpValue) return;
                _BorderThickness = tmpValue;
                OnBorderThicknessChanged();
            }
        }
        private int _BorderThickness = 1;

        /// <summary>
        /// Returns the size of the Gui Element
        /// </summary>
        public Point Size => Bounds.Size;

        /// <summary>
        /// Returns the location of the Gui Element
        /// </summary>
        public Point Location => Bounds.Location;

        /// <summary>
        /// Updates the Gui Element
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the Gui Element to the spriteBatch
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, ExtendedSpriteBatch spriteBatch)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || Bounds.IsEmpty)
                return;

            //NOTE: This only works when using SpriteSortMode.Immediate
            //      and RasterizerState() { ScissorTestEnable = true }
            //      One can used SpriteSortMode.Deferred, but then this
            //      Draw() call would need to be wrapped in its own
            //      spritebatch.Begin()/End() calls.
            var oldScissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
            spriteBatch.GraphicsDevice.ScissorRectangle = Bounds;

            //Only calculate drawBounds once
            var tmpBounds = DrawBounds;

            //Draw the background and element within the border if there is room
            if (!tmpBounds.IsEmpty)
            {
                GuiDraw.DrawElementBackground(spriteBatch, tmpBounds, BackColour);

                DrawGuiElement(gameTime, spriteBatch, tmpBounds);
            }

            if (DrawBorder)
                GuiDraw.DrawElementBorder(spriteBatch, Bounds, BorderColour, BorderThickness);

            spriteBatch.GraphicsDevice.ScissorRectangle = oldScissorRectangle;
        }

        /// <summary>
        /// Occurs when the Gui Element is to be drawn
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="drawBounds">Rectangle within element bounds to draw Gui Element</param>
        protected abstract void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds);

        /// <summary>
        /// Returns the area within the Gui Element border for drawing
        /// </summary>
        protected Rectangle DrawBounds
        {
            get
            {
                //Determine if there is a border
                var borderAmount = DrawBorder ? MathHelper.Max(0, BorderThickness) : 0;

                //Shrink the bounds by the border amount
                var drawBounds = Bounds;
                drawBounds.Shrink(borderAmount);

                if (drawBounds.Width <= 0 || drawBounds.Height <= 0)
                    return Rectangle.Empty;

                return drawBounds;
            }
        }

        /// <summary>
        /// Checks if the Gui Element bounds contains the given position
        /// </summary>
        /// <param name="value">position to check</param>
        /// <returns>true if the Gui Element contains the given position</returns>
        public bool HitTest(Vector2 value) => Bounds.Contains(value);

        /// <summary>
        /// Checks if the Gui Element bounds contains the given point
        /// </summary>
        /// <param name="value">point to check</param>
        /// <returns>true if the Gui Element contains the given point</returns>
        public bool HitTest(Point value) => Bounds.Contains(value);

        /// <summary>
        /// Checks if the Gui Element bounds contains the given co-ordinates
        /// </summary>
        /// <param name="x">x co-ordinate to check</param>
        /// <param name="y">y co-ordinate to check</param>
        /// <returns>true if the Gui Element contains the given co-ordinates</returns>
        public bool HitTest(float x, float y) => Bounds.Contains(x, y);

        /// <summary>
        /// Checks if the Gui Element bounds contains the given rectangle
        /// </summary>
        /// <param name="value">position to check</param>
        /// <returns>true if the Gui Element contains the given position</returns>
        public bool HitTest(Rectangle value) => Bounds.Contains(value);

        /// <summary>
        /// Occurs when the Gui Element Name property has changed
        /// </summary>
        protected virtual void OnNameChanged() {}

        /// <summary>
        /// Occurs when the Gui Element Bounds property has changed
        /// </summary>
        protected virtual void OnBoundsChanged() {}

        /// <summary>
        /// Occurs when the Gui Element Background Colour property has changed
        /// </summary>
        protected virtual void OnBackColourChanged() {}

        /// <summary>
        /// Occurs when the Gui Element DrawBorder property has changed
        /// </summary>
        protected virtual void OnDrawBorderChanged() {}

        /// <summary>
        /// Occurs when the Gui Element Border Colour property has changed
        /// </summary>
        protected virtual void OnBorderColourChanged() {}

        /// <summary>
        /// Occurs when the Gui Element Border Thickness property has changed
        /// </summary>
        protected virtual void OnBorderThicknessChanged() {}


        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
