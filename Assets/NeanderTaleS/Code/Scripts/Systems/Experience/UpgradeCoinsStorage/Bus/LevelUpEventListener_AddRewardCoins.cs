using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.UpgradeCoinsStorage.Bus
{
    public sealed class LevelUpEventListener_AddRewardCoins: IInitializable, IDisposable
    {
        private IUpgradeCoinsStorage _storage;
        private IEventBus _eventBus;
        private const int REWARD = 3;

        public LevelUpEventListener_AddRewardCoins(IUpgradeCoinsStorage storage, IContext context)
        {
            _storage = storage;
            _eventBus = context.GetService<IEventBus>();
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<LevelUpEvent>(RewardCoins);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Subscribe<LevelUpEvent>(RewardCoins);
        }

        private void RewardCoins(LevelUpEvent obj)
        {
            _storage.AddCoins(REWARD);
        }
    }
}