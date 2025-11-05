using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience
{
    public interface IExperienceStorage: IExperienceGetter, IExperienceSetter, ILevelUpListener
    {
        
    }
}