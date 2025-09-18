using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.CoreScripts.Components;
using NeanderTaleS.Code.CoreScripts.EnemiesComponents;
using NeanderTaleS.Code.CoreScripts.PlayerComponents.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.EnemySkills
{
    public class LeapRaptorSkill: MonoBehaviour
    {
        public event Action OnLeapAttack;
        
        [SerializeField] private DistanceToTargetComponent _distanceComponent;
        [SerializeField] private JumpComponent _jumpComponent;
        [SerializeField] private ConditionInstaller _conditionInstaller;
        [SerializeField] private float _activateDistance;
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _recoveringAfterJump = 30;
        [SerializeField] private bool _isLeapReady;
        private bool _isLeaping;
        private CancellationTokenSource _cancell = new ();
        private IDisposable _dispose;
        

        private void Awake()
        {
            SubscribeActivating();
            _jumpComponent.OnJumpEvent += JumpEvent;
            _jumpComponent.OnJumpAction += Leaping;
            
            _conditionInstaller.AddCondition<IMovable>(LeapingOver);
            _conditionInstaller.AddCondition<IRotatable>(LeapingOver);
            _conditionInstaller.AddCondition<IAttackable>(LeapingOver);
        }

        private bool LeapingOver()
        {
            return !_isLeaping;
        }

        private void Leaping()
        {
            _isLeaping = true;
        }

        private void JumpEvent()
        {
            SubscribeActivating();
            Recovering(_cancell).Forget();
            _isLeaping = false;
        }

        private async UniTaskVoid Recovering(CancellationTokenSource cancell)
        {
            await UniTask.Delay(TimeSpan.FromMilliseconds(_recoveringAfterJump));
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