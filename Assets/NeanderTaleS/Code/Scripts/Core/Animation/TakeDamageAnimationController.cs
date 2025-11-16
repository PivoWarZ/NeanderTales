using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation
{
    public sealed class TakeDamageAnimationController: MonoBehaviour, IAnimationController
    {
        private IHitPointsComponent _hitPoints;
        private ITakeDamageEvents _damageEvents;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private DebuffsComponent _debuffs;
        private float _startHitPoints;
        private float _lowDamage;
        private float _mediumDamage;
        private const float LOW_DAMAGE_THREASHOLD = 0.2f;
        private const float MEDIUM_DAMAGE_THREASHOLD = 0.5f;
        private bool _isStrongDamage;
        
        public void Init(LocalProvider localProvider)
        {
            _hitPoints = localProvider.GetInterface<IHitPointsComponent>();
            _damageEvents = localProvider.GetInterface<ITakeDamageEvents>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();
            _debuffs = localProvider.GetService<DebuffsComponent>();
            
            var conDitionInstaller = localProvider.GetService<ConditionInstaller>();
            conDitionInstaller.AddCondition<IRotatable>(IsNormalDamage);
            conDitionInstaller.AddCondition<IMovable>(IsNormalDamage);
            conDitionInstaller.AddCondition<IAttackEvents>(IsNormalDamage);
            
            _damageEvents.OnTakeDamageAction += TakeDamage;
            _event.OnReceiveEvent += ReceiveEvent;
            
            _startHitPoints = _hitPoints.CurrentHitPoints.CurrentValue;
            _lowDamage = _startHitPoints * LOW_DAMAGE_THREASHOLD;
            _mediumDamage = _startHitPoints * MEDIUM_DAMAGE_THREASHOLD;
        }

        private bool IsNormalDamage()
        {
            return !_isStrongDamage;
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "StrongDamageAnimationComplete")
            {
                _isStrongDamage = false;
            }
        }

        private void TakeDamage(float damage, IHitPointsComponent _)
        {
            if (!_debuffs.IsStunOver())
            {
                return;
            }

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
            _isStrongDamage = true;
        }

        private void OnDestroy()
        {
            _damageEvents.OnTakeDamageAction -= TakeDamage;
            _event.OnReceiveEvent -= ReceiveEvent;
        }
    }
}