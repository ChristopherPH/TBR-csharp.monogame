namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    public abstract class GuiElementColumnStyle { }
    public class GuiElementColumnStyleAbsolute : GuiElementColumnStyle
    {
        public GuiElementColumnStyleAbsolute() { }

        public GuiElementColumnStyleAbsolute(int width)
        {
            this.Width = width;
        }

        public int Width { get; set; } = 0;
    }

    public class GuiElementColumnStylePercent : GuiElementColumnStyle
    {
        public GuiElementColumnStylePercent() { }

        public GuiElementColumnStylePercent(float percent)
        {
            this.Percent = percent;
        }

        public float Percent { get; set; } = 100.0f; //must be >0
    }

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

    public abstract class GuiElementRowStyle { }

    public class GuiElementRowStyleAbsolute : GuiElementRowStyle
    {
        public GuiElementRowStyleAbsolute() { }

        public GuiElementRowStyleAbsolute(int height)
        {
            this.Height = height;
        }

        public int Height { get; set; } = 0;
    }

    public class GuiElementRowStylePercent : GuiElementRowStyle
    {
        public GuiElementRowStylePercent() { }

        public GuiElementRowStylePercent(float percent)
        {
            this.Percent = percent;
        }

        public float Percent { get; set; } = 100.0f; //must be >0
    }

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
