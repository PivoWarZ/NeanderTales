namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository
{
    public interface IGameRepository
    {
        bool TryGetData<T>(out T data);
        void SaveData<T>(T data);
        void SaveState(); 
        void LoadState();
    }
}