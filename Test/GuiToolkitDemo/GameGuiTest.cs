using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using TheBlackRoom.MonoGame;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.Extensions;
using TheBlackRoom.MonoGame.GuiToolkit.Borders;
using TheBlackRoom.MonoGame.GuiToolkit.Elements;

namespace GuiToolkitDemo
{
    public class GameGuiTest : Game
    {
        private GraphicsDeviceManager _graphics;
        private ExtendedSpriteBatch _spriteBatch;
        private SpriteFont _headerFont;
        private SpriteFont _textFont;
        private Texture2D _grasstile;
        private GuiLayout guiLayout;

        public GameGuiTest()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 960;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _headerFont = Content.Load<SpriteFont>("headerFont");
            _textFont = Content.Load<SpriteFont>("textFont");
            _grasstile = Content.Load<Texture2D>("grasstile");

            CreateGui();
        }

        KeyboardState _LastKeyboardState = Keyboard.GetState();
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
                    System.Diagnostics.Debug.Print($"Clicked on {p}");
                }
            }

            guiLayout.Update(gameTime);

            _LastKeyboardState = keyboardState;
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

            for (int y = 0; y < 15; y++)
                for (int x = 0; x < 20; x++)
                    _spriteBatch.Draw(_grasstile, new Vector2(x * _grasstile.Width, y * _grasstile.Height));

            guiLayout.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void CreateGui()
        {
            Rectangle s;

            //1280 x 960
            var screenRect = new Rectangle(0, 0,
                _graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            var backColor = new Color(Color.BlanchedAlmond, 0.5f);

            //var border = new GuiSolidBorder(Color.Black, 2);
            //var border = new Gui3DBorder(backColor, 7);
            var border = new GuiRaisedBorder(backColor, 9);

            guiLayout = new GuiLayout()
            {
                Name = "gui",
                Size = new Point(_graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight),
            };

            //Left bar
            var leftBar = new GuiPanel()
            {
                Name = "leftBar",
                Bounds = new Rectangle(20, 20, 200, 600),
                BackColour = backColor,
                Border = border,
            };

            guiLayout.Add(leftBar);

            leftBar.Add(new GuiLabel()
            {
                Name = "charLabel",
                Bounds = leftBar.DockTop(60),
                Text = "Character",
                Font = _headerFont,
                Border = border,
                //Margin = new Padding(5),
            });

            leftBar.Add(new GuiLabel()
            {
                Name = "nameLabel",
                Bounds = leftBar.Elements.Last().AlignBottom(40),
                Text = "Name: Cloak",
                Font = _textFont,
                Alignment = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
            });

            leftBar.Add(new GuiLabel()
            {
                Name = "classLabel",
                Bounds = leftBar.Elements.Last().AlignBottom(40),
                Text = "Class: Evoker",
                Font = _textFont,
                Alignment = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
            });

            //Hotkey bar
            var r = new Rectangle(0, 0, 800, 80);
            s = screenRect.SliceBottom(150, out _);
            var t = r.AlignInside(s, ContentAlignment.MiddleCenter);

            var hotkeyBar = new GuiPanel()
            {
                Name = "hotkeyBar",
                Bounds = t,
                BackColour = backColor,
                Border = border,
            };

            guiLayout.Add(hotkeyBar);

            for (int i = 0; i < 10; i++)
            {
                var height = hotkeyBar.ContentHeight;

                hotkeyBar.Add(new GuiPictureBox()
                {
                    Name = $"hotkey {i}",
                    Bounds = new Rectangle(i * height, 0, height, height),
                    BackColour = backColor,
                    Border = new GuiSolidBorder(Color.Red, 2),
                    Margin = new Padding(1),
                });
            }
        }
    }
}
