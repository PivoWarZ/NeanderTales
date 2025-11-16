using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemySkills
{
    public sealed class LeapPushing: MonoBehaviour, IPushing
    {
        public event Action<GameObject> OnPushing;
        
        [SerializeField] private LeapRaptorSkill _leapRaptorSkill;
        [SerializeField] private OnCollisionComponent _collision;
        [SerializeField] private float _pushPower;
        private Rigidbody _rigidbody;
        private UniTaskCompletionSource<Collision> _task;
        private readonly CancellationTokenSource _cancell = new();

        public float PushPower => _pushPower;

        void IPushing.SetPushPower(float power)
        {
            _pushPower = power;
        }

        private void Awake()
        {
            _leapRaptorSkill.OnLeapAttack += Push;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Push()
        {
            _task = new UniTaskCompletionSource<Collision>();
            _collision.OnEnterCollision += Complete;
            Run(_cancell).Forget();
        }

        private async UniTaskVoid Run(CancellationTokenSource cancel)
        {
            Collision collision = await _task.Task;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.linearVelocity = Vector3.zero;
            _collision.OnEnterCollision -= Complete;
            var contactPoint = collision.contacts[0].point;
            bool isRigidbody = collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody);
            
            if (isRigidbody)
            {
                var pushDirection = rigidbody.position - contactPoint;
                pushDirection.y = 0;
                rigidbody.AddForceAtPosition(pushDirection * PushPower, contactPoint, ForceMode.Impulse);
                
                OnPushing?.Invoke(collision.gameObject);
            }
        }

        private void Complete(Collision collision)
        {
            _task.TrySetResult(collision);
        }

        private void OnDestroy()
        {
            _cancell.Cancel();
            _collision.OnEnterCollision -= Complete;
        }
    }
}