using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class UpgradeSystemInstaller: MonoInstaller
    {
        [SerializeField] private CharacterUpgradesConfig _config;
        public override void InstallBindings()
        {
            BindExperienceStorage();

            BindCharacterUpgrade();

            Container.BindInterfacesAndSelfTo<PlayerStatsInstaller>().AsSingle().NonLazy();
        }

        private void BindCharacterUpgrade()
        {
            Container.BindInstance(_config);
            Container.Bind<CharacterUpgrade>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<CharacterUpgradesSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindExperienceStorage()
        {
            Container.Bind<IExperienceStorage>()
                .To<ExperienceStorage>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ExperienceSystem>()
                .AsSingle()
                .NonLazy();
        }
        
    }
}