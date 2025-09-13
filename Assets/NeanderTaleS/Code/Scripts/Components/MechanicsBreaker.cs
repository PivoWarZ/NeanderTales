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

        public void ResumeCoreMechanics()
        {
            ResumeAttacking();
            ResumeRotating();
            ResumeMoving();
        }

        public void BanAttacking()
        {
            Switch<IAttackable>(false);
        }

        public void ResumeAttacking()
        {
            Switch<IAttackable>(true);
        }

        public void BanMoving()
        {
            Switch<IMovable>(false);
        }

        public void ResumeMoving()
        {
            Switch<IMovable>(true);
        }

        public void BanRotating()
        {
            Switch<IRotatable>(false);
        }

        public void ResumeRotating()
        {
            Switch<IRotatable>(true);
        }

        private void Switch<T>(bool isActive)
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