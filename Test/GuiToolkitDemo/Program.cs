Microsoft.Xna.Framework.Game game = null;

int testNumber = 1;

switch (testNumber)
{
    case 1:
        game = new GuiToolkitDemo.ElementTest();
        break;

    case 2:
        game = new GuiToolkitDemo.AnchorTest();
        break;

    case 3:
        game = new GuiToolkitDemo.GameGuiTest();
        break;
}

game?.Run();
game?.Dispose();
