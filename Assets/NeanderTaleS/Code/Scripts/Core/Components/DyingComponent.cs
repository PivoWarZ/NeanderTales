using System;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class DyingComponent: MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPoints;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        private IDisposable _disposable;

        private void Awake()
        {
            _disposable = _hitPoints.HitPoints.Where(hp => hp <= 0).Subscribe(Dying);
        }

        private void Dying(float _)
        {
            _collider.isTrigger = true;
            _rigidbody.isKinematic = true;
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}