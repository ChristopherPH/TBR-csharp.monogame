namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Base class for row styles
    /// </summary>
    public abstract class GuiElementRowStyle { }

    /// <summary>
    /// Absolute row style
    /// </summary>
    public class GuiElementRowStyleAbsolute : GuiElementRowStyle
    {
        public GuiElementRowStyleAbsolute() { }

        public GuiElementRowStyleAbsolute(int height)
        {
            this.Height = height;
        }

        public int Height { get; set; } = 0;
    }

    /// <summary>
    /// Percent row style
    /// </summary>
    public class GuiElementRowStylePercent : GuiElementRowStyle
    {
        public GuiElementRowStylePercent() { }

        public GuiElementRowStylePercent(float percent)
        {
            this.Percent = percent;
        }

        public float Percent { get; set; } = 100.0f; //must be >0
    }

    /// <summary>
    /// Variable percent row style
    /// </summary>
    public class GuiElementRowStyleVariablePercent : GuiElementRowStylePercent
    {
        public GuiElementRowStyleVariablePercent() { }

        public GuiElementRowStyleVariablePercent(float percent, int minimumHeight, int maximumHeight) :
            base(percent)
        {
            this.MinimumHeight = minimumHeight;
            this.MaximumHeight = maximumHeight;
        }

        public virtual int MinimumHeight { get; set; } = -1; //must be < max
        public virtual int MaximumHeight { get; set; } = -1; //must be > min
    }

}
