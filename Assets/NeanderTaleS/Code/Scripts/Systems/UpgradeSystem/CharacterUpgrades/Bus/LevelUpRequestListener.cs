using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character.Bus
{
    public class LevelUpRequestListener: IInitializable, IDisposable
    {
        private IEventBus _eventBus;
        private ICharacterUpgradeSystem _characterUpgradeSystem;

        public LevelUpRequestListener(IEventBus eventBus, ICharacterUpgradeSystem characterUpgradeSystem)
        {
            _eventBus = eventBus;
            _characterUpgradeSystem = characterUpgradeSystem;
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<LevelUpRequest>(LevelUpRequest);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<LevelUpRequest>(LevelUpRequest);
        }

        private void LevelUpRequest(LevelUpRequest _)
        {
            _characterUpgradeSystem.CanLevelUp();
        }
    }
}