using NeanderTaleS.Code.CoreScripts.Services;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.CoreScripts.DI.ZenjectContext
{
    public class ZenjectContextInstaller: MonoInstaller
    {
        [SerializeField] GameObject _playerPrefab;
        public override void InstallBindings()
        {
            BindPlayerService(_playerPrefab);
        }


        private void BindPlayerService(GameObject prefab)
        {
            Container.Bind<PlayerService>().AsSingle().WithArguments(prefab);
        }
    }
}