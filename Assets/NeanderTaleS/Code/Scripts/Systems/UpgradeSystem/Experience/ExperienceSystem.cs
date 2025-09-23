using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience
{
    public class ExperienceSystem: IDisposable
    {
        private IExperienceStorage _storage;
        private List<IExperienceDealer> _dealers = new ();

        public ExperienceSystem(IExperienceStorage storage)
        {
            _storage = storage;
        }

        public void AddExperienceDealer(IExperienceDealer dealer)
        {
            _dealers.Add(dealer);
            dealer.OnDealExperience += AddExperience;
        }

        private void AddExperience(float value)
        {
            _storage.AddExperience(value);
        }

        public void Dispose()
        {
            if (_dealers != null && _dealers.Count > 0)
            {
                foreach (var experienceDealer in _dealers)
                {
                    experienceDealer.OnDealExperience -= AddExperience;
                }
                
                _dealers.Clear();
                _dealers = null;
            }

        }
    }
}