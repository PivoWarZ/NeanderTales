using NeanderTaleS.Code.CoreScripts.Animation;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.CoreScripts.PlayerComponents.Components;
using NeanderTaleS.Code.CoreScripts.Skills.Installers;
using NeanderTaleS.Code.CoreScripts.WeaponComponents;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Components
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