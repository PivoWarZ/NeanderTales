using System;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.Experience
{
    public interface ILevelUpRequest
    {
        event Action OnLevelUpRequest;
    }
}