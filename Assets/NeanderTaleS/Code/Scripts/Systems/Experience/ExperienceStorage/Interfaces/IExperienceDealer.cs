using System;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Interfaces
{
    public interface IExperienceDealer
    {
        event Action<float> OnDealExperience;
    }
}