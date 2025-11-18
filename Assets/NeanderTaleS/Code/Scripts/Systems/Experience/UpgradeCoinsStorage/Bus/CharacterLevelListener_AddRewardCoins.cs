using System;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using Zenject;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.UpgradeCoinsStorage.Bus
{
    public sealed class CharacterLevelListener_AddRewardCoins: IInitializable, IDisposable
    {
        private readonly IUpgradeCoinsStorage _storage;
        private readonly CharacterUpgrade _characterUpgrade;
        private const int REWARD = 3;
        private IDisposable _disposable;

        public CharacterLevelListener_AddRewardCoins(IUpgradeCoinsStorage storage, CharacterUpgrade characterUpgrade)
        {
            _storage = storage;
            _characterUpgrade = characterUpgrade;
        }

        void IInitializable.Initialize()
        {
            _disposable = _characterUpgrade.Level.Subscribe(RewardCoins);
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }

        private void RewardCoins(int level)
        {
            var reward = level > 1 ? REWARD : 0;
            _storage.AddCoins(reward);
        }
    }
}