using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class BitePoint: MonoBehaviour
    {
        public event Action<GameObject> OnBite;
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
            Debug.Log(other.gameObject.name);
        }
    }
}