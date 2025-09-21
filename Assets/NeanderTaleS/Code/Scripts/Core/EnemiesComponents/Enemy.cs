using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Configs;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.EnemySkills;
using NeanderTaleS.Code.Scripts.Core.WeaponComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.WeaponInterfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class Enemy: MonoBehaviour
    {
        [SerializeField] private EntityBootsTrap _bootsTrap;
        [SerializeField] private LocalProvider _provider;
        [SerializeField] private VelociraptorConfig _config;
        private List<IStartValueSetter> _startValueSetters;

        private void Awake()
        {
            _bootsTrap.EntityInitialize();
            InitializeLeapSkill(_provider);
            
            _startValueSetters = _provider.GetInterfaces<IStartValueSetter>();
            SetScale();
            SetHitPoints();
            SetDamage();
            SetSpeed();
            SetAttackDistance();
            SetActivatingDistance();
            SetStunChance();
            SetPushPower();
            
        }
        
        private void InitializeLeapSkill(LocalProvider localProvider)
        {
            var leapSkill = localProvider.GetService<LeapRaptorSkill>();
            leapSkill.Init();
        }

        private void SetHitPoints()
        {
           var health = _startValueSetters.FirstOrDefault(IStartValueSetter => IStartValueSetter is ITakeDamageble hitPoints);
           health.SetStartValue(_config.HitPoints, _config.HitPoints);
        }

        private void SetDamage()
        {
            var damage = _startValueSetters.FirstOrDefault(IStartValueSetter => IStartValueSetter is IWeapon weapon);
            damage.SetStartValue(_config.Damage);

        }

        private void SetSpeed()
        {
            var speed = _startValueSetters.FirstOrDefault(IStartValueSetter => IStartValueSetter is IMovable move);
            speed.SetStartValue(_config.Speed);
        }

        private float SetAttackDistance()
        {
            var distance = _startValueSetters.FirstOrDefault(IStartValueSetter => IStartValueSetter is IAttackDistance attackDistance);
            var weapon = _provider.GetService<Weapon>();
            var distanceToWeapon = (weapon.transform.position - transform.position).magnitude;
            var distanceToTatget = Mathf.Min(distanceToWeapon + 1.3f * _config.SizeCoefficient, 3.5f);
            distance.SetStartValue(distanceToTatget);
            Debug.Log(distanceToTatget);
            return distanceToTatget;
        }

        private void SetActivatingDistance()
        {
            var activator = _startValueSetters.FirstOrDefault(IStartValueSetter => IStartValueSetter is IEnemyActivator activatingDistance);
            activator.SetStartValue(_config.ActivatingDistance);
        }

        private void SetStunChance()
        {
            var stunChance = _startValueSetters.FirstOrDefault(IStartValueSetter => IStartValueSetter is IStunChance chance);
            stunChance.SetStartValue(_config.StunChance);
        }

        private void SetPushPower()
        {
            var pushing = _startValueSetters.FirstOrDefault(IStartValueSetter => IStartValueSetter is IPushing push);
            pushing.SetStartValue(_config.PushPower);
        }

        private void SetScale()
        {
            var size = _config.Size;
            transform.localScale = new Vector3(size, size, size);
        }
    }
}