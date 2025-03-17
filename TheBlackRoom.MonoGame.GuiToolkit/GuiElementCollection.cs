using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TheBlackRoom.MonoGame.GuiToolkit
{
    /// <summary>
    /// Base class for a Gui Element Collection, which can contain other Gui Elements
    /// </summary>
    public abstract class GuiElementCollection : GuiElement
    {
        protected override void UpdateGuiElement(GameTime gameTime)
        {
            //Update all elements in collection
            foreach (GuiElement element in ChildElements)
            {
                element?.Update(gameTime);
            }
        }

        protected override void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
        {
            //Draw all elements in collection
            foreach (GuiElement element in ChildElements)
            {
                element?.Draw(gameTime, spriteBatch);
            }
        }

        protected IEnumerable<GuiElement> ReversedChildElements =>
            ((IEnumerable<GuiElement>)ChildElements).Reverse();

        /// <summary>
        /// Gets the child element at the given position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GuiElement GetElementAt(Vector2 position)
        {
            //Iterate back to front for correct z-order
            foreach (GuiElement element in ReversedChildElements)
            {
                if (!element.HitTest(position))
                    continue;

                //Found element, check if element is a collection
                if (element is GuiElementCollection guiElementCollection)
                {
                    //Return child element if hittest succeeds
                    var childElement = guiElementCollection.GetElementAt(position);
                    if (childElement != null)
                        return childElement;
                }

                //Return non collection element
                return element;
            }

            return null;
        }

        /// <summary>
        /// Gets the child element at the given point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public GuiElement GetElementAt(Point point) => GetElementAt(point.ToVector2());

        /// <summary>
        /// Gets the child element at the given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GuiElement GetElementAt(float x, float y) => GetElementAt(new Vector2(x, y));
    }
}
