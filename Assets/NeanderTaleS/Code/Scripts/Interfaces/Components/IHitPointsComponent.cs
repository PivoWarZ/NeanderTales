using R3;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IHitPointsComponent
    {
        ReadOnlyReactiveProperty<float> CurrentHitPoints { get; }
        ReadOnlyReactiveProperty<float> MaxHitPoints { get; }
        void AddHitPoints(float currentHitPoints, float maxHitPoints = 0);
        void SetHitPoints(float currentHitPoints, float maxHitPoints);
    }
}