using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces.AnimationInterfaces
{
    public interface IDyingAnimation
    {
        event Action<Vector3> OnDyingAnimationComplete;
    }
}