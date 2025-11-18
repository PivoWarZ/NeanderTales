using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using Debug = UnityEngine.Debug;

namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem
{
    public sealed class EventBus: IEventBus

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
            PrintCallsDebug(@event);
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

        private static void PrintCallsDebug<T>(T @event)
        {
            if (@event is IEventBusEvent busEvent)
            {
                Debug.Log($"<color=yellow> [EVENTBUS]: </color><color=white>{busEvent.Calling}</color><color=yellow> ==> </color><color=white>{@event.GetType().Name}");
            }
            else
            {
                Debug.Log($"<color=red>{@event.GetType()} is NOT EVENT BUS EVENT</color>");
            }
        }
    }
}