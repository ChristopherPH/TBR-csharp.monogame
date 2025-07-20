using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheBlackRoom.MonoGame.GameFramework
{
    public partial class GameEngine
    {
        protected abstract GameState InitialGameState { get; }

        private Stack<GameState> gameStates = new Stack<GameState>();
        private int gameRenderStates = 0;
        private bool _completeCurrentState;
        private GameState _stateToPush = null;

        private void InitGameStates()
        {
            var stateInstance = InitialGameState;

            if (stateInstance == null)
                throw new InvalidOperationException("Invalid initial state");

            stateInstance.Initialize(this);

            gameStates.Push(stateInstance);
            gameRenderStates = 1;

            stateInstance.OnStateStarted(false);
        }

        /// <summary>
        /// Completes the current state (Must be called from Update() method)
        /// </summary>
        public void CompleteCurrentState()
        {
            _completeCurrentState = true;
            _stateToPush = null;

            //if not dispose, then add to hashset to be cleaned up later
        }

        /// <summary>
        /// Adds a new state (Must be called from Update() method)
        /// </summary>
        public void ChangeToState(GameState State)
        {
            if (State == null)
                throw new InvalidOperationException("Invalid state");

            _completeCurrentState = true;
            _stateToPush = State;
        }

        /// <summary>
        /// Completes the current state and adds a new state (Must be called from Update() method)
        /// </summary>
        public void AddState(GameState State)
        {
            if (State == null)
                throw new InvalidOperationException("Invalid state");

            _completeCurrentState = false;
            _stateToPush = State;
        }

        protected int GameStateCount => gameStates.Count;

        /// <summary>
        /// Updates the current game state
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns>false if no game state to update, true if game state updated</returns>
        private bool UpdateGameStates(GameTime gameTime)
        {
            if (gameStates.Count == 0)
            {
                gameRenderStates = 0;
                return false;
            }

            _completeCurrentState = false;
            _stateToPush = null;

            var CurrentState = gameStates.Peek();
            var stateChanged = false;

            CurrentState.Update(gameTime);


            if (_completeCurrentState)
            {
                //complete the state and remove it
                CurrentState.OnStateStopped(false);
                gameStates.Pop();
                CurrentState.Dispose();

                //go back to prev state if not moving to next state
                if ((_stateToPush == null) && (gameStates.Count > 0))
                    gameStates.Peek().OnStateStarted(true);

                stateChanged = true;
            }
            else if (_stateToPush != null)
            {
                //state is still running and we are moving to next state, pause current state
                CurrentState.OnStateStopped(true);
            }

            //move to next state
            if (_stateToPush != null)
            {
                gameStates.Push(_stateToPush);
                _stateToPush.Initialize(this);
                _stateToPush.OnStateStarted(false);

                stateChanged = true;
            }

            if (stateChanged)
            {
                //our states have changed, determine how many states to render
                gameRenderStates = 0;

                foreach (var state in gameStates)
                {
                    gameRenderStates++;
                    if (!state.RenderPreviousState)
                        break;
                }
            }

            return true;
        }

        private void DrawGameStates(GameTime gameTime)
        {
            if (gameStates.Count == 0)
                return;

            //draw states back to front
            foreach (var state in gameStates.Take(gameRenderStates).Reverse())
            {
                state.Draw(gameTime, spriteBatch, GameRectangle);
            }
        }
    }
}
