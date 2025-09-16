using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.AnimationInterfaces;
using NeanderTaleS.Code.Scripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class TakeDamageAnimationController: MonoBehaviour, IAnimationController
    {
        private ITakeDamageble _damageble;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private MechanicsBreaker _mechanicsBreaker;
        private float _startHitPoints;
        private float _lowDamage;
        private float _mediumDamage;
        private const float LOW_DAMAGE_THREASHOLD = 0.2f;
        private const float MEDIUM_DAMAGE_THREASHOLD = 0.5f;
        private bool _isStrongDamage = false;
        
        public void Init(LocalProvider localProvider)
        {
            _damageble = localProvider.GetInterface<ITakeDamageble>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();
            _mechanicsBreaker = localProvider.MechanicsBreaker;

            _damageble.OnTakeDamageAction += TakeDamage;
            _event.OnReceiveEvent += ReceiveEvent;
            
            _startHitPoints = _damageble.HitPoints.CurrentValue;
            _lowDamage = _startHitPoints * LOW_DAMAGE_THREASHOLD;
            _mediumDamage = _startHitPoints * MEDIUM_DAMAGE_THREASHOLD;
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "StrongDamageAnimationComplete")
            {
                _isStrongDamage = false;
                _mechanicsBreaker.EnabledCoreMechanics();
            }
        }

        private void TakeDamage(float damage)
        {
            if (_isStrongDamage)
            {
                return;
            }

            if (damage < _lowDamage)
            {
                _animator.SetTrigger("LowDamage");
                
                return;
            }

            if (damage < _mediumDamage)
            {
                _animator.SetTrigger("MediumDamage");
                return;
            }
            
            _animator.SetTrigger("StrongDamage");
            _mechanicsBreaker.BanCoreMechanics();
            _isStrongDamage = true;
        }

        private void OnDestroy()
        {
            _damageble.OnTakeDamageAction -= TakeDamage;
            _event.OnReceiveEvent -= ReceiveEvent;
        }
    }
}