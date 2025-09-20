using R3;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IStamina
    {
        public ReadOnlyReactiveProperty<float> MaxStamina { get; }
        public ReadOnlyReactiveProperty<float> Stamina { get; }
    }
}