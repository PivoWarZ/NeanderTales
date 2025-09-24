using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository
{
    public interface ISaveLoader
    {
        void LoadGame(IContext context, IGameRepository gameRepository);
        void SaveGame(IContext context, IGameRepository gameRepository);
    }
}