using R3;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Experience
{
    public sealed class ExperienceModel
    {
        public readonly ReadOnlyReactiveProperty<float> CurrentExperience;
        public readonly ReadOnlyReactiveProperty<float> MaxExperience;

        public ExperienceModel(ReadOnlyReactiveProperty<float> currentExperience, ReadOnlyReactiveProperty<float> maxExperience)
        {
            CurrentExperience = currentExperience;
            MaxExperience = maxExperience;
        }
    }
}