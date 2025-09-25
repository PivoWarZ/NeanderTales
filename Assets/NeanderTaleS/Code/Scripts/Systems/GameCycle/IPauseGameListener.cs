namespace NeanderTaleS.Code.Scripts.Systems.GameCycle
{
    public interface IPauseGameListener: IGameCycle
    {
        void OnPauseGame();
    }
}