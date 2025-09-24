using NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.Effects.StepsFX;
using NeanderTaleS.Code.Scripts.Core.EnemySkills;
using NeanderTaleS.Code.Scripts.Core.WeaponComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.WeaponInterfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class Enemy: MonoBehaviour
    {
        [SerializeField] private EntityBootsTrap _bootsTrap;
        [SerializeField] private LocalProvider _provider;
        [SerializeField] private VelociraptorConfig _config;
        
        [Button]
        public void InitEnemy(VelociraptorConfig config)
        {
            _config = config;
            
            _bootsTrap.EntityInitialize();
            InitializeLeapSkill(_provider);
            
            SetScale();
            SetHitPoints();
            SetDamage();
            SetSpeed();
            var attackdistance = SetAttackDistance();
            var jumpDistance = SetLeapSkillJumpDistance(attackdistance);
            var activateLeapSkill = SetLeapSkillActivatingDistance(jumpDistance);
            SetActivatingDistance(activateLeapSkill);
            SetStunChance();
            SetPushPower();
            SetStepFxScale();
            SetExperienceReward();
            InitializeStepFXInstaller();
        }

        private void SetExperienceReward()
        {
            var exp = _provider.GetService<ExperienceRewardComponent>();
            exp.Experience = _config.Experience;
        }

        private void InitializeStepFXInstaller()
        {
            var stepFxInstaller = _provider.GetService<StepsFXInstaller>();
            stepFxInstaller.Init();
        }

        private void SetStepFxScale()
        {
            var stepFX = _provider.GetService<OnStepFX>();
            stepFX.Scale *= _config.SizeCoefficient;
            stepFX.CreateEffects();
        }

        private void InitializeLeapSkill(LocalProvider localProvider)
        {
            var leapSkill = localProvider.GetService<LeapRaptorSkill>();
            leapSkill.Init();
        }

        private void SetHitPoints()
        {
            var health = _provider.GetInterface<ITakeDamageable>();
           health.AddedtHitPoints(_config.HitPoints, _config.HitPoints);
        }

        private void SetDamage()
        {
            var damage = _provider.GetInterface<IWeapon>();
            damage.SetDamage(_config.Damage);

        }

        private void SetSpeed()
        {
            var speed = _provider.GetInterface<IMovable>();
            speed.SetSpeed(_config.Speed);
        }

        private float SetAttackDistance()
        {
            var distance = _provider.GetInterface<IAttackDistance>();
            var weapon = _provider.GetService<Weapon>();
            var distanceToWeapon = (weapon.transform.position - transform.position).magnitude;
            var distanceToTatget = Mathf.Min(distanceToWeapon + 1.3f * _config.SizeCoefficient, 3.5f);
            distance.SetAttackDistance(distanceToTatget);
            return distanceToTatget;
        }

        private float SetLeapSkillActivatingDistance(float jumpDistance)
        {
            var offset = 1f;
            var activating = jumpDistance + offset;
            _provider.GetService<LeapRaptorSkill>().SetActivateDistance(activating);
            
            return activating;
        }

        private float SetLeapSkillJumpDistance(float attackDistance)
        {
            var offset = 0.5f;
            var jumpDistance = attackDistance + offset;
            _provider.GetService<LeapRaptorSkill>().SetJumpDistance(jumpDistance);
            return jumpDistance;
        }

        private void SetActivatingDistance(float LeapSkillActivating)
        {
            float offset = 1f;
            var activator = _provider.GetInterface<IEnemyActivator>();
            activator.SetActivatingDistance(LeapSkillActivating + offset);
        }

        private void SetStunChance()
        {
            var stunChance = _provider.GetInterface<IStunChance>();
            stunChance.SetStunChance(_config.StunChance);
        }

        private void SetPushPower()
        {
            var pushing = _provider.GetInterface<IPushing>();
            pushing.SetPushPower(_config.PushPower);
        }

        private void SetScale()
        {
            var size = _config.Size;
            transform.localScale = new Vector3(size, size, size);
        }
    }
}