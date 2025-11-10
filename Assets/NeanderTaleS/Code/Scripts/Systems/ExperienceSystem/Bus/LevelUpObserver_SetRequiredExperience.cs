using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.ExperienceSystem
{
    public class LevelUpObserver_SetRequiredExperience: IInitializable
    {
        private IExperienceSetter _experienceSetter;
        private IEventBus _eventBus;

        public LevelUpObserver_SetRequiredExperience(IExperienceSetter experienceSetter, IEventBus eventBus)
        {
            _experienceSetter = experienceSetter;
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<LevelUpEvent>(OnLevelUp);
        }

        private void OnLevelUp(LevelUpEvent @event)
        {
            var requiredExperience = @event.RequiredExperienceToNextLevel;
            _experienceSetter.SetRequiredExperience(requiredExperience);
        }
    }
}