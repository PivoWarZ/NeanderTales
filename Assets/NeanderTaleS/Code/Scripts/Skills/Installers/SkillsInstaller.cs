using NeanderTaleS.Code.Scripts.Animation.Interfaces.Animations;
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
            ConditionInstaller conditionInstaller = localProvider.GetService<ConditionInstaller>();
           
            ComboAttack combo = new ComboAttack();
            combo.Init(attackComponent, conditionInstaller);
        }
    }
}