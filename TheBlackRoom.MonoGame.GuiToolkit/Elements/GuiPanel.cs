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
    }
}
