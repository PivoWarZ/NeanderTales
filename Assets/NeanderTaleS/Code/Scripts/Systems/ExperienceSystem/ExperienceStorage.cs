using System;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.ExperienceSystem
{
    public class ExperienceStorage: IExperienceStorage, IDisposable
    {
        private ReactiveProperty<float> _currentExperience = new();
        private ReactiveProperty<float> _requiredExperience = new ();
        
        public ReadOnlyReactiveProperty<float> RequiredExperience => _requiredExperience;
        public ReadOnlyReactiveProperty<float> CurrentExperience => _currentExperience;
        
        void IExperienceSetter.AddExperience(float exp)
        {
            _currentExperience.Value += exp;
        }

        void IExperienceSetter.SetExperience(float exp, float requiredExperience)
        {
            _currentExperience.Value = exp;
            _requiredExperience.Value = requiredExperience;
        }

        void IExperienceSetter.SetRequiredExperience(float exp)
        {
            _requiredExperience.Value = exp;
        }

        bool IExperienceSetter.IsLevelUp()
        {
            return CurrentExperience.CurrentValue >= RequiredExperience.CurrentValue;
        }

        public void Dispose()
        {
            RequiredExperience.Dispose();
            CurrentExperience.Dispose();
            _requiredExperience.Dispose();
            _currentExperience.Dispose();
            _requiredExperience = null;
            _currentExperience = null;
        }
    }
}