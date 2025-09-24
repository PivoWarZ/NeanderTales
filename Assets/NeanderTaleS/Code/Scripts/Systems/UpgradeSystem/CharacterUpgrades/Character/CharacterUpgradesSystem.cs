using System;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgradesSystem: IInitializable, IDisposable
    {
        private IExperienceStorage _experience;
        private CharacterUpgrade _upgrade;
        private ICharacterUpgrade _character;
        private IDisposable _disposable;

        public CharacterUpgradesSystem(IExperienceStorage experience, CharacterUpgrade upgrade, ICharacterUpgrade character)
        {
            _experience = experience;
            _upgrade = upgrade;
            _character = character;
            _upgrade.Construct(_character);
        }


        void IInitializable.Initialize()
        {
            _experience.OnLevelUp += CanLevelUp;
            
            var nextLevelExperience = _upgrade.NextPrice;
            _experience.SetRequiredExperience(nextLevelExperience);
        }

        private void CanLevelUp()
        {
            _upgrade.LevelUp();
            
            var nextLevelExperience = _upgrade.NextPrice;
            _experience.SetRequiredExperience(nextLevelExperience);
        }

        public void Dispose()
        {
            _experience.OnLevelUp -= CanLevelUp;
        }
    }
}