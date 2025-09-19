using NeanderTaleS.Code.Scripts.Core.Animation;
using NeanderTaleS.Code.Scripts.Core.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components;
using NeanderTaleS.Code.Scripts.Core.WeaponComponents;
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