namespace NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events.GameCyclesEvents
{
    public class ResumeGameRequestEvent: IEventBusEvent
    {
        public ResumeGameRequestEvent(string calling)
        {
            Calling = calling;
        }

        public string Calling { get; }
    }
}