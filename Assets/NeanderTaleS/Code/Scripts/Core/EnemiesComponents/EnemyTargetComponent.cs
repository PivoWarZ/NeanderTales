using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public sealed class EnemyTargetComponent: MonoBehaviour
    {
        public event Action<GameObject> OnTargetChanged;
        
        [SerializeField] private GameObject _target;

        public GameObject Target => _target;

        private void Awake()
        {
            if (_target)
            {
                OnTargetChanged?.Invoke(_target);
            }
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            OnTargetChanged?.Invoke(_target);
        }
    }
}