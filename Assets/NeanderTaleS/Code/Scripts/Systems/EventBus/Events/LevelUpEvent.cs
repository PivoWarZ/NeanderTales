namespace NeanderTaleS.Code.Scripts.Systems.EventBus.Events
{
    public class LevelUpEvent
    {
        public LevelUpEvent(int requiredExperienceToNextLevel)
        {
            RequiredExperienceToNextLevel = requiredExperienceToNextLevel;
        }

        public int RequiredExperienceToNextLevel { get; }
    }
}