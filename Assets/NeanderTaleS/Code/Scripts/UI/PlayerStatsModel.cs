using R3;

namespace NeanderTaleS.Code.Scripts.UI
{
    public class PlayerStatsModel
    {
        public ReadOnlyReactiveProperty<float> MaxHitPoints;
        public ReadOnlyReactiveProperty<float> CurrentHitPoints;
        public ReadOnlyReactiveProperty<float> MaxStamina;
        public ReadOnlyReactiveProperty<float> CurrentStamina;
        public ReadOnlyReactiveProperty<float> RequiredExperience;
        public ReadOnlyReactiveProperty<float> CurrentExperience;
        public ReadOnlyReactiveProperty<int> Level;
    }
}