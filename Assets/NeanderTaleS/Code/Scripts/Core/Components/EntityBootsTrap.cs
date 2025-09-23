using NeanderTaleS.Code.Scripts.Core.Animation;
using NeanderTaleS.Code.Scripts.Core.Effects.StepsFX;
using NeanderTaleS.Code.Scripts.Core.EnemySkills;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components;
using NeanderTaleS.Code.Scripts.Core.WeaponComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class EntityBootsTrap: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;

        public void EntityInitialize()
        {
            _localProvider.Initialize();

            InitializeAnimatorControllers(_localProvider);

            InitializeCurrentWeapon(_localProvider);
            
            InitializeStamina(_localProvider);
            
            InitializeDebuffComponent(_localProvider);

            InitializeConditionInstaller(_localProvider);
            
            InitializeStepFX();
            
            Debug.Log("Entity Initialize");
            
        }

        private void InitializeStepFX()
        {
            _localProvider.GetService<StepsFXInstaller>().Init();
        }

        private void InitializeConditionInstaller(LocalProvider localProvider)
        {
            var conditionInstaller = localProvider.GetService<ConditionInstaller>();
            conditionInstaller.Init();
        }

        private void InitializeDebuffComponent(LocalProvider localProvider)
        {
            var isDebuff = localProvider.TryGetService<DebuffsComponent>(out DebuffsComponent debuffsComponent);

            if (isDebuff)
            {
                debuffsComponent.Init();
            }
        }

        private void InitializeStamina(LocalProvider localProvider)
        {
            bool isStamina = localProvider.TryGetService<StaminaComponent>(out var staminaComponent);

            if (isStamina)
            {
                IAttackable attacker = localProvider.GetInterface<IAttackable>();
                staminaComponent.Init(attacker);
            }
        }

        private void InitializeCurrentWeapon(LocalProvider localProvider)
        {
            WeaponInitializer initializer = new();
            initializer.Init(localProvider);
        }

        private void InitializeAnimatorControllers(LocalProvider localProvider)
        {
            AnimationControllersInstaller installer = new ();
            installer.Init(localProvider);
        }
    }
}