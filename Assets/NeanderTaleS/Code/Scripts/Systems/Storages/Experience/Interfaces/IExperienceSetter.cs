namespace NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Interfaces
{
    public interface IExperienceSetter
    {
        void SetExperience(float exp, float requiredExperience);
        void SetRequiredExperience(float exp);
        void AddExperience(float exp);
        bool IsLevelUp();
    }
}