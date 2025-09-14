using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;

namespace NeanderTaleS.Code.Scripts.Animation
{
    public class AnimationControllersInstaller
    {
        public void Init(LocalProvider localProvider)
        {
            List<IAnimationController> animationControllers = localProvider.GetInterfaces<IAnimationController>();

            for (var index = 0; index < animationControllers.Count; index++)
            {
                var animationController = animationControllers[index];
                animationController.Init(localProvider);
            }
        }
    }
}