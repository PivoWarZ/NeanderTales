using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.CoreScripts.Condition;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.PlayerComponents.Components
{
    public class StaminaRecovering: MonoBehaviour
    {
        public int RecoveringSpeed;
        
        [SerializeField] private StaminaComponent _staminaComponent;
        private IDisposable _disposable;
        private CancellationTokenSource _cancell;

        private void Awake()
        {
            SubscribesStartRecovering();
        }

        private void Recovering(float stamina)
        {
            _cancell = new CancellationTokenSource();
            
            _disposable.Dispose();
            
            _disposable = _staminaComponent.Stamina
                .Where(currentStamina => Mathf.Approximately(currentStamina, _staminaComponent.MaxStamina.CurrentValue))
                .Subscribe(StopRecovering);
            
            Run(_cancell).Forget();
        }

        private async UniTaskVoid Run(CancellationTokenSource cancell)
        {
            while (!cancell.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                _staminaComponent.AddStamina(RecoveringSpeed);
            }
        }

        private void StopRecovering(float _)
        {
            _cancell.Cancel();
            _disposable.Dispose();
            
            SubscribesStartRecovering();
        }

        private void SubscribesStartRecovering()
        {
            _disposable = _staminaComponent.Stamina
                .Where(stamina => stamina < _staminaComponent.MaxStamina.CurrentValue)
                .Subscribe(Recovering);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
            _cancell?.Cancel();
        }
    }
}