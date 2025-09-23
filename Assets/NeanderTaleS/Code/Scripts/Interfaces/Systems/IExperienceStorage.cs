using R3;

namespace NeanderTaleS.Code.Scripts.Interfaces.Systems
{
    public interface IExperienceStorage
    {
        ReadOnlyReactiveProperty<float> RequiredExperience{ get; }
        ReadOnlyReactiveProperty<float> CurrentExperience { get; }

        void AddExperience(float exp);
        
        void SetRequiredExperience(float exp);
    }
}