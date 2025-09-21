using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;
using Timer = NeanderTaleS.Code.Scripts.Core.Services.Timer;

namespace NeanderTaleS.Code.Scripts.UI.EnemyStates
{
    public class EnemyStateAdapter: IDisposable
    {
        private EnemyStateView _view;
        private Timer _timer = new ();
        ITakeDamageble _takeDamageble;
        private const float LIFE_TIME = 3f;
        private CancellationTokenSource _cancell = new ();

        public EnemyStateAdapter(EnemyStateView view)
        {
            _view = view;
            
            _timer.IsLoop = false;
            _timer.Duration = LIFE_TIME;

            _timer.OnEnded += HidePopup;
            
            Run(_cancell).Forget();
        }

        private async UniTaskVoid Run(CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.WaitForFixedUpdate();

                if (_timer.IsPlaying())
                {
                    _timer.Tick(Time.fixedDeltaTime);
                }
            }
        }

        private void HidePopup()
        {
            _view.gameObject.SetActive(false);
        }

        public void Init(float damage, ITakeDamageble takeDamageble)
        {
            _view.gameObject.SetActive(true);
            
            if (_takeDamageble != takeDamageble || _takeDamageble == null)
            {
                _takeDamageble = takeDamageble;
                
                InitView(damage, _takeDamageble);
                _timer.Start();
            }
            else
            {
                SetSlider(damage, _takeDamageble);
                _timer.ForceStart();
            }
        }

        public void SetSlider(float damage, ITakeDamageble takeDamageble)
        {
            var newValue = (takeDamageble.CurrentHitPoints.CurrentValue - damage) / takeDamageble.MaxHitPoints.CurrentValue;
            _view.SetSlider(newValue);
        }

        private void InitView(float damage, ITakeDamageble takeDamageble)
        {
            var newValue = (takeDamageble.CurrentHitPoints.CurrentValue - damage) / takeDamageble.MaxHitPoints.CurrentValue;
            _view.Init(newValue);
        }


        public void Dispose()
        {
            _cancell.Cancel();
            _timer.OnEnded -= HidePopup;
        }
    }
}