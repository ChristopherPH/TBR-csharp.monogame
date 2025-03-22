using Microsoft.Xna.Framework;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Panel Gui Element
    /// </summary>
    public class GuiPanel : GuiElementCollection
    {
        /// <summary>
        /// Adds the specified Gui Element to the panel
        /// </summary>
        /// <param name="element">Gui element to add</param>
        public void Add(GuiElement element)
        {
            AddCollectionElement(element);
        }

        /// <summary>
        /// Removes the specified Gui Element from the panel
        /// </summary>
        /// <param name="element">Gui element to remove</param>
        public void Remove(GuiElement element)
        {
            RemoveCollectionElement(element);
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
    }
}
