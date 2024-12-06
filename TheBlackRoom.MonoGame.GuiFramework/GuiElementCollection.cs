using System.Collections.Generic;

namespace TheBlackRoom.MonoGame.GuiFramework
{
    /// <summary>
    /// Collection of Gui Elements
    /// </summary>
    public class GuiElementCollection : List<GuiElement>
    {
        public void Draw(ExtendedSpriteBatch spriteBatch)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed)
                return;

            foreach (GuiElement element in this)
            {
                element?.Draw(spriteBatch);
            }
        }
    }
}
