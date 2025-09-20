using System;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class ExperienceComponent: MonoBehaviour, IExperienceDealer
    {
        public event Action<float> OnDealExperience;
        
        [SerializeField] private float _experience;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        private IDisposable _disposable;

        private void Awake()
        {
            _disposable = _hitPointsComponent.CurrentHitPoints.Where(hitPoints => hitPoints <= 0).Subscribe(DealExperience);
        }

        private void DealExperience(float _)
        {
            OnDealExperience?.Invoke(_experience);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}