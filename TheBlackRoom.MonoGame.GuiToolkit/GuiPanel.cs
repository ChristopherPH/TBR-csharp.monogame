using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TheBlackRoom.MonoGame.GuiToolkit
{
    /// <summary>
    /// Panel Gui Element
    /// </summary>
    public class GuiPanel : GuiElementCollection
    {
        /// <summary>
        /// Adds the specified Gui Element to the panel
        /// </summary>
        /// <param name="element"></param>
        public void Add(GuiElement element)
        {
            AddChildElement(element);
        }

        /// <summary>
        /// Removes the specified Gui Element from the panel
        /// </summary>
        /// <param name="element"></param>
        public void Remove(GuiElement element)
        {
            RemoveChildElement(element);
        }
    }
}
