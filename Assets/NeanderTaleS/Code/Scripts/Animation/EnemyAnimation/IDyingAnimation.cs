using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public interface IDyingAnimation
    {
        event Action<Vector3> OnDyingAnimationComplete;
    }
}