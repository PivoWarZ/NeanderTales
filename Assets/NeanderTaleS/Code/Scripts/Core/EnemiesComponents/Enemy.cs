using NeanderTaleS.Code.Configs;
using NeanderTaleS.Code.Scripts.Core.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class Enemy: MonoBehaviour
    {
        [SerializeField] private EntityBootsTrap _bootsTrap;
        [SerializeField] private VelociraptorConfig _config;

        private void Awake()
        {
            _bootsTrap.EntityInitialize();
        }
    }
}