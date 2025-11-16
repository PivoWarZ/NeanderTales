using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.Experience
{
    public sealed class ExperienceManager: IExperienceManager, ILevelUpRequest, IDisposable
    {
        public event Action OnLevelUpRequest;
        private readonly IExperienceSetter _experienceSetter;
        private List<IExperienceDealer> _dealers = new ();

        public ExperienceManager(IExperienceSetter experienceSetter)
        {
            _experienceSetter = experienceSetter;
        }
        
        public bool TryAddExperienceDealer(GameObject enemy)
        {
            enemy.TryGetComponent<IExperienceDealer>(out IExperienceDealer experienceDealer);
            
            if (experienceDealer != null)
            {
                AddExperienceDealer(experienceDealer);
                return true;
            }
            
            return false;
        }
        
        private void AddExperienceDealer(IExperienceDealer dealer)
        {
            _dealers.Add(dealer);
            dealer.OnDealExperience += AddExperience;
        }
        
        private void AddExperience(float value)
        {
            _experienceSetter.AddExperience(value);

            if (_experienceSetter.IsLevelUp())
            {
                OnLevelUpRequest?.Invoke();
            }
        }

        void IDisposable.Dispose()
        {
            if (_dealers is { Count: > 0 })
            {
                foreach (var experienceDealer in _dealers)
                {
                    experienceDealer.OnDealExperience -= AddExperience;
                }
                
                _dealers.Clear();
                _dealers = null;
            }
        }

        public void AddExpFromCheatButton(float cheatButtonValue)
        {
            AddExperience(cheatButtonValue);
        }
    }
}