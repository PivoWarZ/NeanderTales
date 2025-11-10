using System;
using System.Collections.Generic;

namespace NeanderTaleS.Code.Scripts.Systems.EventBus
{
    public class EventBus: IEventBus

    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
            {
                _handlers[type] = new List<Delegate>();
            }

            _handlers[type].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);

            if (_handlers.ContainsKey(type))
            {
                _handlers[type].Remove(handler);
            }
        }

        public void RiseEvent<T>(T @event)
        {
            var type = @event.GetType();

            if (_handlers.ContainsKey(type))
            {
                var riseEvent = _handlers[type];

                for (var index = 0; index < riseEvent.Count; index++)
                {
                    var handler = riseEvent[index];
                    var action = handler as Action<T>;
                    action?.Invoke(@event);
                }
            }
        }
    }
}