namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events
{
    public sealed class LevelUpEvent: IEventBusEvent
    {
        public LevelUpEvent(string calling)
        {
            Calling = calling;
        }

        public string Calling { get; }
    }
}