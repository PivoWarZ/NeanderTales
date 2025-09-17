using NeanderTaleS.Code.Scripts.Animation;
using NeanderTaleS.Code.Scripts.Skills.Installers;
using NeanderTaleS.Code.Scripts.WeaponComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class EntityBootsTrap: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;

        public void EntityInitialize()
        {
            _localProvider.Initialize();

            InitializeAnimatorControllers(_localProvider);

            InitializeSkill(_localProvider);

            InitializeCurrentWeapon(_localProvider);
        }

        private void InitializeSkill(LocalProvider localProvider)
        {
            var isSkillEntity = localProvider.TryGetService<SkillsInstaller>(out var skills);

            if (isSkillEntity)
            {
                skills.Initialize(localProvider);
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