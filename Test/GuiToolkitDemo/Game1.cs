using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheBlackRoom.MonoGame;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.MonoGame.Extensions;
using TheBlackRoom.MonoGame.GuiToolkit;
using TheBlackRoom.MonoGame.GuiToolkit.Borders;
using TheBlackRoom.MonoGame.GuiToolkit.Elements;

namespace GuiToolkitDemo
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

        GuiLayout guiLayout;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _headerFont = Content.Load<SpriteFont>("headerFont");
            _textFont = Content.Load<SpriteFont>("textFont");

            var thickBorder = new GuiSolidBorder(Color.DarkGray, 10);

            guiLayout = new GuiLayout()
            {
                Size = new Point(_graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight),
            };

            guiLayout.Add(new GuiLabel()
            {
                //GuiElement Properties
                Name = "lblCenter",
                Bounds = new Rectangle(10, 10, 200, 100),
                BackColour = Color.Firebrick,
                Border = thickBorder,

                //GuiTextElement Properties
                Font = _headerFont,
                ForeColour = Color.White,

                //GuiLabel Properties
                Text = "Center",
                Alignment = ContentAlignment.MiddleCenter,
                Padding = Padding.Empty,
            });

            guiLayout.Add(new GuiLabel()
            {
                Name = "lblPadding",
                Bounds = new Rectangle(10, 120, 200, 100),

                BackColour = Color.Firebrick,
                Border = thickBorder,

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
                Border = thickBorder,

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

            guiLayout.Add(_listBox);


            var lbOwnerDraw = new GuiListBox()
            {
                //GuiElement Properties
                Name = "lbOwnerDraw",
                Bounds = new Rectangle(520, 10, 200, 300),
                BackColour = Color.Firebrick,
                Border = thickBorder,

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

            guiLayout.Add(lbOwnerDraw);


            var thinBorder = new GuiSolidBorder(Color.DarkGray, 1);

            guiLayout.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileTL",
                Bounds = new Rectangle(10, 250, 100, 100),
                Border = thinBorder,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.BottomRight,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.Crop,
            });


            guiLayout.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileBL",
                Bounds = new Rectangle(10, 375, 40, 40),
                Border = thinBorder,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.TopLeft,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.Crop,
            });


            guiLayout.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileTR",
                Bounds = new Rectangle(120, 250, 140, 100),
                Border = thinBorder,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.TopCenter,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.ScaleToFit,
            });


            guiLayout.Add(new GuiPictureBox()
            {
                //GuiElement Properties
                Name = "pbTileBR",
                Bounds = new Rectangle(120, 375, 140, 100),
                Border = thinBorder,

                //GuiPictureBox Properties
                Alignment = ContentAlignment.TopRight,
                Picture = Content.Load<Texture2D>("avatar"),
                ScaleMode = ScaleModes.ScaleAspect,
            });


            //Show relative positioning
            var relativePanel = new GuiPanel()
            {
                //GuiElement Properties
                Name = "panel",
                Bounds = new Rectangle(850, 50, 250, 300),
                BackColour = Color.Firebrick,
            };

            relativePanel.Add(new GuiLabel(_textFont, "Dock\nLeft")
            {
                Bounds = relativePanel.DockLeft(75),
                Border = thinBorder,
            });

            relativePanel.Add(new GuiLabel(_textFont, "Dock Top")
            {
                Bounds = relativePanel.DockTop(25),
                Border = thinBorder,
            });

            relativePanel.Add(new GuiLabel(_textFont, "Dock\nRight")
            {
                Bounds = relativePanel.DockRight(75),
                Border = thinBorder,
            });

            relativePanel.Add(new GuiLabel(_textFont, "Dock Bottom")
            {
                Bounds = relativePanel.DockBottom(25),
                Border = thinBorder,
            });

            guiLayout.Add(relativePanel);

            guiLayout.Add(new GuiLabel(_textFont, "Align\nLeft")
            {
                Bounds = relativePanel.AlignLeft(75),
                Border = thinBorder,
            });

            guiLayout.Add(new GuiLabel(_textFont, "Align Top")
            {
                Bounds = relativePanel.AlignTop(25),
                Border = thinBorder,
            });

            guiLayout.Add(new GuiLabel(_textFont, "Align\nRight")
            {
                Bounds = relativePanel.AlignRight(75),
                Border = thinBorder,
            });

            guiLayout.Add(new GuiLabel(_textFont, "Align Bottom")
            {
                Bounds = relativePanel.AlignBottom(25),
                Border = thinBorder,
            });

        }

        void DrawRectangleSlices(ExtendedSpriteBatch spriteBatch)
        {
            var rect = new Rectangle(310, 350, 400, 100);

            int thick = 5;
            GuiDraw.DrawBorder(_spriteBatch, rect, Color.Black, thick);
            rect.Shrink(thick);

            var leftRect = rect.SliceLeft(100, out rect);
            GuiDraw.DrawBorder(_spriteBatch, leftRect, Color.Blue, thick);

            var rightRect = rect.SliceRightPercent(0.25f, out rect);
            GuiDraw.DrawBorder(_spriteBatch, rightRect, Color.Purple, thick);

            var topRect = rect.SliceTopPercent(0.75f, out rect);
            GuiDraw.DrawBorder(_spriteBatch, topRect, Color.Red, thick);

            var bottomRect = topRect.SliceBottom(50, out var innerRect);
            bottomRect.Shrink(thick);
            GuiDraw.DrawBorder(_spriteBatch, bottomRect, Color.Yellow, thick);

            GuiDraw.DrawBorder(_spriteBatch, rect, Color.Green, thick);
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
                var element = guiLayout.GetElementAt(p);
                if (element != null)
                {
                    System.Diagnostics.Debug.Print($"Cicked on {element.Name} at {p}");
                }
                else
                {
                    System.Diagnostics.Debug.Print($"Cicked on {p}");
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
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null,
                new RasterizerState() { ScissorTestEnable = true }, null, null);

            guiLayout.Draw(gameTime, _spriteBatch);

            DrawRectangleSlices(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
