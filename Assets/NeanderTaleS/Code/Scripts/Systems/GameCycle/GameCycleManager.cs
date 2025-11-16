using System.Collections.Generic;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public sealed class GameCycleManager
    {
        private readonly List<IStartGameListener> _startGameListeners = new ();
        private readonly List<IFinishGameListener> _finishGameListeners = new ();
        private readonly List<IPauseGameListener> _pauseGameListeners = new ();
        private readonly List<IResumeGameListener> _resumeGameListeners = new ();

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

        public void StartGame()
        {
            _startGameListeners.ForEach(listener => listener.OnStartGame());
            Debug.Log($"<color=green>{GetType().Name}: Start game... </color>");
        }

        public void FinishGame()
        {
            _finishGameListeners.ForEach(listener => listener.OnFinishGame());
            Debug.Log($"<color=red>{GetType().Name}: Finish game...</color>");
        }

        public void PauseGame()
        {
            _pauseGameListeners.ForEach(listener => listener.OnPauseGame());
            Debug.Log($"<color=yellow>{GetType().Name}: Pause game...</color>");
        }

        public void ResumeGame()
        {
            _resumeGameListeners.ForEach(listener => listener.OnResumeGame());
            Debug.Log($"<color=green>{GetType().Name}: Resume game...</color>");
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
    }
}