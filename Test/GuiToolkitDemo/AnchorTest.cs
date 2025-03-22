using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheBlackRoom.MonoGame;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.GuiToolkit.Borders;
using TheBlackRoom.MonoGame.GuiToolkit.Elements;

namespace GuiToolkitDemo
{
    public class AnchorTest : Game
    {
        private GraphicsDeviceManager _graphics;
        private ExtendedSpriteBatch _spriteBatch;
        private SpriteFont _headerFont;
        private SpriteFont _textFont;
        private GuiLayout guiLayout;

        public AnchorTest()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Window.AllowUserResizing = true;
            Window.Title = "User Resize Test";

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 960;
            _graphics.ApplyChanges();

            Window.ClientSizeChanged += Window_ClientSizeChanged;

            base.Initialize();
        }

        private bool changingSize = false;

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            if (changingSize)
                return;

            changingSize = true;

            _graphics.PreferredBackBufferWidth = MathHelper.Max(640, Window.ClientBounds.Width);
            _graphics.PreferredBackBufferHeight = MathHelper.Max(480, Window.ClientBounds.Height);
            _graphics.ApplyChanges();

            if (guiLayout != null)
                guiLayout.Size = Window.ClientBounds.Size;

            changingSize = false;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _headerFont = Content.Load<SpriteFont>("headerFont");
            _textFont = Content.Load<SpriteFont>("textFont");

            LoadGuiLayout(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        MouseState _LastMouseState = Mouse.GetState();

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (mouseState.LeftButton.HasFlag(ButtonState.Pressed) && !_LastMouseState.LeftButton.HasFlag(ButtonState.Pressed))
            {
                var p = new Point(mouseState.X, mouseState.Y);

                var element = guiLayout.GetElementAt(p);
                if (element != null)
                {
                    System.Diagnostics.Debug.Print($"Clicked on {element.Name} at {p} ");
                }
                else
                {
                    System.Diagnostics.Debug.Print($"Clicked on {p} but no element");
                }
            }

            guiLayout.Update(gameTime);

            _LastMouseState = mouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Immediate,
                BlendState.NonPremultiplied,
                null, null,
                new RasterizerState() { ScissorTestEnable = true },
                null, null);

            guiLayout.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void LoadGuiLayout(int width, int height)
        {
            guiLayout = new GuiLayout()
            {
                Name = "gui",
                Size = new Point(width, height),
                BackColour = Color.Teal,
            };

            var border = new GuiSolidBorder(Color.DarkGray, 3);

            //Divide screen into 4 equal squares
            var quad_tl = guiLayout.Bounds.SliceTopPercent(0.5f, out var quad_bl);
            var quad_tr = quad_tl.SliceRightPercent(0.5f, out quad_tl);
            var quad_br = quad_bl.SliceRightPercent(0.5f, out quad_bl);

            //Quadrant top left
            //Divide rectangle into 9 equal squares
            var rect = quad_tl;
            var midleft = rect.SliceLeftPercent(0.33f, out var center);
            var topleft = midleft.SliceTopPercent(0.33f, out midleft);
            var botleft = midleft.SliceBottomPercent(0.5f, out midleft);

            var midright = center.SliceRightPercent(0.5f, out center);
            var topright = midright.SliceTopPercent(0.33f, out midright);
            var botright = midright.SliceBottomPercent(0.5f, out midright);

            var topcent = center.SliceTopPercent(0.33f, out center);
            var botcent = center.SliceBottomPercent(0.5f, out center);

            guiLayout.Add(new GuiLabel(_textFont, "Top Left")
            {
                Bounds = topleft,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.TopLeft);

            guiLayout.Add(new GuiLabel(_textFont, "Top Left Right")
            {
                Bounds = topcent,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.TopLeftRight);

            guiLayout.Add(new GuiLabel(_textFont, "Top Right")
            {
                Bounds = topright,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.TopRight);


            guiLayout.Add(new GuiLabel(_textFont, "Left Top Bottom")
            {
                Bounds = midleft,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.LeftTopBottom);

            guiLayout.Add(new GuiLabel(_textFont, "Left Top Right Bottom")
            {
                Bounds = center,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.All);

            guiLayout.Add(new GuiLabel(_textFont, "Right Top Bottom")
            {
                Bounds = midright,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.RightTopBottom);


            guiLayout.Add(new GuiLabel(_textFont, "Bottom Left")
            {
                Bounds = botleft,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.BottomLeft);

            guiLayout.Add(new GuiLabel(_textFont, "Bottom Left Right")
            {
                Bounds = botcent,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.BottomLeftRight);

            guiLayout.Add(new GuiLabel(_textFont, "Bottom Right")
            {
                Bounds = botright,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.BottomRight);

            //Quadrant top right
            //Divide rectangle into 4 equal squares
            rect = quad_tr;
            var tl = rect.SliceTopPercent(0.5f, out var bl);
            var tr = tl.SliceRightPercent(0.5f, out tl);
            var br = bl.SliceRightPercent(0.5f, out bl);

            guiLayout.Add(new GuiLabel(_textFont, "Left")
            {
                Bounds = tl,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.Left);


            guiLayout.Add(new GuiLabel(_textFont, "Right")
            {
                Bounds = tr,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.Right);

            guiLayout.Add(new GuiLabel(_textFont, "Top")
            {
                Bounds = bl,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.Top);

            guiLayout.Add(new GuiLabel(_textFont, "Bottom")
            {
                Bounds = br,
                BackColour = Color.Firebrick,
                Border = border,
                Margin = new Padding(5),
            }, GuiElementAnchorStyles.Bottom);


            //Quadrant bottom left
            var guiTableLayout = new GuiTablePanel(
                [
                    new GuiElementColumnStyleAbsolute(50),
                    new GuiElementColumnStylePercent(50),
                    new GuiElementColumnStylePercent(50),
                    new GuiElementColumnStyleVariablePercent(100, 25, 75),
                ],
                [
                    new GuiElementRowStyleAbsolute(50),
                    new GuiElementRowStylePercent(50),
                    new GuiElementRowStylePercent(50),
                    new GuiElementRowStyleVariablePercent(100, 25, 75),
                    new GuiElementRowStyleVariablePercent(100, 25, 75),
                ])
            {
                Name = "content",
                Bounds = quad_bl,
                BackColour = Color.AliceBlue,
            };

            guiLayout.Add(guiTableLayout);

            //Left bar
            guiTableLayout.Add(new GuiLabel(_textFont, "Left Bar"), 0, 1, 1, guiTableLayout.RowCount);
        }
    }
}
