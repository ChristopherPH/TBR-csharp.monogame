using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheBlackRoom.MonoGame;
using TheBlackRoom.MonoGame.Drawing;
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
                Text = "Demo",
                Font = _headerFont,
                ForeColour = Color.White,
                BackColour = Color.Firebrick,
                BorderColour = Color.DarkGray,
                BorderThickness = 10,
                DrawBorder = true,
                Bounds = new Rectangle(10, 10, 200, 100),
                Alignment = ContentAlignment.MiddleCenter,
            });
            });

            _listBox = new GuiListBox()
            {
                Font = _textFont,
                ForeColour = Color.White,
                BackColour = Color.Firebrick,
                BorderColour = Color.DarkGray,
                BorderThickness = 10,
                DrawBorder = true,
                Bounds = new Rectangle(310, 10, 200, 400),
                Alignment = ContentAlignment.MiddleLeft,
                Items = new System.Collections.Generic.List<object>
                {
                    "Zero",
                    "One",
                    "Two",
                },
                ItemHeight = 50,
                SelectedIndex = 1,
                ShowScrollbar = true,
                FormatString = "- {0} -",
            };
            _listBox.NotifyListItemsChanged();

            guiElements.Add(_listBox);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            guiElements.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
