using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.EventBus.Events
{
    public class InstantiatePlayerEvent: IEventBusEvent

    {
    public InstantiatePlayerEvent(GameObject player, string calling)
    {
        Player = player;
        Calling = calling;
    }

    public GameObject Player { get; }
    public string Calling { get; }
    }
}