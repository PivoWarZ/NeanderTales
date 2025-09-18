using NeanderTaleS.Code.Scripts.Core.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class Enemy: MonoBehaviour
    {
        [SerializeField] private EntityBootsTrap _bootsTrap;

        private void Awake()
        {
            _bootsTrap.EntityInitialize();
        }
    }
}