using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class MechanicsBreaker: MonoBehaviour, IMachanicsBreaker
    {
        private List<IBreakable> _breakables = new ();

        private void Awake()
        {
            _breakables = GetComponents<IBreakable>().ToList();
        }

        public void BanCoreMechanics()
        {
            BanAttacking();
            BanRotating();
            BanMoving();
        }

        public void EnabledCoreMechanics()
        {
            ResumeAttacking();
            ResumeRotating();
            ResumeMoving();
        }

        public void BanAttacking()
        {
            SwitchSelectedMechanic<IAttackable>(false);
        }

        public void ResumeAttacking()
        {
            SwitchSelectedMechanic<IAttackable>(true);
        }

        public void BanMoving()
        {
            SwitchSelectedMechanic<IMovable>(false);
        }

        public void ResumeMoving()
        {
            SwitchSelectedMechanic<IMovable>(true);
        }

        public void BanRotating()
        {
            SwitchSelectedMechanic<ICursorFollower>(false);
        }

        public void ResumeRotating()
        {
            SwitchSelectedMechanic<ICursorFollower>(true);
        }

        public void SwitchSelectedMechanic<T>(bool isActive)
        {
            foreach (IBreakable breakable in _breakables)
            {
                if (breakable is T)
                {
                    if (isActive)
                    {
                        breakable.EnabledMechanic();
                    }
                    else
                    {
                        breakable.DisablingMechanic();
                    }
                }
            }
        }
    }
}