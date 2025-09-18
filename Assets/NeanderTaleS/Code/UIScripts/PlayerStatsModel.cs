using R3;

namespace NeanderTaleS.Code.UIScripts
{
    public class PlayerStatsModel
    {
        public ReadOnlyReactiveProperty<float> StartHitPointValue;
        public ReadOnlyReactiveProperty<float> MaxStaminaValue;
        public ReadOnlyReactiveProperty<float> RequiredExperience;
        public ReadOnlyReactiveProperty<float> HitPoints;
        public ReadOnlyReactiveProperty<float> Stamina;
        public ReadOnlyReactiveProperty<float> MaxStamina;
        public ReadOnlyReactiveProperty<float> Experience;
        public ReadOnlyReactiveProperty<int> Level;
    }
}