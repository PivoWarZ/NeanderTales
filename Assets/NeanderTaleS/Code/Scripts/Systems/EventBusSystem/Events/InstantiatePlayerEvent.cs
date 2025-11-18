using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events
{
    public sealed class InstantiatePlayerEvent: IEventBusEvent

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