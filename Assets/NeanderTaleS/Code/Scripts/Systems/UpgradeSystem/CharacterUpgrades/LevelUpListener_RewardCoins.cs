using System;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    public class LevelUpListener_RewardCoins: IInitializable, IDisposable
    {
        private ExperienceStorage _storage;
        private const int REWARD_COINS = 3;

        public LevelUpListener_RewardCoins(ExperienceStorage storage)
        {
            _storage = storage;
        }

        void IInitializable.Initialize()
        {
            _storage.OnLevelUp += RewardCoins;
        }

        private void RewardCoins()
        {
            _storage.Coins.Value += REWARD_COINS;
        }

        void IDisposable.Dispose()
        {
            _storage.OnLevelUp += RewardCoins;
        }
    }
}