﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace TheBlackRoom.MonoGame.GameStateEngine
{
    public abstract class GameState : IDisposable
    {
        /// <summary>
        /// called when state is pushed on stack (resumed == false)
        /// or the state on top of this state was popped off stack (resumed == true)
        /// </summary>
        public virtual void OnStateStarted(bool Resumed) { }

        /// <summary>
        /// called when state is popped off stack (paused == false)
        /// or a state is pushed on top of this state (paused == true)
        /// </summary>
        public virtual void OnStateStopped(bool Paused) { }

        public abstract void Draw(GameTime gameTime, ExtendedSpriteBatch spriteBatch, Rectangle GameRectangle);

        public abstract void Update(GameTime gameTime, ref GameStateOperation Operation);


        /// <summary>
        /// Amount of time spent in state
        /// </summary>
        public double stateTimer { get; set; }

        protected ContentManager Content { get; private set; }
        protected GameEngine Engine;

        /*
         * GameState control options below
         */

        /// <summary>
        /// Base path for content
        /// </summary>
        protected virtual string ContentRoot => "Content";

        public virtual bool RenderPreviousState => false;

        
        public void SetupState(GameEngine Engine)
        {
            //this means the state has already been set up
            if (Content != null) 
                return;

            this.Engine = Engine;

            // Create a new content manager to load content used just by this state.
            Content = new ContentManager(Engine.Services, ContentRoot);
            LoadContent();

            InitGameState(Engine);
        }

        protected abstract void LoadContent();

        protected virtual void InitGameState(GameEngine gameEngine) { }
        protected virtual void DeInitGameState(GameEngine gameEngine) { }

        public void Dispose()
        {
            if (Content != null)
            {
                Content.Unload();
                Content = null;

                DeInitGameState(Engine);
            }
        }
    }
}
