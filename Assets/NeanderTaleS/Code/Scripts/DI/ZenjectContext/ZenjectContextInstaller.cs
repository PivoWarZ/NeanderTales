using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Core.Services;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI.ZenjectContext
{
    public class ZenjectContextInstaller: MonoInstaller
    {
        [SerializeField] Player _player;
        public override void InstallBindings()
        {
            BindPlayerService(_player);
        }
        
        private void BindPlayerService(Player player)
        {
            PlayerService playerService = new PlayerService(player);
            Container.BindInstance(playerService).AsSingle();
        }
    }
}