using Microsoft.Xna.Framework;

namespace TheBlackRoom.MonoGame.GuiToolkit.Interfaces
{
    /// <summary>
    /// Base interface for decorating a Gui Element
    /// </summary>
    public interface IGuiAdornment
    {
        public void Update(GameTime gameTime);
        public void Draw(GameTime gameTime, ExtendedSpriteBatch spriteBatch, Rectangle bounds);
    }
}
