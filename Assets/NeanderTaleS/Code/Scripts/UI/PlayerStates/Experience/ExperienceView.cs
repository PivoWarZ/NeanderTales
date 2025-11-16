using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Experience
{
    public sealed class ExperienceView: MonoBehaviour
    {
        [SerializeField] private Slider _experienceSlider;
        private ExperienceViewModel _viewModel;
        private float _currentExperience;
        private float _requiredExperience;
        private float _sliderValue;

        public void Construct(ExperienceViewModel viewModel)
        {
            _experienceSlider.interactable = false;
            _viewModel = viewModel;
            _viewModel.OnDataChanged += Refresh;
            Initialize();
        }
        
        private void Initialize()
        {
            _currentExperience = _viewModel.CurrentExperience;
            _requiredExperience = _viewModel.RequiredExperience;
            _sliderValue = _requiredExperience > 0 ? _currentExperience / _requiredExperience : 0;
            
            DOTween.To(() => _experienceSlider.value, exp => _experienceSlider.value = exp, _sliderValue, 0.5f);
        }

        private void Refresh()
        {
            Initialize();
        }
    }
}