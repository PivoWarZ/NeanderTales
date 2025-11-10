namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public interface ICharacterUpgradeSystem: ILevelUpEVent
    {
        void CanLevelUp();
        int GetRequiredExperienceToNextLevel();
    }
}