namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public interface IGameCycleFixedTick: IGameCycle
    {
        void FixedTick(float deltaTime);
    }
}