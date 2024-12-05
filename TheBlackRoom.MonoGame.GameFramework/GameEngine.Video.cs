using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheBlackRoom.MonoGame.External;

namespace TheBlackRoom.MonoGame.GameFramework
{
    public partial class GameEngine
    {
        private void InitVideo(int width, int height,
            VideoSettings.WindowModeTypes windowMode, bool vSync)
        {
            graphics = new GraphicsDeviceManager(this);

            IndependentResolutionRendering.Init(ref graphics);

            //Virtual Resolution (Game Resolution):
            //  Amount of pixels for Draw() to draw on. This will be scaled to the Window resolution for display.
            //  Note: VirtualResolution doesn't need to be a valid resolution
            IndependentResolutionRendering.SetVirtualResolution(GameResolutionWidth, GameResolutionHeight);

            SetResolution(width, height, windowMode, vSync, false);
        }

        GraphicsDeviceManager graphics;

        public void BeginBatchDraw(bool ScissorTest = false)
        {
            /* SpriteSortMode.Immediate draws a sprite right away, whereas
             * SpriteSortMode.Deferred waits until spriteBatch.End().
             * When using RasterizerState.ScissorTestEnable = true, one can
             * set spriteBatch.GraphicsDevice.ScissorRectangle to crop the
             * draw. Note that Immediate will allow changes to the
             * ScissorRectangle for each draw, whereas Deferred takes the
             * last ScissorRectangle, so this requires multiple spriteBatch
             * Begin() and End() calls.
             *
             * According to the answer of the following link:
             * https://gamedev.stackexchange.com/questions/82799/in-monogame-why-is-multiple-tile-drawing-slow-when-rendering-in-windowed-fulls
             * It is best to use the same texture as much as possible when
             * using Deferred, as it avoids a texture flush. This means
             * group draws to the same texture as much as possible.
             */
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                null, null,
                new RasterizerState() { ScissorTestEnable = ScissorTest },
                null, DrawMatrix);
        }

        public void EndBatchDraw()
        {
            spriteBatch.End();
        }

        protected Matrix DrawMatrix => IndependentResolutionRendering.getTransformationMatrix();
        public Rectangle GameRectangle => new Rectangle(0, 0, GameResolutionWidth, GameResolutionHeight);

        protected Vector2 TranslateWindowToGame(Vector2 Location)
        {
            var vp = new Vector2(
                IndependentResolutionRendering.ViewportX,
                IndependentResolutionRendering.ViewportY);

            return Vector2.Transform(Location - vp, Matrix.Invert(DrawMatrix));
        }

        public void SetResolution(int Width, int Height, VideoSettings.WindowModeTypes WindowMode, bool VSync, bool SaveSettings)
        {
            this.IsFixedTimeStep = VSync;
            graphics.SynchronizeWithVerticalRetrace = VSync;

            switch (WindowMode)
            {
                case VideoSettings.WindowModeTypes.Windowed:
                    Window.IsBorderless = false;

                    var x = (GraphicsDevice.DisplayMode.Width - Width) / 2;
                    var y = (GraphicsDevice.DisplayMode.Height - Height) / 2;
                    Window.Position = new Point(x, y);

                    IndependentResolutionRendering.SetResolution(Width, Height, false);
                    break;


                case VideoSettings.WindowModeTypes.Fullscreen:
                    Window.IsBorderless = false;
                    IndependentResolutionRendering.SetResolution(Width, Height, true);
                    break;


                case VideoSettings.WindowModeTypes.WindowedFullscreen:
                    Window.IsBorderless = true;
                    Window.Position = new Point(0, 0);
                    IndependentResolutionRendering.SetResolution(
                        GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height, false);
                    break;
            }

            //TODO: Ensure video changed successfully before saving


            if (SaveSettings)
            {
                GameEngineSettings.Video.Width = Width;
                GameEngineSettings.Video.Height = Height;
                GameEngineSettings.Video.WindowMode = WindowMode;
                GameEngineSettings.Video.VSync = VSync;

                GameEngineSettings.SaveSettings(SettingsFile);
            }
        }
    }
}
