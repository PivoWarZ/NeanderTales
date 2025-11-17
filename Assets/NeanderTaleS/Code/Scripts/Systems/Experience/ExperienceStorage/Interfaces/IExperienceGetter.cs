using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Interfaces
{
    public interface IExperienceGetter
    {
        ReadOnlyReactiveProperty<float> RequiredExperience{ get; }
        ReadOnlyReactiveProperty<float> CurrentExperience { get; }
    }
}