using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.EventBus.Events
{
    public class InstantiatePlayerEvent: IEventBusEvent

    {
    public InstantiatePlayerEvent(GameObject player)
    {
        Player = player;
    }

    public GameObject Player { get; }
    }
}