using System;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.LevelCounter.Bus
{
    public class CharacterLevelHandler_SetLevelUpCounter: IDisposable
    {
        private readonly ILevelUpCounter _levelUpCounter;
        private readonly CharacterUpgrade _characterUpgrade;
        private readonly IDisposable _disposables;

        public CharacterLevelHandler_SetLevelUpCounter(ILevelUpCounter levelUpCounter, CharacterUpgrade characterUpgrade)
        {
            _levelUpCounter = levelUpCounter;
            _characterUpgrade = characterUpgrade;

            _disposables = _characterUpgrade.Level.Subscribe(IncrementLevelUpCounter);
        }

        private void IncrementLevelUpCounter(int level)
        {
            _levelUpCounter.SetLevel(level);
        }

        void IDisposable.Dispose()
        {
            _disposables?.Dispose();
            _levelUpCounter.Dispose();
        }
    }
}