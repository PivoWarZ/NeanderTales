using System;

namespace NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Animations
{
    public interface IHitAnimationListener
    {
        event Action OnHitAnimation;
    }
}