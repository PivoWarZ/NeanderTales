using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Interfaces
{
    public interface IExperienceManager
    {
        bool TryAddExperienceDealer(GameObject enemy);
    }
}