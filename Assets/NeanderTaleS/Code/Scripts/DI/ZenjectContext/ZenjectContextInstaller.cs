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
            PlayerProvider playerProvider = _playerPrefab.GetComponent<PlayerProvider>();
            
            BindPlayerProvider(playerProvider);
            BindPlayerService(_playerPrefab);
            Container.BindInterfacesAndSelfTo<SkillsInstaller>().AsSingle().NonLazy();
        }

        private void BindPlayerProvider(PlayerProvider playerProvider)
        {
            Container.BindInstance(playerProvider).AsSingle();
        }

        private void BindPlayerService(GameObject prefab)
        {
            Container.Bind<PlayerService>().AsSingle().WithArguments(prefab);
        }
    }
}