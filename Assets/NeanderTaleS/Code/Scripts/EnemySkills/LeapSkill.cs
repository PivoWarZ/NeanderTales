using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemySkills
{
    public class LeapSkill: MonoBehaviour
    {
        public event Action OnLeapAttack;
        public bool IsLeapAttack { get; private set; }
        
        [SerializeField] private DistanceToTargetComponent _distanceComponent;
        [SerializeField] private JumpComponent _jumpComponent;
        [SerializeField] private float _activateDistance;
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _recoveringAfterJump = 30;
        [SerializeField] private bool _isLeapReady;
        private CancellationTokenSource _cancell = new ();
        private IDisposable _dispose;
        

        private void Awake()
        {
            SubscribeActivating();
            _jumpComponent.OnJumpEvent += JumpEvent;
        }

        private void JumpEvent()
        {
            SubscribeActivating();
            Recovering(_cancell).Forget();
        }

        private async UniTaskVoid Recovering(CancellationTokenSource cancell)
        {
            await UniTask.Delay(TimeSpan.FromMilliseconds(_recoveringAfterJump));
            IsLeapAttack = false;
        }

        private void Activate(float distance)
        {
            _isLeapReady = true;
            Unsubscribe();
            SubscribeJumping();
        }

        private void Jump(float _)
        {
            if (!_isLeapReady)
            {
                return;
            }
            
            OnLeapAttack?.Invoke();
            
            Unsubscribe();
            _jumpComponent.Jump();
            
            IsLeapAttack = true;
            _isLeapReady = false;
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