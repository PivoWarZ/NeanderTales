using System;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
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
        }
        
        
        public void Initialize()
        {
            Init();
        }

        public void Init()
        {
            _upgrade.Construct(_character);
            _experience.OnLevelUp += CanLevelUp;
        }

        private void CanLevelUp()
        {
            var nextLevelExperience = _upgrade.NextPrice;
            _experience.SetRequiredExperience(nextLevelExperience);
            
            _upgrade.LevelUp();
        }

        public void Dispose()
        {
            _experience.OnLevelUp -= CanLevelUp;
        }
    }
}