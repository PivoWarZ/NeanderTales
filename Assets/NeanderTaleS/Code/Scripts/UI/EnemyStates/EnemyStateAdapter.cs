using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;
using Zenject;
using Timer = NeanderTaleS.Code.Scripts.Core.Services.Timer;

namespace NeanderTaleS.Code.Scripts.UI.EnemyStates
{
    public class EnemyStateAdapter: IDisposable, IInitializable
    {
        private EnemyStateView _view;
        private Timer _timer = new ();
        ITakeDamageable _takeDamageable;
        private const float LIFE_TIME = 3f;
        private CancellationTokenSource _cancell = new ();

        public EnemyStateAdapter(EnemyStateView view)
        {
            _view = view;
        }


        void IInitializable.Initialize()
        {
            _timer.IsLoop = false;
            _timer.Duration = LIFE_TIME;

            _timer.OnEnded += HidePopup;
            
            Run(_cancell).Forget();
        }
        
        public void Construct(float damage, ITakeDamageable takeDamageable)
        {
            _view.gameObject.SetActive(true);
            
            if (_takeDamageable != takeDamageable || _takeDamageable == null)
            {
                _takeDamageable = takeDamageable;
                
                InitView(damage, _takeDamageable);
                _timer.Start();
            }
            else
            {
                SetSlider(damage, _takeDamageable);
                _timer.ForceStart();
            }
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

        public void SetSlider(float damage, ITakeDamageable takeDamageable)
        {
            var newValue = (takeDamageable.CurrentHitPoints.CurrentValue - damage) / takeDamageable.MaxHitPoints.CurrentValue;
            _view.SetSlider(newValue);
        }

        private void InitView(float damage, ITakeDamageable takeDamageable)
        {
            var newValue = (takeDamageable.CurrentHitPoints.CurrentValue - damage) / takeDamageable.MaxHitPoints.CurrentValue;
            _view.Init(newValue);
        }


        public void Dispose()
        {
            _cancell.Cancel();
            _timer.OnEnded -= HidePopup;
        }
    }
}