using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.EnemiesComponents
{
    public class EnemyTargetComponent: MonoBehaviour
    {
        public event Action<GameObject> OnTargetChanged;
        
        [SerializeField] private GameObject _target;

        public GameObject Target => _target;

        [Button]
        private void OnHunt()
        {
            OnTargetChanged?.Invoke(_target);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            OnTargetChanged?.Invoke(_target);
        }
    }
}