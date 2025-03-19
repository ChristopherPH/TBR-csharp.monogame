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
        protected List<GuiElement> ElementCollection { get; } = new List<GuiElement>();

        protected override void UpdateGuiElement(GameTime gameTime)
        {
            //Update all elements in collection
            foreach (GuiElement element in ElementCollection)
            {
                element?.Update(gameTime);
            }
        }

        protected override void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
        {
            //Draw all elements in collection
            foreach (GuiElement element in ElementCollection)
            {
                element?.Draw(gameTime, spriteBatch);
            }
        }


        /// <summary>
        /// Adds an element to the element collection
        /// </summary>
        /// <param name="element"></param>
        /// <returns>true if element added</returns>
        protected bool AddCollectionElement(GuiElement element)
        {
            if (element == null)
                return false;

            //Element already added to this element
            if (element.ParentElement == this)
                return false;

            //Element already added to another element, remove it
            if (element.ParentElement is GuiElementCollection guiElementCollection)
                guiElementCollection.RemoveCollectionElement(element);

            //Add element
            var ix = ElementCollection.Count;

            OnElementAdding(ix, element);

            ElementCollection.Add(element);
            element.ParentElement = this;

            OnElementAdded(ix, element);

            return true;
        }

        /// <summary>
        /// Removes an element from the Gui Element
        /// </summary>
        /// <param name="element"></param>
        /// <returns>true if the element removed</returns>
        protected bool RemoveCollectionElement(GuiElement element)
        {
            if (element == null)
                return false;

            //Get index of element
            var ix = ElementCollection.IndexOf(element);

            //Element not added to this element
            if (ix == -1)
                return false;

            //Remove element
            OnElementRemoving(ix, element);

            ElementCollection.RemoveAt(ix);
            element.ParentElement = null;

            OnElementRemoved(ix, element);

            return true;
        }

        protected IEnumerable<GuiElement> ReversedElementCollection =>
            ((IEnumerable<GuiElement>)ElementCollection).Reverse();

        /// <summary>
        /// Returns all elements in element collection
        /// </summary>
        public IEnumerable<GuiElement> Elements => ElementCollection;

        /// <summary>
        /// Returns all descendants in element collection
        /// </summary>
        public IEnumerable<GuiElement> Descendants
        {
            get
            {
                foreach (var element in ElementCollection)
                {
                    yield return element;

                    if (element is GuiElementCollection guiElementCollection)
                    {
                        foreach (var descendantElement in guiElementCollection.Descendants)
                        {
                            yield return descendantElement;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the collection element at the given position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GuiElement GetElementAt(Vector2 position)
        {
            //Iterate back to front for correct z-order
            foreach (GuiElement element in ReversedElementCollection)
            {
                if (!element.HitTest(position))
                    continue;

                //Found element, check if element is a collection
                if (element is GuiElementCollection guiElementCollection)
                {
                    //Return inner element if hittest succeeds
                    var innerElement = guiElementCollection.GetElementAt(position);

                    if (innerElement != null)
                        return innerElement;
                }

                //Return non collection element
                return element;
            }

            return null;
        }

        /// <summary>
        /// Gets the element at the given point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public GuiElement GetElementAt(Point point) => GetElementAt(point.ToVector2());

        /// <summary>
        /// Gets the element at the given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GuiElement GetElementAt(float x, float y) => GetElementAt(new Vector2(x, y));


        /// <summary>
        /// Occurs when the Gui Element will be added to the
        /// element collection
        /// </summary>
        protected virtual void OnElementAdding(int index, GuiElement element) { }

        /// <summary>
        /// Occurs when the Gui Element has been added to the
        /// element collection
        /// </summary>
        protected virtual void OnElementAdded(int index, GuiElement element) { }

        /// <summary>
        /// Occurs when the Gui Element will be removed from the
        /// element collection
        /// </summary>
        protected virtual void OnElementRemoving(int index, GuiElement element) { }

        /// <summary>
        /// Occurs when the Gui Element has been removed from the
        /// element collection
        /// </summary>
        protected virtual void OnElementRemoved(int index, GuiElement element) { }
    }
}
