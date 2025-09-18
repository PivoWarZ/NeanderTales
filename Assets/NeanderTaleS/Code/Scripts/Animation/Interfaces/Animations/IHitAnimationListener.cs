using System;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces.Animations
{
    public interface IHitAnimationListener
    {
        event Action OnHitAnimation;
    }
}