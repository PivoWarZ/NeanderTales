using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Interfaces.Animations
{
    public interface IDyingAnimation
    {
        event Action<Vector3> OnDyingAnimationComplete;
    }
}