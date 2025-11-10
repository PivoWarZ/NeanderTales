namespace NeanderTaleS.Code.Scripts.Systems.ExperienceSystem
{
    public interface IExperienceSetter
    {
        void SetExperience(float exp, float requiredExperience);
        void SetRequiredExperience(float exp);
        void AddExperience(float exp);
        bool IsLevelUp();
    }
}