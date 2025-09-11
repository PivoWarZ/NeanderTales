using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class OnCollisionComponent: MonoBehaviour
    {
        public event Action<Collision> OnEnterCollision;
        private void OnCollisionEnter(Collision other)
        {
            OnEnterCollision?.Invoke(other);
            Debug.Log("OnCollisionEnter");
        }
    }
}