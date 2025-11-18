using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.Character;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.Experience;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.UpgradeCoins;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class SaveLoadSystemInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindContext();
            BindSaveLoaders();
            BindGameRepository();
            BindIGameStateSaver();
            BindSaveLoadManager();
            BindGameLoader();
            
            DebugLogger.PrintBinding(this);
        }

        private void BindGameLoader()
        {
            Container.Bind<GameLoader>()
                .AsSingle()
                .NonLazy();
        }

        private void BindIGameStateSaver()
        {
            Container.Bind<IGameStateSaver>().To<SaveToFileGameStateSaver>().AsSingle();
        }

        private void BindSaveLoadManager()
        {
            Container.Bind<SaveLoadManager>().AsSingle();
        }

        private void BindGameRepository()
        {
            Container.BindInterfacesAndSelfTo<GameRepository>().AsCached();
        }

        private void BindSaveLoaders()
        {
            Container.BindInterfacesAndSelfTo<CharacterSaveLoader>().AsCached();
            Container.BindInterfacesAndSelfTo<ExperienceSaveLoad>().AsCached();
            Container.BindInterfacesAndSelfTo<CoinsSaveLoad>().AsCached();
        }

        private void BindContext()
        {
            Container.Bind<IContext>().To<ZenjectContext>().AsSingle();
        }
    }
}