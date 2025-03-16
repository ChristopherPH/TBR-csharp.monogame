using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TheBlackRoom.MonoGame.GuiToolkit
{
    /// <summary>
    /// Collection of Gui Elements
    /// </summary>
    public class GuiElementCollection
    {
        protected List<GuiElement> Elements { get; } = new List<GuiElement>();

        public void Add(GuiElement element)
        {
            if (element == null)
                return;

            var ix = Elements.Count;

            OnElementAdding(ix, element);

            Elements.Add(element);

            OnElementAdded(ix, element);
        }

        public void Remove(GuiElement element)
        {
            if (element == null)
                return;

            var ix = Elements.IndexOf(element);
            if (ix == -1)
                return;

            OnElementRemoving(ix, element);

            Elements.RemoveAt(ix);

            OnElementRemoved(ix, element);
        }

        public void Update(GameTime gameTime)
        {
            foreach (GuiElement element in Elements)
            {
                element?.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, ExtendedSpriteBatch spriteBatch)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed)
                return;

            foreach (GuiElement element in Elements)
            {
                element?.Draw(gameTime, spriteBatch);
            }
        }

        public GuiElement GetElementAt(Vector2 position)
        {
            foreach (GuiElement element in Elements)
                if (element?.HitTest(position) ?? false)
                    return element;

            return null;
        }

        public GuiElement GetElementAt(Point point)
        {
            foreach (GuiElement element in Elements)
                if (element?.HitTest(point) ?? false)
                    return element;

            return null;
        }

        public GuiElement GetElementAt(float x, float y)
        {
            foreach (GuiElement element in Elements)
                if (element?.HitTest(x, y) ?? false)
                    return element;

            return null;
        }

        /// <summary>
        /// Occurs when the Gui Element will be added to the
        /// Gui Element Collection
        /// </summary>
        protected virtual void OnElementAdding(int index, GuiElement element) { }

        /// <summary>
        /// Occurs when the Gui Element has been added to the
        /// Gui Element Collection
        /// </summary>
        protected virtual void OnElementAdded(int index, GuiElement element) { }

        /// <summary>
        /// Occurs when the Gui Element will be removed from the
        /// Gui Element Collection
        /// </summary>
        protected virtual void OnElementRemoving(int index, GuiElement element) { }

        /// <summary>
        /// Occurs when the Gui Element has been removed from the
        /// Gui Element Collection
        /// </summary>
        protected virtual void OnElementRemoved(int index, GuiElement element) { }
    }
}
