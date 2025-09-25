namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public interface IFinishGameListener: IGameCycle
    {
        void OnFinishGame();
    }
}