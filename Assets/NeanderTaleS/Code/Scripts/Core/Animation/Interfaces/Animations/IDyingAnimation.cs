using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.Interfaces.Animations
{
    public interface IDyingAnimation
    {
        event Action<Vector3> OnDyingAnimationComplete;
    }
}