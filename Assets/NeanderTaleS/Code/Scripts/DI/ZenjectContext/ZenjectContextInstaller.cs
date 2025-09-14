using NeanderTaleS.Code.Scripts.Services;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI.ZenjectContext
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