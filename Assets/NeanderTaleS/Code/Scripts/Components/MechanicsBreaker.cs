using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ServiceInterfaces;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class MechanicsBreaker
    {
        private List<IBreakable> _breakables = new ();

        public void Init(List<IBreakable> breakables)
        {
            _breakables = breakables;
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