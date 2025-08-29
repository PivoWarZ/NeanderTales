using NeanderTaleS.Code.Scripts.Animation;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents
{
    public class PlayerProvider: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventDispatcher _eventDispatcher;

        public Animator Animator => _animator;

        public AnimationEventDispatcher AnimationEventDispatcher => _eventDispatcher;
    }
}