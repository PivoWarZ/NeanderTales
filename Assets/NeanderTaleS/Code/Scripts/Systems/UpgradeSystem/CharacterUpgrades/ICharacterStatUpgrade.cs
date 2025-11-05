using NeanderTaleS.Code.Scripts.Core.Services;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    public interface ICharacterStatUpgrade
    {
        void Construct(GameObject player);
    }
}