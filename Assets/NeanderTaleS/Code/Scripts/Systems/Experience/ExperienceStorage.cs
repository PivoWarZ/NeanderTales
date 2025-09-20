using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Experience
{
    public class ExperienceStorage: IExperienceStorage
    {
        ReactiveProperty<float> _requiredExperience = new (10);
        ReactiveProperty<float> _currentExperience = new();
        ReactiveProperty<int> _currentLevel = new (1);
        public ReadOnlyReactiveProperty<float> RequiredExperience => _requiredExperience;
        public ReadOnlyReactiveProperty<float> CurrentExperience => _currentExperience;
        public ReadOnlyReactiveProperty<int> Level => _currentLevel;
        
        public void LevelUp(float requiredExperience)
        {
            _requiredExperience.Value = requiredExperience;
            _currentLevel.Value++;
        }

        public void AddExperience(float exp)
        {
            _currentExperience.Value += exp;
        }
    }
}