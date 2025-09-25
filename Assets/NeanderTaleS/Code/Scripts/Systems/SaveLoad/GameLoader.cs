using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad
{
    public class GameLoader
    {
        private SaveLoadManager _saveLoadManager;

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