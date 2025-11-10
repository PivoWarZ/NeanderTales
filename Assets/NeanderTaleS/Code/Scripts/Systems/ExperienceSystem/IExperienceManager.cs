using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.ExperienceSystem
{
    public interface IExperienceManager
    {
        bool TryAddExperienceDealer(GameObject enemy);
    }
}