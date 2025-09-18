using NeanderTaleS.Code.CoreScripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.EnemiesComponents
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