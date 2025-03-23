using Microsoft.Xna.Framework;
using System.Linq;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Gui Layout, top level element that can contain other Gui Elements
    /// </summary>
    public class GuiLayout : GuiPanel
    {
        public GuiElement SelectedElement { get; private set; } = null;

        public void SelectElementAt(Vector2 position) => SelectElement(GetElementAt(position));
        public void SelectElementAt(Point point) => SelectElement(GetElementAt(point));
        public void SelectElementAt(float x, float y) => SelectElement(GetElementAt(x, y));
        public void SelectElementByName(string name) => SelectElement(Descendants.FirstOrDefault(x => x.Name == name));

        public void ActivateSelectedElement() => SelectedElement?.Activate();

        protected void SelectElement(GuiElement element)
        {
            //Remove selection from previous element
            if (SelectedElement != null)
            {
                //TODO: Remove IGuiSelection ...
            }

            SelectedElement = element;

            //Select new element
            if (element != null)
            {
                //TODO: Set IGuiSelection ...
            }
        }

        protected override void OnElementRemoved(int index, GuiElement element)
        {
            base.OnElementRemoved(index, element);

            if (SelectedElement == element)
            {
                SelectElement(null);
            }

            /* TODO:
             * If an element is removed from a collection within this collection,
             * this should also unselect the element.
             */
        }
    }
}
