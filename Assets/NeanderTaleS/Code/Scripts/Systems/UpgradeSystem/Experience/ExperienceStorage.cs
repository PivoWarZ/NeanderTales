using System;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using R3;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience
{
    public class ExperienceStorage: IInitializable, IExperienceStorage, ICoinsStorage, IDisposable
    {
        public event Action OnLevelUp;
        
        private ReactiveProperty<float> _requiredExperience = new ();
        private ReactiveProperty<float> _currentExperience = new();
        public ReactiveProperty<int> Coins { get; set; }

        public ReadOnlyReactiveProperty<float> RequiredExperience => _requiredExperience;
        public ReadOnlyReactiveProperty<float> CurrentExperience => _currentExperience;
        
        void IInitializable.Initialize()
        {
            Coins = new ReactiveProperty<int>(0);
        }
        
        void IExperienceStorage.AddExperience(float exp)
        {
            _currentExperience.Value += exp;
            
            TryLevelUp();
        }

        void IExperienceStorage.SetRequiredExperience(float exp)
        {
            _requiredExperience.Value = exp;
        }

        private void TryLevelUp()
        {
            if (_currentExperience.Value >= _requiredExperience.Value)
            {
                OnLevelUp?.Invoke();
            }
        }

        public void Dispose()
        {
            RequiredExperience.Dispose();
            CurrentExperience.Dispose();
            _requiredExperience.Dispose();
            _currentExperience.Dispose();
            Coins.Dispose();
            Coins = null;
            _requiredExperience = null;
            _currentExperience = null;
        }
    }
}