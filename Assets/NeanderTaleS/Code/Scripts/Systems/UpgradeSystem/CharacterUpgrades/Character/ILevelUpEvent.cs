using System;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public interface ILevelUpEvent
    {
        public event Action OnLevelUpEvent;
    }
}