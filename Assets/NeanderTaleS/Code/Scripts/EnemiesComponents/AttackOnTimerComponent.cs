using NeanderTaleS.Code.Scripts.Services;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class AttackOnTimerComponent: MonoBehaviour
    {
        [SerializeField] private float _attackColdown = 2f;
        private Timer _attackTimer;
        public bool IsAttackTime;

        private void Awake()
        {
            _attackTimer = new Timer();
            _attackTimer.IsLoop = false;
            _attackTimer.Duration = _attackColdown;
            
            _attackTimer.OnEnded += AttackReady;
        }

        private void Update()
        {
            _attackTimer.Tick(Time.deltaTime);
        }

        private void AttackReady()
        {
            IsAttackTime = true;
        }

        public void StartAttackTimer()
        {
            _attackTimer.Start();
        }

        public void PauseAttackTimer()
        {
            _attackTimer.Pause();
        }

        public void ResumeAttackTimer()
        {
            _attackTimer.Resume();
        }

        public void StopAttackTimer()
        {
            _attackTimer.Stop();
            IsAttackTime = false;
        }

        private void OnDestroy()
        {
            _attackTimer.OnEnded -= AttackReady;
        }
    }
}