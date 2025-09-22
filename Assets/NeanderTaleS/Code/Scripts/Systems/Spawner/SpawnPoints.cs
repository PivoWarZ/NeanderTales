using System.Collections.Generic;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Spawner
{
    public class SpawnPoints: MonoBehaviour
    {
        [SerializeField] private List<Transform> _enemySpawnPoints;
        [SerializeField] private Transform _playerStartPosition;

        public Transform PlayerStartPosition => _playerStartPosition;

        public List<Transform> GetEnemySpawnPoints()
        {
            return _enemySpawnPoints;
        }
    }
}