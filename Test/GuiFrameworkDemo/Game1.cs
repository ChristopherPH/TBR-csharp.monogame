using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheBlackRoom.MonoGame;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.Extensions;
using TheBlackRoom.MonoGame.GuiFramework;

namespace GuiFrameworkDemo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private ExtendedSpriteBatch _spriteBatch;
        private SpriteFont _headerFont;
        private SpriteFont _textFont;
        private GuiListBox _listBox;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        GuiElementCollection guiElements = new GuiElementCollection();

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _headerFont = Content.Load<SpriteFont>("headerFont");
            _textFont = Content.Load<SpriteFont>("textFont");

            guiElements.Add(new GuiLabel()
            {
                //GuiElement Properties
                Name = "lblCenter",
                Bounds = new Rectangle(10, 10, 200, 100),
                BackColour = Color.Firebrick,
                DrawBorder = true,
                BorderColour = Color.DarkGray,
                BorderThickness = 10,

                //GuiTextElement Properties
                Font = _headerFont,
                ForeColour = Color.White,

                //GuiLabel Properties
                Text = "Center",
                Alignment = ContentAlignment.MiddleCenter,
                Padding = Padding.Empty,
            });

            guiElements.Add(new GuiLabel()
            {
                Name = "lblPadding",
                Bounds = new Rectangle(10, 120, 200, 100),

                BackColour = Color.Firebrick,
                DrawBorder = true,
                BorderColour = Color.DarkGray,
                BorderThickness = 10,

                Font = _headerFont,
                ForeColour = Color.White,

                Text = "Padding",
                Alignment = ContentAlignment.BottomRight,
                Padding = new Padding(0, 0, 20, 5),
            });

            _listBox = new GuiListBox()
            {
                //GuiElement Properties
                Name = "lbBasic",
                Bounds = new Rectangle(310, 10, 200, 300),
                BackColour = Color.Firebrick,
                DrawBorder = true,
                BorderThickness = 10,
                BorderColour = Color.DarkGray,

                //GuiTextElement Properties
                Font = _textFont,
                ForeColour = Color.White,

                //GuiListBox Properties
                Items = new System.Collections.Generic.List<object>
                {
                    "Zero",
                    "One",
                    "Two",
                },

                ItemHeight = 50,
                Alignment = ContentAlignment.MiddleLeft,
                FormatString = "- {0} -",
                OwnerDraw = false,
                GetItemText = null,
                ShowScrollbar = true,
                SelectedIndex = 1,
            };

            _listBox.Items.Add("Three");
            _listBox.NotifyListItemsChanged();

            guiElements.Add(_listBox);


            var lbOwnerDraw = new GuiListBox()
            {
                //GuiElement Properties
                Name = "lbOwnerDraw",
                Bounds = new Rectangle(520, 10, 200, 300),
                BackColour = Color.Firebrick,
                DrawBorder = true,
                BorderThickness = 10,
                BorderColour = Color.DarkGray,

                //GuiTextElement Properties
                Font = _textFont,
                ForeColour = Color.White,

                //GuiListBox Properties
                Items = new System.Collections.Generic.List<object>
                {
                    "Zero",
                    "One",
                    "Two",
                },

                ItemHeight = 50,
                Alignment = ContentAlignment.MiddleLeft,
                ShowScrollbar = true,
                SelectedIndex = 1,

                OwnerDraw = true,
            };

            lbOwnerDraw.GetItemText = (o) => $"{o} {o}";
            lbOwnerDraw.DrawItem += (s, e) =>
            {
                e.spriteBatch.FillRectangle(e.itemBounds,
                    e.selected ? Color.DarkGreen : Color.WhiteSmoke);
                e.spriteBatch.DrawString(lbOwnerDraw.Font, e.itemText, e.itemBounds,
                    ContentAlignment.MiddleCenter, Color.Black, e.selected ? 1.2f : 1.0f);
            };

            guiElements.Add(lbOwnerDraw);


            guiElements.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileTL",
                Bounds = new Rectangle(10, 250, 100, 100),
                DrawBorder = true,
                BorderColour = Color.DarkGray,
                BorderThickness = 1,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.BottomRight,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.Crop,
            });


            guiElements.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileBL",
                Bounds = new Rectangle(10, 375, 40, 40),
                DrawBorder = true,
                BorderColour = Color.DarkGray,
                BorderThickness = 1,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.TopLeft,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.Crop,
            });


            guiElements.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileTR",
                Bounds = new Rectangle(120, 250, 140, 100),
                DrawBorder = true,
                BorderColour = Color.DarkGray,
                BorderThickness = 1,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.TopCenter,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.ScaleToFit,
            });


            guiElements.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileBR",
                Bounds = new Rectangle(120, 375, 140, 100),
                DrawBorder = true,
                BorderColour = Color.DarkGray,
                BorderThickness = 1,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.TopRight,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.ScaleAspect,
            });
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

            if (keyboardState.IsKeyDown(Keys.Down) && _LastKeyboardState.IsKeyUp(Keys.Down))
                _listBox.SelectionDown();
            else if (keyboardState.IsKeyDown(Keys.Up) && _LastKeyboardState.IsKeyUp(Keys.Up))
                _listBox.SelectionUp();
            else if (keyboardState.IsKeyDown(Keys.Home) && _LastKeyboardState.IsKeyUp(Keys.Home))
                _listBox.SelectionTop();
            else if (keyboardState.IsKeyDown(Keys.End) && _LastKeyboardState.IsKeyUp(Keys.End))
                _listBox.SelectionBottom();

            if (mouseState.LeftButton.HasFlag(ButtonState.Pressed) && !_LastMouseState.LeftButton.HasFlag(ButtonState.Pressed))
            {
                var p = new Point(mouseState.X, mouseState.Y);
                var element = guiElements.GetElementAt(p);
                if (element != null)
                {
                    System.Diagnostics.Debug.Print($"Cicked on {element.Name} at {p}");
                }
                else
                {
                    System.Diagnostics.Debug.Print($"Cicked on {p}");
                }
            }

            guiElements.Update(gameTime);

            _LastKeyboardState = keyboardState;
            _LastMouseState = mouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null,
                new RasterizerState() { ScissorTestEnable = true }, null, null);

            guiElements.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
