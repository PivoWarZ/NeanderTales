namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events
{
    public sealed class LevelUpRequest: IEventBusEvent
    {
        public LevelUpRequest(string calling)
        {
            Calling = calling;
        }

        public string Calling { get; }
    }
}