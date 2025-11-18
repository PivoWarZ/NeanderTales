using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events
{
    public sealed class SpawnEnemyEvent: IEventBusEvent
    {
        public SpawnEnemyEvent(GameObject enemy, string calling)
        {
            Enemy = enemy;
            Calling = calling;
        }

        public GameObject Enemy { get; }
        public string Calling { get; }
    }
}