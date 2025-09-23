using System;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using R3;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience
{
    public class ExperienceStorage: IExperienceStorage, ICoinsStorage
    {
        public event Action OnLevelUp;
        
        private ReactiveProperty<float> _requiredExperience = new (10);
        private ReactiveProperty<float> _currentExperience = new();
        public ReactiveProperty<int> Coins { get; set; }

        public ReadOnlyReactiveProperty<float> RequiredExperience => _requiredExperience;
        public ReadOnlyReactiveProperty<float> CurrentExperience => _currentExperience;
        
        void IExperienceStorage.AddExperience(float exp)
        {
            _currentExperience.Value += exp;

            if (_currentExperience.Value >= _requiredExperience.Value)
            {
                OnLevelUp?.Invoke();
            }
        }

        void IExperienceStorage.SetRequiredExperience(float exp)
        {
            _requiredExperience.Value = exp;
        }
    }

    public interface ICoinsStorage
    {
        ReactiveProperty<int> Coins { get; set; }
    }
}