using System;
using R3;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Experience
{
    public sealed class ExperienceViewModel: IDisposable
    {
        public event Action OnDataChanged;
        private ExperienceModel _model;
        private IDisposable _disposable;
        private float _currentExperience;
        private float _requiredExperience;

        public ExperienceViewModel(ExperienceModel model)
        {
            _model = model;
        }

        public float CurrentExperience => _currentExperience;

        public float RequiredExperience => _requiredExperience;

        public void Initialize()
        {
            _disposable = _model.CurrentExperience.Merge(_model.MaxExperience).Subscribe(OnDataChange);
        }

        private void OnDataChange(float _)
        {
            _currentExperience = _model.CurrentExperience.CurrentValue;
            _requiredExperience = _model.MaxExperience.CurrentValue;
            
            OnDataChanged?.Invoke();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}