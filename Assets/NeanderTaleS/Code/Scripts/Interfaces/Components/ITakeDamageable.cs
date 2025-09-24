using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using R3;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface ITakeDamageable
    {
        event TakeDamageRequestHandler OnTakeDamageRequest;
        event Action<float, ITakeDamageable> OnTakeDamageAction;
        event Action OnTakeDamageEvent;
        ReadOnlyReactiveProperty<float> CurrentHitPoints { get; }
        ReadOnlyReactiveProperty<float> MaxHitPoints { get; }
        void TakeDamage(float damage);
        void TakeDamageEvent();
        void AddedtHitPoints(float currentHitPoints, float maxHitPoints = 0);
    }
}