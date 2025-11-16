using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public sealed class RootTransforms: MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftFoot;
        [SerializeField] private Transform _rightFoot;

        public Transform Head => _head;

        public Transform LeftHand => _leftHand;

        public Transform RightHand => _rightHand;

        public Transform LeftFoot => _leftFoot;

        public Transform RightFoot => _rightFoot;
    }
}