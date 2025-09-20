using System;

namespace NeanderTaleS.Code.Scripts.Interfaces.Animations
{
    public interface IHitAnimationListener
    {
        event Action OnHitAnimation;
    }
}