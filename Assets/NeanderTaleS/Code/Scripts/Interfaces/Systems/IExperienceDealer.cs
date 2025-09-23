using System;

namespace NeanderTaleS.Code.Scripts.Interfaces.Systems
{
    public interface IExperienceDealer
    {
        event Action<float> OnDealExperience;
    }
}