using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Factory
{
    public class CamerasProviderInstaller: IInitializable, IDisposable
    {
        private IEventBus _eventBus;
        private CamerasProvider _camerasProvider;

        public CamerasProviderInstaller(IEventBus eventBus, CamerasProvider camerasProvider)
        {
            _eventBus = eventBus;
            _camerasProvider = camerasProvider;
        }

        void IInitializable.Initialize()
        {
           _eventBus.Subscribe<InstantiatePlayerEvent>(InitializeCamerasProvider);
        }
        
        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(InitializeCamerasProvider);
        }

        private void InitializeCamerasProvider(InstantiatePlayerEvent @event)
        {
            _camerasProvider.Initialize(@event.Player);
        }
    }
}