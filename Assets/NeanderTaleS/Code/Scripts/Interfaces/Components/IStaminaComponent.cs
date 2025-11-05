using R3;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IStaminaComponent
    {
        public ReadOnlyReactiveProperty<float> MaxStamina { get; }
        public ReadOnlyReactiveProperty<float> Stamina { get; }
        
        void SetStamina(float stamina, float maxStamina);
    }
}