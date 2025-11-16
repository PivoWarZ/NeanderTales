using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation
{
    public sealed class AnimationEventDispatcher: MonoBehaviour
    {
        public event Action<string> OnReceiveEvent;

        public void ReceiveEvent(string message)
        {
            OnReceiveEvent?.Invoke(message);
        }
    }
}