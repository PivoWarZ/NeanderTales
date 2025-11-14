using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;

namespace NeanderTaleS.Code.Scripts.UI.Bus
{
    public class InstantiatePlayerEventObserver_CreatetModelViewPresenter_UI: IDisposable
    {
        private IEventBus _eventBus;
        private PlayerStatsInstaller _playerStatsInstaller;

        public InstantiatePlayerEventObserver_CreatetModelViewPresenter_UI(IEventBus eventBus, PlayerStatsInstaller playerStatsInstaller)
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
            _playerStatsInstaller.ConstructModelViewPresenter();
        }
    }
}