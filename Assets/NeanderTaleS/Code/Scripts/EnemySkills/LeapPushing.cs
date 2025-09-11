using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemySkills
{
    public class LeapPushing: MonoBehaviour
    {
        [SerializeField] private LeapSkill _leapSkill;
        [SerializeField] private OnCollisionComponent _collision;
        [SerializeField] private float _pushPower;
        private UniTaskCompletionSource<Collision> _task;
        private CancellationTokenSource _cancell = new();

        private void Awake()
        {
            _leapSkill.OnLeapAttack += Push;
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
            _collision.OnEnterCollision -= Complete;
            var pushPoint = collision.contacts[0].point;
            var pushDirection = pushPoint - transform.position;
            Rigidbody rigidbody = collision.rigidbody;
            rigidbody.AddForceAtPosition(pushDirection * _pushPower, pushPoint, ForceMode.Impulse);
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