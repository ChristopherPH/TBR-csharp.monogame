using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace TheBlackRoom.MonoGame.GameFramework
{
    /// <summary>
    /// Game State class that can be added to a Game Engine, allowing
    /// the engine to swap between active states. The Game State can
    /// maintain its own content, has its own Update() and Draw()
    /// routines, and gets notified when the state has been activated
    /// or deactivated.
    ///
    /// Only one state can be active at a time, although previous
    /// states may be rendered if the current state only overlays
    /// part of the previous state (a popup menu, for instance)
    /// </summary>
    public abstract class GameState : IDisposable
    {
        /// <summary>
        /// Creates a new GameState and defers initialization
        /// </summary>
        public GameState() { }

        /// <summary>
        /// Creates a new GameState and initializes the GameState immediately
        /// </summary>
        /// <param name="Engine"></param>
        public GameState(GameEngine Engine)
        {
            Initialize(Engine);
        }

        /// <summary>
        /// GameEngine that manages this GameState
        /// </summary>
        protected GameEngine Engine { get; private set; }

        /// <summary>
        /// Content Manager for this GameState
        /// </summary>
        protected ContentManager Content { get; private set; }

        /// <summary>
        /// Optional ContentRoot for content manager, set to blank to
        /// use that of the GameEngine
        /// </summary>
        protected virtual string ContentRoot => "";

        /// <summary>
        /// Flag that indicates that all content contained in the content
        /// manager should be unloaded when the GameState is uninitialized
        /// </summary>
        protected bool AutoUnloadContent => false;

        /// <summary>
        /// Flag that indicates that previous states should be rendered before
        /// this state is rendered (eg. this GameState is an overlay)
        /// </summary>
        public virtual bool RenderPreviousState => false;

        /// <summary>
        /// Total amount of time spent in state
        /// </summary>
        protected double stateTime { get; private set; }

        /// <summary>
        /// Flag that indicates whether GameState has been initialized or not
        /// </summary>
        public bool Initialized { get; private set; } = false;


        /// <summary>
        /// Initializes the GameState if not previously initialized
        /// </summary>
        /// <param name="gameEngine"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Initialize(GameEngine gameEngine)
        {
            if (Initialized)
                return;

            if (gameEngine == null)
                throw new ArgumentNullException(nameof(gameEngine));

            // Save engine
            Engine = gameEngine;

            // Create a new content manager to load content used just by this state.
            if (string.IsNullOrEmpty(this.ContentRoot))
                Content = new ContentManager(Engine.Services, Engine.Content.RootDirectory);
            else
                Content = new ContentManager(Engine.Services, ContentRoot);

            // Mark state as initialized before loading content
            Initialized = true;

            LoadContent();
        }

        /// <summary>
        /// Cleans up the GameState if previously initialized
        /// </summary>
        protected void Deinitialize()
        {
            if (!Initialized)
                return;
            
            // Unload content before un-initalizing state
            UnloadContent();

            Engine = null;
            Initialized = false;
        }


        /// <summary>
        /// Called when state is pushed on stack (resumed == false)
        /// or the state on top of this state was popped off stack (resumed == true)
        /// </summary>
        public virtual void OnStateStarted(bool Resumed) { }

        /// <summary>
        /// called when state is popped off stack (paused == false)
        /// or a state is pushed on top of this state (paused == true)
        /// </summary>
        public virtual void OnStateStopped(bool Paused) { }

        protected virtual void LoadContent() { }

        protected virtual void UnloadContent()
        {
            if (AutoUnloadContent)
                Content?.Unload();
        }

        public virtual void Draw(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch,
            Rectangle GameRectangle)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            stateTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        protected void CompleteState() => Engine?.CompleteCurrentState();
        protected void AddState(GameState state) => Engine?.AddState(state);
        protected void ChangeToState(GameState state) => Engine?.ChangeToState(state);

        public void Dispose()
        {
            Deinitialize();

            Content?.Dispose();
            Content = null;
        }
    }
}
