using System.Collections.Generic;

namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public class GameCycleManager
    {
        private List<IStartGameListener> _startGameListeners = new ();
        private List<IFinishGameListener> _finishGameListeners = new ();
        private List<IPauseGameListener> _pauseGameListeners = new ();
        private List<IResumeGameListener> _resumeGameListeners = new ();

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
        }

        public void FinishGame()
        {
            _finishGameListeners.ForEach(listener => listener.OnFinishGame());
        }

        public void PauseGame()
        {
            _pauseGameListeners.ForEach(listener => listener.OnPauseGame());
        }

        public void ResumeGame()
        {
            _resumeGameListeners.ForEach(listener => listener.OnResumeGame());
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