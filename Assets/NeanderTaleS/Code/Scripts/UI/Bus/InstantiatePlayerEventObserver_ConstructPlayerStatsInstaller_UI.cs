namespace NeanderTaleS.Code.Scripts.UI.Bus
{
    /*public class InstantiatePlayerEventObserver_ConstructPlayerStatsInstaller_UI: IDisposable
    {
        private IEventBus _eventBus;
        private PlayerStatsInstaller _playerStatsInstaller;

        public InstantiatePlayerEventObserver_ConstructPlayerStatsInstaller_UI(IEventBus eventBus, PlayerStatsInstaller playerStatsInstaller)
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
    }*/
}