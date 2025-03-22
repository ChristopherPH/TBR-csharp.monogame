using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.GuiToolkit.Interfaces;

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
        /// Gui Element Bounds (Bounding Box)
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
        /// Interior margin (padding) of the Gui Element, inset from the bounds
        /// to the border (if any).
        /// </summary>
        public Padding Margin
        {
            get => _Margin;
            set
            {
                if (_Margin == value) return;
                _Margin = value;
                OnMarginChanged();
            }
        }
        private Padding _Margin = Padding.Empty;

        /// <summary>
        /// Border adornment to draw around Gui Element
        /// </summary>
        public IGuiBorder Border { get; set; } = null;

        /// <summary>
        /// Size of the Gui Element
        /// </summary>
        public Point Size
        {
            get => _Bounds.Size;
            set
            {
                var tmpValue = value;
                if ((tmpValue.X <= 0) || (tmpValue.Y <= 0))
                    tmpValue = Point.Zero;

                if (_Bounds.Size == tmpValue) return;
                _Bounds.Size = tmpValue;
                OnBoundsChanged();
            }
        }

        /// <summary>
        /// Location of the Gui Element
        /// </summary>
        public Point Location
        {
            get => _Bounds.Location;
            set
            {
                if (_Bounds.Location == value) return;
                _Bounds.Location = value;
                OnBoundsChanged();
            }
        }

        /// <summary>
        /// Parent Gui Element of the Gui Element
        /// </summary>
        public GuiElement ParentElement
        {
            get => _ParentElement;
            internal set
            {
                if (_ParentElement == value) return;
                _ParentElement = value;
                OnParentElementChanged();
            }
        }
        private GuiElement _ParentElement = null;

        /// <summary>
        /// Updates the Gui Element
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            UpdateGuiElement(gameTime);
        }

        /// <summary>
        /// Occurs when the Gui Element is to be updated
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void UpdateGuiElement(GameTime gameTime);

        /// <summary>
        /// Draws the Gui Element to the spriteBatch
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, ExtendedSpriteBatch spriteBatch)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || ScreenBounds.IsEmpty)
                return;

            //NOTE: This only works when using SpriteSortMode.Immediate
            //      and RasterizerState() { ScissorTestEnable = true }
            //      One can used SpriteSortMode.Deferred, but then this
            //      Draw() call would need to be wrapped in its own
            //      spritebatch.Begin()/End() calls.
            var oldScissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
            spriteBatch.GraphicsDevice.ScissorRectangle = ScreenBounds;

            //Draw the background over the whole element
            GuiDraw.DrawBackground(spriteBatch, ScreenBounds, BackColour);

            //Draw the element within the content area if there is room
            if (!ScreenContentBounds.IsEmpty)
                DrawGuiElement(gameTime, spriteBatch, ScreenContentBounds);

            //Draw the border around the content area
            if (Border != null)
                Border.Draw(gameTime, spriteBatch, ScreenBounds);

            spriteBatch.GraphicsDevice.ScissorRectangle = oldScissorRectangle;
        }

        /// <summary>
        /// Occurs when the Gui Element is to be drawn
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="drawBounds">Rectangle within element bounds to draw Gui Element</param>
        protected abstract void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds);


        /// <summary>
        /// Returns the content area within the Gui Element border,
        /// or Rectangle.Empty if no content area
        /// </summary>
        public virtual Rectangle ContentBounds
        {
            get
            {
                //Start with full bounds
                var contentBounds = Bounds;

                //Shrink the bounds by the margin
                contentBounds.Shrink(Margin.Left, Margin.Top, Margin.Right, Margin.Bottom);

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
        /// Gets the offset to adjust the Gui Element bounds to the screen location
        /// </summary>
        protected Point ScreenOffset
        {
            get
            {
                if (ParentElement == null)
                    return Point.Zero;

                return ParentElement.ScreenOffset + ParentElement.ContentBounds.Location;
            }
        }

        /// <summary>
        /// Gui Element Bounds, adjusted to the screen location
        /// </summary>
        protected virtual Rectangle ScreenBounds
        {
            get
            {
                var bounds = Bounds;

                //Shrink the bounds by the margin
                bounds.Shrink(Margin.Left, Margin.Top, Margin.Right, Margin.Bottom);

                //If there is no content area, return
                if (bounds.Width <= 0 || bounds.Height <= 0)
                    return Rectangle.Empty;

                bounds.Offset(ScreenOffset);
                return bounds;
            }
        }

        /// <summary>
        /// Gui Element Content Bounds, adjusted to the screen location
        /// </summary>
        protected virtual Rectangle ScreenContentBounds
        {
            get
            {
                var bounds = ContentBounds;
                bounds.Offset(ScreenOffset);
                return bounds;
            }
        }

        /// <summary>
        /// Checks if the Gui Element bounds contains the given position
        /// </summary>
        /// <param name="value">position to check</param>
        /// <returns>true if the Gui Element contains the given position</returns>
        public bool HitTest(Vector2 value) => ScreenBounds.Contains(value);

        /// <summary>
        /// Checks if the Gui Element bounds contains the given point
        /// </summary>
        /// <param name="value">point to check</param>
        /// <returns>true if the Gui Element contains the given point</returns>
        public bool HitTest(Point value) => ScreenBounds.Contains(value);

        /// <summary>
        /// Checks if the Gui Element bounds contains the given co-ordinates
        /// </summary>
        /// <param name="x">x co-ordinate to check</param>
        /// <param name="y">y co-ordinate to check</param>
        /// <returns>true if the Gui Element contains the given co-ordinates</returns>
        public bool HitTest(float x, float y) => ScreenBounds.Contains(x, y);

        /// <summary>
        /// Checks if the Gui Element bounds contains the given rectangle
        /// </summary>
        /// <param name="value">position to check</param>
        /// <returns>true if the Gui Element contains the given position</returns>
        public bool HitTest(Rectangle value) => ScreenBounds.Contains(value);



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
        /// Occurs when the Gui Element Margin property has changed
        /// </summary>
        protected virtual void OnMarginChanged() { }

        /// <summary>
        /// Occurs when the Gui Element Parent property has changed
        /// </summary>
        protected virtual void OnParentElementChanged() { }


        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
