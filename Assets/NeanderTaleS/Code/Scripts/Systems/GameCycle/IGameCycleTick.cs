namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public interface IGameCycleTick: IGameCycle
    {
        void Tick(float deltaTime);
    }
}