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
        private readonly EnemyStateView _view;
        private readonly Timer _timer = new ();
        IHitPointsComponent _hitPointsComponent;
        private const float LIFE_TIME = 3f;
        private readonly CancellationTokenSource _cancell = new ();

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
        
        public void Construct(float damage, IHitPointsComponent hitPointsComponent)
        {
            _view.gameObject.SetActive(true);
            
            if (_hitPointsComponent != hitPointsComponent || _hitPointsComponent == null)
            {
                _hitPointsComponent = hitPointsComponent;
                
                InitView(damage, _hitPointsComponent);
                _timer.Start();
            }
            else
            {
                SetSlider(damage, _hitPointsComponent);
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

        public void SetSlider(float damage, IHitPointsComponent hitPoints)
        {
            var newValue = (hitPoints.CurrentHitPoints.CurrentValue - damage) / hitPoints.MaxHitPoints.CurrentValue;
            _view.SetSlider(newValue);
        }

        private void InitView(float damage, IHitPointsComponent hitPoints)
        {
            var newValue = (hitPoints.CurrentHitPoints.CurrentValue - damage) / hitPoints.MaxHitPoints.CurrentValue;
            _view.Init(newValue);
        }


        public void Dispose()
        {
            _cancell.Cancel();
            _timer.OnEnded -= HidePopup;
        }
    }
}