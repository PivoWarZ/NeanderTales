using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class OnCollisionComponent: MonoBehaviour
    {
        public event Action<Collision> OnEnterCollision;
        public event Action<Collider> OnEnterTrigger;
        private void OnCollisionEnter(Collision other)
        {
            OnEnterCollision?.Invoke(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnterTrigger?.Invoke(other);
        }
    }
}