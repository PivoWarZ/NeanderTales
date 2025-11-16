using System;


namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public sealed class CharacterUpgradeSystem: ICharacterUpgradeSystem
    {
        public event Action OnLevelUpEvent;

        private readonly CharacterUpgrade _characterUpgrade;
        private IDisposable _disposable;

        public CharacterUpgradeSystem(CharacterUpgrade upgrade)
        {
            _characterUpgrade = upgrade;
        }

        int ICharacterUpgradeSystem.GetRequiredExperienceToNextLevel()
        {
            return _characterUpgrade.NextPrice;
        }

        void ICharacterUpgradeSystem.CanLevelUp()
        {
            bool isLevelUp = _characterUpgrade.TryLevelUp();

            if (isLevelUp)
            {
                OnLevelUpEvent?.Invoke();
            }
        }
    }
}