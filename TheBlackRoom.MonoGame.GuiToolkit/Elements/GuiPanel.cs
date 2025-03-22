using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Panel Gui Element
    /// </summary>
    public class GuiPanel : GuiElementCollection
    {
        private Dictionary<GuiElement, GuiElementCollectionMetaData> _ElementMetaData
            = new Dictionary<GuiElement, GuiElementCollectionMetaData>();

        /// <summary>
        /// Adds the specified Gui Element to the element collection
        /// </summary>
        /// <param name="element">Gui element to add</param>
        /// <param name="anchorStyle">Anchor style for gui element</param>
        public void Add(GuiElement element,
            GuiElementAnchorStyles anchorStyle = GuiElementAnchorStyles.None)
        {
            if (AddCollectionElement(element))
            {
                _ElementMetaData[element] = new GuiElementCollectionMetaData
                {
                    AnchorStyle = anchorStyle,
                };
            }
        }

        /// <summary>
        /// Removes the specified Gui Element from the element collection
        /// </summary>
        /// <param name="element">Gui element to remove</param>
        public void Remove(GuiElement element)
        {
            if (RemoveCollectionElement(element))
            {
                _ElementMetaData.Remove(element);
            }
        }

        protected override void OnBoundsChanged(Rectangle oldBounds)
        {
            base.OnBoundsChanged(oldBounds);

            //Calculate deltas
            var dX = Bounds.X - oldBounds.X;
            var dY = Bounds.Y - oldBounds.Y;
            var dWidth = Bounds.Width - oldBounds.Width;
            var dHeight = Bounds.Height - oldBounds.Height;

            //Update element positions and sizes based on anchors
            foreach (var element in ElementCollection)
            {
                if (_ElementMetaData.TryGetValue(element, out var metaData))
                {
                    //Get old bounds as a working bounds, to adjust
                    var bounds = element.Bounds;

                    //If anchored to left and right, then need to adjust position and width,
                    //otherwise just adjust position
                    if (metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Left) &&
                        metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Right))
                    {
                        bounds.X += dX;
                        bounds.Width += dWidth - dX;
                    }
                    else if (metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Left))
                    {
                        bounds.X += dX;
                    }
                    else if (metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Right))
                    {
                        bounds.X += dWidth;
                    }

                    //If anchored to top and bottom, then need to adjust position and height,
                    //otherwise just adjust position
                    if (metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Top) &&
                        metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Bottom))
                    {
                        bounds.Y += dY;
                        bounds.Height += dHeight - dY;
                    }
                    else if (metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Top))
                    {
                        bounds.Y += dY;
                    }
                    else if (metaData.AnchorStyle.HasFlag(GuiElementAnchorStyles.Bottom))
                    {
                        bounds.Y += dHeight;
                    }

                    //Set updated bounds
                    element.Bounds = bounds;
                }
            }
        }

        /// <summary>
        /// Returns a rectangle positioned inside the panel,
        /// with the new rectangles left edge aligned with the
        /// left edge of the element bounds, and the height of
        /// the new rectangle equal to the height of the element.
        /// </summary>
        /// <param name="Width">Width of rectangle to create</param>
        /// <returns>Rectangle aligned to left of element</returns>
        public Rectangle DockLeft(int Width) => new Rectangle(
                0, 0, MathHelper.Min(MathHelper.Max(0, Width), ContentWidth), ContentHeight);

        /// <summary>
        /// Returns a rectangle positioned inside the panel,
        /// with the new rectangles top edge aligned with the
        /// top edge of the element bounds, and the width of
        /// the new rectangle equal to the width of the element.
        /// </summary>
        /// <param name="Height">Height of rectangle to create</param>
        /// <returns>Rectangle aligned to top of element</returns>
        public Rectangle DockTop(int Height) => new Rectangle(
                0, 0, ContentWidth, MathHelper.Min(MathHelper.Max(0, Height), ContentHeight));

        /// <summary>
        /// Returns a rectangle positioned inside the panel,
        /// with the new rectangles right edge aligned with the
        /// right edge of the element bounds, and the height of
        /// the new rectangle equal to the height of the element.
        /// </summary>
        /// <param name="Width">Width of rectangle to create</param>
        /// <returns>Rectangle aligned to left of element</returns>
        public Rectangle DockRight(int Width) => new Rectangle(
                ContentWidth - MathHelper.Min(MathHelper.Max(0, Width), ContentWidth), 0,
                MathHelper.Min(MathHelper.Max(0, Width), ContentWidth), ContentHeight);

        /// <summary>
        /// Returns a rectangle positioned inside the panel,
        /// with the new rectangles bottom edge aligned with the
        /// bottom edge of the element bounds, and the width of
        /// the new rectangle equal to the width of the element.
        /// </summary>
        /// <param name="Height">Height of rectangle to create</param>
        /// <returns>Rectangle aligned to bottom of element</returns>
        public Rectangle DockBottom(int Height) => new Rectangle(
                0, ContentHeight - MathHelper.Min(MathHelper.Max(0, Height), ContentHeight),
                ContentWidth, MathHelper.Min(MathHelper.Max(0, Height), ContentHeight));

        private class GuiElementCollectionMetaData
        {
            public GuiElementAnchorStyles AnchorStyle { get; set; }
        }
    }
}
