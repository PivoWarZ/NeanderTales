namespace NeanderTaleS.Code.Scripts.Systems.EventBus.Events
{
    public sealed class ExperiencePriceChangedEvent: IEventBusEvent
    {
        public ExperiencePriceChangedEvent(float requiredExperience, string calling)
        {
            RequiredExperience = requiredExperience;
            Calling = calling;
        }
        public float RequiredExperience { get; }
        public string Calling { get; }
    }
}