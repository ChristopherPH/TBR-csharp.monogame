using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TheBlackRoom.MonoGame.GuiFramework
{
    /// <summary>
    /// Collection of Gui Elements
    /// </summary>
    public class GuiElementCollection : List<GuiElement>
    {
        public void Update(GameTime gameTime)
        {
            foreach (GuiElement element in this)
            {
                element?.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, ExtendedSpriteBatch spriteBatch)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed)
                return;

            foreach (GuiElement element in this)
            {
                element?.Draw(gameTime, spriteBatch);
            }
        }

        public GuiElement GetElementAt(Vector2 position)
        {
            foreach (GuiElement element in this)
                if (element?.HitTest(position) ?? false)
                    return element;

            return null;
        }

        public GuiElement GetElementAt(Point point)
        {
            foreach (GuiElement element in this)
                if (element?.HitTest(point) ?? false)
                    return element;

            return null;
        }

        public GuiElement GetElementAt(float x, float y)
        {
            foreach (GuiElement element in this)
                if (element?.HitTest(x, y) ?? false)
                    return element;

            return null;
        }
    }
}
