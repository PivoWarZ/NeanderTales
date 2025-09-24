using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class SceneSwitcher: MonoBehaviour
    {
        [SerializeField] private string sceneNameToLoad;

        private AsyncOperation asyncLoadOperation;
        private bool isSceneLoaded = false;

        private void Start()
        {
            PreloadSceneAsync().Forget();
        }

        private async UniTaskVoid PreloadSceneAsync()
        {
            asyncLoadOperation = SceneManager.LoadSceneAsync(sceneNameToLoad);
            asyncLoadOperation.allowSceneActivation = false;

            // Ждём, пока загрузка не дойдёт до 0.9 (сцена загружена, но не активирована)
            while (asyncLoadOperation.progress < 0.9f)
            {
                // Можно здесь обновить UI загрузки или логи
                await UniTask.Yield();
            }

            isSceneLoaded = true;
            Debug.Log("Scene preloaded and waiting for activation");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isSceneLoaded)
            {
                asyncLoadOperation.allowSceneActivation = true;
            }
        }
    }
}