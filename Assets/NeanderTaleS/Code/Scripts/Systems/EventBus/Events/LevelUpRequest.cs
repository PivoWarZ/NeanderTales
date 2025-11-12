namespace NeanderTaleS.Code.Scripts.Systems.EventBus.Events
{
    public class LevelUpRequest: IEventBusEvent
    {
        public LevelUpRequest(string calling)
        {
            Calling = calling;
        }

        public string Calling { get; }
    }
}