using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.EnemiesComponents
{
    public class EnemyTargetController: MonoBehaviour
    {
        [SerializeField] EnemyTargetComponent _targetComponent;
        private List<ITargetInitComponent> _targetInitComponents = new ();

        private void Awake()
        {
            _targetComponent.OnTargetChanged += SetTarget;
            _targetInitComponents = gameObject.GetComponents<ITargetInitComponent>().ToList();
        }

        private void SetTarget(GameObject target)
        {
            if (_targetInitComponents.Count == 0)
            {
                return;
            }

            for (var i = 0; i < _targetInitComponents.Count; i++)
            {
                var targetInitComponent = _targetInitComponents[i];
                targetInitComponent.SetTarget(target);
            }
        }

        private void OnDestroy()
        {
            _targetComponent.OnTargetChanged -= SetTarget;
        }
    }
}