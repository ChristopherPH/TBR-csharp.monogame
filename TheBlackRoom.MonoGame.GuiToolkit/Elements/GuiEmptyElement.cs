using Microsoft.Xna.Framework;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Gui element that does nothing, useful for a hittest,
    /// only contains base properties
    /// </summary>
    public class GuiEmptyElement : GuiElement
    {
        protected override void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
        { }

        protected override void UpdateGuiElement(GameTime gameTime) { }
    }
}
