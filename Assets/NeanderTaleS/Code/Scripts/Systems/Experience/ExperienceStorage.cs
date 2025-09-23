using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Experience
{
    public class ExperienceStorage: IExperienceStorage
    {
        ReactiveProperty<float> _requiredExperience = new (10);
        ReactiveProperty<float> _currentExperience = new();
        public ReadOnlyReactiveProperty<float> RequiredExperience => _requiredExperience;
        public ReadOnlyReactiveProperty<float> CurrentExperience => _currentExperience;

        void IExperienceStorage.AddExperience(float exp)
        {
            _currentExperience.Value += exp;
        }

        void IExperienceStorage.SetRequiredExperience(float exp)
        {
            _requiredExperience.Value = exp;
        }
    }
}