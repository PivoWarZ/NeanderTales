using NeanderTaleS.Code.CoreScripts.Animation;
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