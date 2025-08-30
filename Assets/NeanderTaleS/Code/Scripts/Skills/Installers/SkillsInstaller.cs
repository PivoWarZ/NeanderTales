using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Skills.Installers
{
    public class SkillsInstaller: IInitializable
    {
        private PlayerProvider _playerProvider;

        public SkillsInstaller(PlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            AttackComponent attackComponent = _playerProvider.AttackComponent;
            Transform visual = _playerProvider.Visual;
            IHitAnimationListener hitAnimationListener = visual.GetComponent<IHitAnimationListener>();
           
            ComboAttack combo = new ComboAttack();
            combo.Init(attackComponent, hitAnimationListener);
        }
    }
}