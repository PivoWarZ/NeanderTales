using System;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgradeSystemSystem: ICharacterUpgradeSystem
    {
        public event Action OnLevelUpEvent;

        private readonly CharacterUpgrade _upgrade;
        private IDisposable _disposable;

        public CharacterUpgradeSystemSystem(CharacterUpgrade upgrade)
        {
            _upgrade = upgrade;
        }

        int ICharacterUpgradeSystem.GetRequiredExperienceToNextLevel()
        {
            return _upgrade.NextPrice;
        }

        void ICharacterUpgradeSystem.CanLevelUp()
        {
            _upgrade.LevelUp();
            OnLevelUpEvent?.Invoke();
        }
    }
}