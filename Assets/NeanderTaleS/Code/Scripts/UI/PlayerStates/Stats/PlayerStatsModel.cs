using R3;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Stats
{
    public sealed class PlayerStatsModel
    {
        public ReadOnlyReactiveProperty<float> MaxHitPoints;
        public ReadOnlyReactiveProperty<float> CurrentHitPoints;
        public ReadOnlyReactiveProperty<float> MaxStamina;
        public ReadOnlyReactiveProperty<float> CurrentStamina;
    }
}