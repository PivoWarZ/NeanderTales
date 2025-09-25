using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public class GameCycleInstaller: IInitializable
    {
        private readonly List<IGameCycle> _cycles;
        private GameCycleManager _manager;

        public GameCycleInstaller(IGameCycle[] gameCycles, GameCycleManager gameCycleManager)
        {
            _cycles = gameCycles.ToList();
            _manager = gameCycleManager;
        }

        public void Initialize()
        {
            foreach (var gameCycle in _cycles)
            {
                if (gameCycle is IStartGameListener startGameListener)
                {
                    _manager.AddStartGameListener(startGameListener);
                }
                
                if (gameCycle is IFinishGameListener finishGameListener)
                {
                    _manager.AddFinishGameListener(finishGameListener);
                }
                
                if (gameCycle is IPauseGameListener pauseGameListener)
                {
                    _manager.AddPauseGameListener(pauseGameListener);
                }
                
                if (gameCycle is IResumeGameListener resumeGameListener)
                {
                    _manager.AddResumeGameListener(resumeGameListener);
                }
            }
        }
    }
}