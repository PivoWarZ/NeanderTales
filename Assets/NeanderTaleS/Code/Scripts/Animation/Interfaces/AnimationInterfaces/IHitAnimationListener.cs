using System;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces.AnimationInterfaces
{
    public interface IHitAnimationListener
    {
        event Action OnHitAnimation;
    }
}