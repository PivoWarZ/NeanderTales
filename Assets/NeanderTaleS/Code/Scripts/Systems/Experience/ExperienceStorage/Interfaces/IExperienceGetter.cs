using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Interfaces
{
    public interface IExperienceGetter
    {
        ReadOnlyReactiveProperty<float> RequiredExperience{ get; }
        ReadOnlyReactiveProperty<float> CurrentExperience { get; }
    }
}