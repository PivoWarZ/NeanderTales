using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.Services;
using NeanderTaleS.Code.Scripts.Skills.Installers;
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