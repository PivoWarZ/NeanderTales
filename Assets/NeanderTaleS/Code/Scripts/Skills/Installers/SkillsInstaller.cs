using NeanderTaleS.Code.Scripts.Animation.Interfaces.AnimationInterfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Skills.Installers
{
    public class SkillsInstaller: MonoBehaviour
    {
        public void Initialize(LocalProvider localProvider)
        {
            AttackComponent attackComponent = localProvider.GetService<AttackComponent>();
            IHitAnimationListener hitAnimationListener = localProvider.GetInterface<IHitAnimationListener>();
           
            AddComboAttack(attackComponent, hitAnimationListener);
        }

        private static void AddComboAttack(AttackComponent attackComponent, IHitAnimationListener hitAnimationListener)
        {
            ComboAttack combo = new ComboAttack();
            combo.Init(attackComponent, hitAnimationListener);
        }
    }
}