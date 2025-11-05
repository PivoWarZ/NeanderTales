using System;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Systems.InputSystems;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgradesSystem: IInitializable, IDisposable, IInitializedAsPlayer
    {
        private IExperienceStorage _experience;
        private CharacterUpgrade _upgrade;
        private IDisposable _disposable;

        public CharacterUpgradesSystem(IExperienceStorage experience, CharacterUpgrade upgrade)
        {
            _experience = experience;
            _upgrade = upgrade;
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

        public void Initialize(GameObject player)
        {
            IUpgradePlayer upgradePlayer = player.GetComponent<IUpgradePlayer>();

            if (upgradePlayer == null)
            {
                throw new Exception($"{GetType()} Player not found");
            }

            _upgrade.Construct(upgradePlayer);
        }
    }
}