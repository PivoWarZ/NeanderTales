using System;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class ExperienceRewardComponent: MonoBehaviour, IExperienceDealer
    {
        public event Action<float> OnDealExperience;
       
        public float Experience;
        
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        private IDisposable _disposable;

        private void Awake()
        {
            _disposable = _hitPointsComponent.CurrentHitPoints.Where(hitPoints => hitPoints <= 0).Subscribe(DealExperience);
        }

        private void DealExperience(float _)
        {
            OnDealExperience?.Invoke(Experience);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}