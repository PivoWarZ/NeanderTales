using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Stats;

namespace NeanderTaleS.Code.Scripts.UI.Bus
{
    public sealed class InstantiatePlayerEventObserver_CreateModelViewPresenter_UI: IDisposable
    {
        private IEventBus _eventBus;
        private PlayerStatsInstaller _playerStatsInstaller;

        public InstantiatePlayerEventObserver_CreateModelViewPresenter_UI(IEventBus eventBus, PlayerStatsInstaller playerStatsInstaller)
        {
            _eventBus = eventBus;
            _playerStatsInstaller = playerStatsInstaller;
            
            _eventBus.Subscribe<InstantiatePlayerEvent>(ConstructModelViewPresenter);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(ConstructModelViewPresenter);
        }

        private void ConstructModelViewPresenter(InstantiatePlayerEvent @event)
        {
            _playerStatsInstaller.ConstructModelViewPresenter(@event.Player);
        }
    }
}