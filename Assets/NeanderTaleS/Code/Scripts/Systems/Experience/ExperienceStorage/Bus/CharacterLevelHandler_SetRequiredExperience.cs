using System;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Bus
{
    public sealed class CharacterLevelHandler_SetRequiredExperience: IDisposable
    {
        private readonly CharacterUpgrade _characterUpgrade;
        private readonly IExperienceSetter _experienceSetter;
        private readonly IDisposable _disposable;

        public CharacterLevelHandler_SetRequiredExperience(IExperienceSetter experienceSetter, CharacterUpgrade characterUpgrade)
        {
            _experienceSetter = experienceSetter;
            _characterUpgrade = characterUpgrade;

            _disposable = _characterUpgrade.Level.Subscribe(SetExperienceValues);
        }

        private void SetExperienceValues(int level)
        {
            var requiredExperience = _characterUpgrade.NextPrice;
            _experienceSetter.SetRequiredExperience(requiredExperience);
        }

        void IDisposable.Dispose()
        {
            _disposable?.Dispose();
        }
    }
}