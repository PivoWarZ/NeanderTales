using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Skills.Installers
{
    public class SkillsInstaller: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;

        private void Start()
        {
            AttackComponent attackComponent = _localProvider.GetService<AttackComponent>();
            IHitAnimationListener hitAnimationListener = _localProvider.GetInterface<IHitAnimationListener>();
           
            AddComboAttack(attackComponent, hitAnimationListener);
        }

        private static void AddComboAttack(AttackComponent attackComponent, IHitAnimationListener hitAnimationListener)
        {
            ComboAttack combo = new ComboAttack();
            combo.Init(attackComponent, hitAnimationListener);
        }
    }
}