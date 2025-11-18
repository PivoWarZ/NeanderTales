using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.Bus
{
    public sealed class InstantiatePlayerObserver_ConstructInputSystem: IDisposable
    {
        private IEventBus _eventBus;
        private InputSystemInitializer _initializer;

        public InstantiatePlayerObserver_ConstructInputSystem(IEventBus eventBus, InputSystemInitializer initializer)
        {
            _eventBus = eventBus;
            _initializer = initializer;
            
            _eventBus.Subscribe<InstantiatePlayerEvent>(ConstructInputSystem);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(ConstructInputSystem);
        }
        
        private void ConstructInputSystem(InstantiatePlayerEvent @event)
        {
            _initializer.ConstructInputSystem(@event.Player);
        }
    }
}