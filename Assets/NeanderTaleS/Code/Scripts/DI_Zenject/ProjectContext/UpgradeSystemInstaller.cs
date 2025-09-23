using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.Experience;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class UpgradeSystemInstaller: MonoInstaller
    {
        [SerializeField] private CharacterUpgradesConfig _config;
        public override void InstallBindings()
        {
          //  Container.Bind<ExperienceCoinsStorage>().AsSingle().NonLazy();
          //  Container.Bind<IExperienceStorage>().To<ExperienceStorage>().AsSingle();
         //   Container.BindInterfacesAndSelfTo<ExperienceSystem>().AsSingle().NonLazy();
            
          //  Container.BindInstance(_config);
          // Container.Bind<CharacterUpgrade>().AsSingle().NonLazy();
           // Container.BindInterfacesAndSelfTo<CharacterUpgradesSystem>().AsSingle().NonLazy();
        }
    }
}