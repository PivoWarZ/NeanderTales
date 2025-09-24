using NeanderTaleS.Code.Scripts.Core.Services;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    public interface ICharacterStatUpgrade
    {
        void Construct(PlayerService service);
    }
}