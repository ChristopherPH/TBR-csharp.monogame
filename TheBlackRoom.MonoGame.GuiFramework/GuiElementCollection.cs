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
    }
}
