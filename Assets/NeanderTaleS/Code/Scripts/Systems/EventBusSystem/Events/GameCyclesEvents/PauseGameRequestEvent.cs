namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events.GameCyclesEvents
{
    public class PauseGameRequestEvent: IEventBusEvent
    {
        public PauseGameRequestEvent(string calling)
        {
            Calling = calling;
        }

        public string Calling { get; }
    }
}