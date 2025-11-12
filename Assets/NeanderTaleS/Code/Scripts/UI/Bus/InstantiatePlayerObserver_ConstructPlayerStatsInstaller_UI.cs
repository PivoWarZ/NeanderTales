using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;

namespace NeanderTaleS.Code.Scripts.UI.Bus
{
    public class InstantiatePlayerObserver_ConstructPlayerStatsInstaller_UI: IDisposable
    {
        private IEventBus _eventBus;
        private PlayerStatsInstaller _playerStatsInstaller;

        public InstantiatePlayerObserver_ConstructPlayerStatsInstaller_UI(IEventBus eventBus, PlayerStatsInstaller playerStatsInstaller)
        {
            _eventBus = eventBus;
            _playerStatsInstaller = playerStatsInstaller;
            
            _eventBus.Subscribe<InstantiatePlayerEvent>(ConstructPlayerStatsInstaller);
        }


        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(ConstructPlayerStatsInstaller);
        }

        private void ConstructPlayerStatsInstaller(InstantiatePlayerEvent @event)
        {
            _playerStatsInstaller.Construct(@event.Player);
        }
    }
}