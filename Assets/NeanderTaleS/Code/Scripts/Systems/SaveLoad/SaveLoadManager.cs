using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad
{
    public sealed class SaveLoadManager
    {
        private readonly ISaveLoader[] _saveLoaders;
        private readonly IGameRepository _gameRepository;
        private readonly IContext _context;

        public SaveLoadManager(ISaveLoader[] saveLoaders, GameRepository gameRepository, IContext context)
        { 
            _saveLoaders = saveLoaders;
            _gameRepository = gameRepository;
            _context = context;
        }

        public void SaveGame()
        {
            for (int i = 0; i < _saveLoaders.Length; i++)
            {
                ISaveLoader loader = _saveLoaders[i];
                loader.SaveGame(_context, _gameRepository);
            }

            _gameRepository.SaveState();
        }

        public void LoadGame()
        {
            _gameRepository.LoadState();

            for (int i = 0; i < _saveLoaders.Length; i++)
            {
                ISaveLoader loader = _saveLoaders[i];
                loader.LoadGame(_context, _gameRepository);
            }
        }
    }
}