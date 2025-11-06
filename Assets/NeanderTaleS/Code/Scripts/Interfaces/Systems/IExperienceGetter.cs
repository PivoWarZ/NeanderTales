using R3;

namespace NeanderTaleS.Code.Scripts.Interfaces.Systems
{
    public interface IExperienceGetter
    {
        ReadOnlyReactiveProperty<float> RequiredExperience{ get; }
        ReadOnlyReactiveProperty<float> CurrentExperience { get; }
    }
}