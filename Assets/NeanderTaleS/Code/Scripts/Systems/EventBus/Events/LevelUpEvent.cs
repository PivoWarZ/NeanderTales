namespace NeanderTaleS.Code.Scripts.Systems.EventBus.Events
{
    public class LevelUpEvent: IEventBusEvent
    {
        public LevelUpEvent(string calling)
        {
            Calling = calling;
        }

        public string Calling { get; }
    }
}