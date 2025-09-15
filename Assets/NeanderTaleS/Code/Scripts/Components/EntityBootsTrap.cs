using System.Collections.Generic;
using System.Threading;
using NeanderTaleS.Code.Scripts.Animation;
using NeanderTaleS.Code.Scripts.Animation.EnemyAnimation;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces;
using NeanderTaleS.Code.Scripts.Skills.Installers;
using NeanderTaleS.Code.Scripts.WeaponComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class EntityBootsTrap: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        private CancellationTokenSource _cancell = new ();

        public void EntityInitialize()
        {
            _localProvider.Initialize();
            
            InstallMechanicsBreaker();

            InitializeBreakMechanicsComponents();

            InitializeAnimatorControllers(_localProvider);

            InitializeSkill(_localProvider);

            InitializeCurrentWeapon(_localProvider);
            
        }


        private void InitializeBreakMechanicsComponents()
        {
            bool isBreakers = _localProvider.TryGetInterfaces<IBreakMechanics>(out List<IBreakMechanics> mechanicsBreakers);

            if (!isBreakers)
            {
                return;
            }
            
            foreach (var mechanicsBreaker in mechanicsBreakers)
            {
                mechanicsBreaker.SetMechanicsBreaker(_localProvider.MechanicsBreaker);
            }
        }

        private void InitializeSkill(LocalProvider localProvider)
        {
            var isSkillEntity = localProvider.TryGetService<SkillsInstaller>(out var skills);

            if (isSkillEntity)
            {
                skills.Initialize(localProvider);
            }
        }

        private void InstallMechanicsBreaker()
        {
            MechanicsBreaker mechanicsBreaker = new ();
            List<IBreakable> breakables = _localProvider.GetInterfaces<IBreakable>();
            mechanicsBreaker.Init(breakables);
            _localProvider.MechanicsBreaker = mechanicsBreaker;
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

        private void OnDestroy()
        {
            _cancell.Cancel();
        }
    }
}