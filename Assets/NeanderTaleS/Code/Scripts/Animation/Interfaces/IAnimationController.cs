using NeanderTaleS.Code.Scripts.Animation.PlayerAnimation;
using NeanderTaleS.Code.Scripts.PlayerComponents;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces
{
    public interface IAnimationController
    {
        void Init(PlayerProvider playerProvider, AnimationEventDispatcher eventDispatcher);
    }
}