using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.EventBus.Events
{
    public class SpawnEnemyEvent
    {
        public SpawnEnemyEvent(GameObject enemy)
        {
            Enemy = enemy;
        }

        public GameObject Enemy { get; }
    }
}