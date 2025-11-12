using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;

namespace NeanderTaleS.Code.Scripts.UI.Bus
{
    public class LevelUpEventObserver_ConstructModelViewPresenter_UI: IDisposable
    {
        private IEventBus _eventBus;
        private PlayerStatsInstaller _playerStatsInstaller;

        public LevelUpEventObserver_ConstructModelViewPresenter_UI(IEventBus eventBus, PlayerStatsInstaller playerStatsInstaller)
        {
            _eventBus = eventBus;
            _playerStatsInstaller = playerStatsInstaller;
            
            _eventBus.Subscribe<LevelUpEvent>(InitializePlayerStatsInstaller);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<LevelUpEvent>(InitializePlayerStatsInstaller);
        }

        private void InitializePlayerStatsInstaller(LevelUpEvent @event)
        {
            _playerStatsInstaller.ConstructModelViewPresenter();
        }
    }
}