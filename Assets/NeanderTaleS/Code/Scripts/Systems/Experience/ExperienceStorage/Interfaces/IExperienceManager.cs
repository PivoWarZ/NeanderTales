using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Interfaces
{
    public interface IExperienceManager
    {
        bool TryAddExperienceDealer(GameObject enemy);
    }
}