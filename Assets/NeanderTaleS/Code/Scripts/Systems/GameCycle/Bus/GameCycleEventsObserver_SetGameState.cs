using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events.GameCyclesEvents;

namespace NeanderTaleS.Code.Scripts.Systems.GameCycle.Bus
{
    public sealed class GameCycleEventsObserver_SetGameState: IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly GameCycleManager _manager;

        public GameCycleEventsObserver_SetGameState(IEventBus eventBus, GameCycleManager manager)
        {
            _eventBus = eventBus;
            _manager = manager;
            
            _eventBus.Subscribe<PauseGameRequestEvent>(PauseGame);
            _eventBus.Subscribe<ResumeGameRequestEvent>(ResumeGame);
        }

        private void ResumeGame(ResumeGameRequestEvent obj)
        {
            _manager.ResumeGame();
        }

        private void PauseGame(PauseGameRequestEvent obj)
        {
            _manager.PauseGame();
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<PauseGameRequestEvent>(PauseGame);
            _eventBus.Unsubscribe<ResumeGameRequestEvent>(ResumeGame);
        }
    }
}