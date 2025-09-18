using System.Collections.Generic;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.CoreScripts.Components;

namespace NeanderTaleS.Code.CoreScripts.Animation
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