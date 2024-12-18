using Microsoft.Xna.Framework;
using TheBlackRoom.MonoGame.Drawing;

namespace TheBlackRoom.MonoGame.GuiFramework.Interfaces
{
    /// <summary>
    /// Gui Element Border Adornment
    /// </summary>
    public interface IGuiBorder : IGuiAdornment
    {
        /// <summary>
        /// Border edge thicknesses
        /// </summary>
        public Padding BorderThickness { get; }
    }
}
