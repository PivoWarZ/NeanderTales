using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events
{
    public class EnemySpawnedEvent: IEventBusEvent
    {

        public EnemySpawnedEvent(GameObject enemy, string calling)
        {
            Enemy = enemy;
            Calling = calling;
        }
        
        public GameObject Enemy { get; }
        
        public string Calling { get; }
    }
}