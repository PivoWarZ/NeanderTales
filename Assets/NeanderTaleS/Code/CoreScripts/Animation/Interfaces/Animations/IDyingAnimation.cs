using System;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Animations
{
    public interface IDyingAnimation
    {
        event Action<Vector3> OnDyingAnimationComplete;
    }
}