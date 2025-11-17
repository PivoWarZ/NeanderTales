namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Interfaces
{
    public interface IExperienceSetter
    {
        void SetExperience(float exp, float requiredExperience);
        void SetRequiredExperience(float exp);
        void AddExperience(float exp);
        bool IsLevelUp();
    }
}