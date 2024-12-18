using Microsoft.Xna.Framework;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.GuiFramework.Interfaces;

namespace TheBlackRoom.MonoGame.GuiToolkit
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
        /// Border adornment to draw around Gui Element
        /// </summary>
        public IGuiBorder Border { get; set; } = null;

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

            //Draw the background over the whole element
            GuiDraw.DrawBackground(spriteBatch, Bounds, BackColour);

            //Draw the element within the content area if there is room
            if (!ContentBounds.IsEmpty)
                DrawGuiElement(gameTime, spriteBatch, ContentBounds);

            //Draw the border around the content area
            if (Border != null)
                Border.Draw(gameTime, spriteBatch, Bounds);

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
        /// Returns the area within the Gui Element border, or Rectangle.Empty if no content area
        /// </summary>
        protected virtual Rectangle ContentBounds
        {
            get
            {
                //Start with full bounds
                var contentBounds = Bounds;

                //Shrink the bounds by the border amount
                if (Border != null)
                    contentBounds.Shrink(Border.BorderThickness);

                //If there is no content area, return
                if (contentBounds.Width <= 0 || contentBounds.Height <= 0)
                    return Rectangle.Empty;

                return contentBounds;
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


        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
