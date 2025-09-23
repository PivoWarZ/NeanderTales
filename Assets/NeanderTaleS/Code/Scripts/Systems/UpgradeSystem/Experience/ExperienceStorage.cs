using System;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using R3;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience
{
    public class ExperienceStorage: IExperienceStorage, ICoinsStorage
    {
        public event Action OnLevelUp;
        
        private ReactiveProperty<float> _requiredExperience = new ();
        private ReactiveProperty<float> _currentExperience = new();
        public ReactiveProperty<int> Coins { get; set; }

        public ReadOnlyReactiveProperty<float> RequiredExperience => _requiredExperience;
        public ReadOnlyReactiveProperty<float> CurrentExperience => _currentExperience;

        public ExperienceStorage()
        {
            Debug.Log($"ExperienceStorage {_currentExperience.Value} / {_requiredExperience.Value}");
        }

        void IExperienceStorage.AddExperience(float exp)
        {
            _currentExperience.Value += exp;
            
            LevelUp();
            
            Debug.Log($"{CurrentExperience.CurrentValue} / {RequiredExperience.CurrentValue}");
        }

        void IExperienceStorage.SetRequiredExperience(float exp)
        {
            _requiredExperience.Value = exp;
        }

        private void LevelUp()
        {
            Debug.Log($"ExperienceStorage {_currentExperience.Value} / {_requiredExperience.Value}");
            if (_currentExperience.Value >= _requiredExperience.Value)
            {
                OnLevelUp?.Invoke();
                LevelUp();
            }
        }
    }
}