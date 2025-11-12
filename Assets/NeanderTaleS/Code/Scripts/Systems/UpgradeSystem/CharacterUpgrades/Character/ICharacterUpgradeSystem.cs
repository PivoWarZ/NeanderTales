namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public interface ICharacterUpgradeSystem: ILevelUpEvent
    {
        void CanLevelUp();
        int GetRequiredExperienceToNextLevel();
    }
}