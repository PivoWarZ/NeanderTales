using System;
using R3;

namespace NeanderTaleS.Code.Scripts.Components
{
    public interface ITakeDamageble
    {
        event TakeDamageRequestHandler OnTakeDamageRequest;
        event Action<float> OnTakeDamageAction;
        event Action OnTakeDamageEvent;
        float GetStartHitPoints();
        ReadOnlyReactiveProperty<float> HitPoints { get; }
        void TakeDamage(float damage);
    }
}