using System;
using NeanderTaleS.Code.Scripts.Core.Components;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface ITakeDamageEvents
    {
        event TakeDamageRequestHandler OnTakeDamageRequest;
        event Action<float, IHitPointsComponent> OnTakeDamageAction;
        event Action OnTakeDamageEvent;
    }
}