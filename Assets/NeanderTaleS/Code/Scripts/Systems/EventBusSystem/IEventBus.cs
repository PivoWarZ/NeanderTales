using System;

namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem
{
    public interface IEventBus
    {
        public void Subscribe<T> (Action<T> handler);
        public void Unsubscribe<T> (Action<T> handler);
        public void RiseEvent<T> (T @event);
    }
}