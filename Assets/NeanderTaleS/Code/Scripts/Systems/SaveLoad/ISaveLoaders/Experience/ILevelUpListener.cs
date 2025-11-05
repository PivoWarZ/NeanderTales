using System;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience
{
    public interface ILevelUpListener
    {
        event Action OnLevelUp;
    }
}