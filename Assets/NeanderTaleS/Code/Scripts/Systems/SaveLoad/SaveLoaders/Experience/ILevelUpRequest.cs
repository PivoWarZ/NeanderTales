using System;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience
{
    public interface ILevelUpRequest
    {
        event Action OnLevelUpRequest;
    }
}