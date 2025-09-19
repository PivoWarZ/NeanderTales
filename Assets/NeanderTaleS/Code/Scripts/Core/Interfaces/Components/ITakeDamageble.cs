using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using R3;

namespace NeanderTaleS.Code.Scripts.Core.Interfaces.Components
{
    public interface ITakeDamageble
    {
        event TakeDamageRequestHandler OnTakeDamageRequest;
        event Action<float> OnTakeDamageAction;
        event Action OnTakeDamageEvent;
        ReadOnlyReactiveProperty<float> HitPoints { get; }
        void TakeDamage(float damage);
        void TakeDamageEvent();
    }
}