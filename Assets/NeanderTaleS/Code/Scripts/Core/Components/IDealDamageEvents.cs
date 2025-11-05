using System;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public interface IDealDamageEvents
    {
        event DealDamageRequestHandler OnDealDamageRequest;
        public event Action<float> OnDealDamageAction;
        public event Action OnDealDamageEvent;
    }
}