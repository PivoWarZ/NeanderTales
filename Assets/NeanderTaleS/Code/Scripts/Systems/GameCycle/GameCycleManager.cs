using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public sealed class GameCycleManager: ITickable, IFixedTickable, IDisposable
    {
        private readonly List<IStartGameListener> _startGameListeners = new ();
        private readonly List<IFinishGameListener> _finishGameListeners = new ();
        private readonly List<IPauseGameListener> _pauseGameListeners = new ();
        private readonly List<IResumeGameListener> _resumeGameListeners = new ();
        private readonly List<IGameCycleTick> _tickables = new ();
        private readonly List<IGameCycleFixedTick> _fixedTicks = new ();
        private GameState _gameState = GameState.NotStarted;

        public void AddStartGameListener(IStartGameListener listener)
        {
            _startGameListeners.Add(listener);
        }

        public void AddFinishGameListener(IFinishGameListener listener)
        {
            _finishGameListeners.Add(listener);
        }

        public void AddPauseGameListener(IPauseGameListener listener)
        {
            _pauseGameListeners.Add(listener);
        }

        public void AddResumeGameListener(IResumeGameListener listener)
        {
            _resumeGameListeners.Add(listener);
        }
        
        public void AddTickable(IGameCycleTick tickable)
        {
            _tickables.Add(tickable);
        }

        public void AddFixedTicks(IGameCycleFixedTick fixedTick)
        {
            _fixedTicks.Add(fixedTick);
        }
        
        void ITickable.Tick()
        {
            if (!IsPlaying())
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            
            foreach (var gameCycleTick in _tickables)
            {
                gameCycleTick.Tick(deltaTime);
            }
        }

        void IFixedTickable.FixedTick()
        {
            if (!IsPlaying())
            {
                return;
            }
            
            float deltaTime = Time.fixedDeltaTime;
            
            foreach (var gameCycleFixedTick in _fixedTicks)
            {
                gameCycleFixedTick.FixedTick(deltaTime);
            }
        }

        public void StartGame()
        {
            if (_gameState is not GameState.NotStarted)
            {
                return;
            }

            _startGameListeners.ForEach(listener => listener.OnStartGame());
            _gameState = GameState.StartGame;
            Debug.Log($"<color=green>{GetType().Name}: {_gameState} </color>");
        }

        public void FinishGame()
        {
            _finishGameListeners.ForEach(listener => listener.OnFinishGame());
            _gameState = GameState.FinishGame;
            Debug.Log($"<color=red>{GetType().Name}: {_gameState}</color>");
        }

        public void PauseGame()
        {
            if (!IsPlaying())
            {
                return;
            }

            _pauseGameListeners.ForEach(listener => listener.OnPauseGame());
            _gameState = GameState.PauseGame;
            Debug.Log($"<color=yellow>{GetType().Name}: {_gameState}</color>");
        }

        public void ResumeGame()
        {
            if (_gameState is not GameState.PauseGame)
            {
                return;
            }

            _resumeGameListeners.ForEach(listener => listener.OnResumeGame());
            _gameState = GameState.ResumeGame;
            Debug.Log($"<color=green>{GetType().Name}: {_gameState}</color>");
        }

        public void RemoveListener<T>(T listener)
        {
            if (listener is IStartGameListener startGameListener)
            {
                _startGameListeners.Remove(startGameListener);
                return;
            }

            if (listener is IFinishGameListener finishGameListener)
            {
                _finishGameListeners.Remove(finishGameListener);
                return;
            }

            if (listener is IPauseGameListener pauseGameListener)
            {
                _pauseGameListeners.Remove(pauseGameListener);
                return;
            }

            if (listener is IResumeGameListener resumeGameListener)
            {
                _resumeGameListeners.Remove(resumeGameListener);
            }
        }

        private bool IsPlaying()
        {
            return _gameState is GameState.StartGame or GameState.ResumeGame;
        }

        void IDisposable.Dispose()
        {
            _startGameListeners.Clear();
            _finishGameListeners.Clear();
            _pauseGameListeners.Clear();
            _resumeGameListeners.Clear();
            _tickables.Clear();
            _fixedTicks.Clear();
        }
    }
}