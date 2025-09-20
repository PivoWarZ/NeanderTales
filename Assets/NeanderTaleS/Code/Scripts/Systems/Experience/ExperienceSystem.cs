using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;

namespace NeanderTaleS.Code.Scripts.Systems.Experience
{
    public class ExperienceSystem: IDisposable
    {
        private ExperienceComponent _component;
        private IExperienceStorage _storage;

        public ExperienceSystem(ExperienceComponent component, IExperienceStorage storage)
        {
            _component = component;
            _storage = storage;
            
            _component.OnDealExperience += AddExperience;
        }

        private void AddExperience(float value)
        {
            _storage.AddExperience(value);
        }

        void IDisposable.Dispose()
        {
            _component.OnDealExperience -= AddExperience;
        }
    }
}