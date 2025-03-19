namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// Base class for column styles
    /// </summary>
    public abstract class GuiElementColumnStyle { }

    /// <summary>
    /// Absolute column style
    /// </summary>
    public class GuiElementColumnStyleAbsolute : GuiElementColumnStyle
    {
        public GuiElementColumnStyleAbsolute() { }

        public GuiElementColumnStyleAbsolute(int width)
        {
            this.Width = width;
        }

        public int Width { get; set; } = 0;
    }

    /// <summary>
    /// Percent column style
    /// </summary>
    public class GuiElementColumnStylePercent : GuiElementColumnStyle
    {
        public GuiElementColumnStylePercent() { }

        public GuiElementColumnStylePercent(float percent)
        {
            this.Percent = percent;
        }

        public float Percent { get; set; } = 100.0f; //must be >0
    }

    /// <summary>
    /// Variable percent column style
    /// </summary>
    public class GuiElementColumnStyleVariablePercent : GuiElementColumnStylePercent
    {
        public GuiElementColumnStyleVariablePercent() { }

        public GuiElementColumnStyleVariablePercent(float percent, int minimumWidth, int maximumWidth) :
            base(percent)
        {
            this.MinimumWidth = minimumWidth;
            this.MaximumWidth = maximumWidth;
        }

        public virtual int MinimumWidth { get; set; } = -1; //must be < max
        public virtual int MaximumWidth { get; set; } = -1; //must be > min
    }
}
