using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems
{
    public sealed class InputSystemInitializer: IInitializable, IDisposable
    {
        private readonly IPlayerInput[] _playerInputs;
        private IEventBus _eventBus;
        

        public InputSystemInitializer(IPlayerInput[] playerInputs, IContext context)
        {
            _playerInputs = playerInputs;
            _eventBus = context.GetService<IEventBus>();
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<InstantiatePlayerEvent>(ConstructPlayerInputs);
        }
        
        public void Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(ConstructPlayerInputs);
        }

        private void ConstructPlayerInputs(InstantiatePlayerEvent @event)
        {
            foreach (var playerInput in _playerInputs)
            {
                playerInput.Construct(@event.Player);
            }
        }
    }
}