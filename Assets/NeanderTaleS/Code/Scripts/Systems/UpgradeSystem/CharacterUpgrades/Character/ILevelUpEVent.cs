using System;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public interface ILevelUpEVent
    {
        public event Action OnLevelUpEvent;
    }
}