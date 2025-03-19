namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Gui Layout, top level element that can contain other Gui Elements
    /// </summary>
    public class GuiLayout : GuiElementCollection
    {
        /// <summary>
        /// Adds the specified Gui Element to the layout
        /// </summary>
        /// <param name="element">Gui element to add</param>
        public void Add(GuiElement element)
        {
            AddCollectionElement(element);
        }

        /// <summary>
        /// Removes the specified Gui Element from the layout
        /// </summary>
        /// <param name="element">Gui element to remove</param>
        public void Remove(GuiElement element)
        {
            RemoveCollectionElement(element);
        }
    }
}
