using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad
{
    public sealed class GameLoader
    {
        private readonly SaveLoadManager _saveLoadManager;

        public GameLoader(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }

        public async void LoadGame()
        {
            await SceneManager.LoadSceneAsync("Home");
            _saveLoadManager.LoadGame();
            
        }
    }
}