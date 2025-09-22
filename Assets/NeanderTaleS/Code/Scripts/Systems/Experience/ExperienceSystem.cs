using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;

namespace NeanderTaleS.Code.Scripts.Systems.Experience
{
    public class ExperienceSystem: IDisposable
    {
        private ExperienceRewardComponent _rewardComponent;
        private IExperienceStorage _storage;

        public ExperienceSystem(ExperienceRewardComponent rewardComponent, IExperienceStorage storage)
        {
            _rewardComponent = rewardComponent;
            _storage = storage;
            
            _rewardComponent.OnDealExperience += AddExperience;
        }

        private void AddExperience(float value)
        {
            _storage.AddExperience(value);
        }

        void IDisposable.Dispose()
        {
            _rewardComponent.OnDealExperience -= AddExperience;
        }
    }
}