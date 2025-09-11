using System;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemySkills
{
    public class LeapSkill: MonoBehaviour
    {
        public bool IsLeapAttack { get; private set; }
        
        [SerializeField] private DistanceToTargetComponent _distanceComponent;
        [SerializeField] private JumpComponent _jumpComponent;
        [SerializeField] private float _activateDistance;
        [SerializeField] private float _jumpDistance;
        [ShowInInspector] private bool _isLeapReady;
        private IDisposable _dispose;
        

        private void Awake()
        {
            SubscribeActivating();
        }

        private void Activate(float distance)
        {
            _isLeapReady = true;
            Unsubscribe();
            SubscribeJumping();
        }

        private void Jump(float _)
        {
            _isLeapReady = false;
            _jumpComponent.Jump();
            Unsubscribe();
            SubscribeActivating();
        }

        private void SubscribeActivating()
        {
            _dispose = _distanceComponent.TargetDistance.Where(onNext => onNext >= _activateDistance).Subscribe(Activate);
        }

        private void SubscribeJumping()
        {
            _dispose = _distanceComponent.TargetDistance.Where(onNext => onNext <= _jumpDistance).Subscribe(Jump);
        }

        private void Unsubscribe()
        {
            _dispose.Dispose();
        }

        private void OnDestroy()
        {
            _dispose?.Dispose();
        }
    }
}