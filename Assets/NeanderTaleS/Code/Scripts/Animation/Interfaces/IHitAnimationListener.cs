using System;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces
{
    public interface IHitAnimationListener
    {
        event Action OnHitAnimation;
    }
}