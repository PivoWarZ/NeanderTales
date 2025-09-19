using System;

namespace NeanderTaleS.Code.Scripts.Core.Interfaces.Animations
{
    public interface IHitAnimationListener
    {
        event Action OnHitAnimation;
    }
}