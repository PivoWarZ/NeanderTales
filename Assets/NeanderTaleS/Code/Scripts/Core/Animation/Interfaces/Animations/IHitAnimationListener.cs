using System;

namespace NeanderTaleS.Code.Scripts.Core.Animation.Interfaces.Animations
{
    public interface IHitAnimationListener
    {
        event Action OnHitAnimation;
    }
}