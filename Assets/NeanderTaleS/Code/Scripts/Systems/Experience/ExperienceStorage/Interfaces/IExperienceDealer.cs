using System;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Interfaces
{
    public interface IExperienceDealer
    {
        event Action<float> OnDealExperience;
    }
}