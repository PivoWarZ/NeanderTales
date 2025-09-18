using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class BitePoint: MonoBehaviour
    {
        public event Action<GameObject> OnBite;
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}