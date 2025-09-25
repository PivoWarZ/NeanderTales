using NeanderTaleS.Code.Scripts.Systems.SaveLoad;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Character;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class SaveLoadSystemInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindContext();
            BindSaveLoaders();
            BindGameRepository();
            BindIGameStateSaver();
            BindSaveLoadManager();
            Container.Bind<GameLoader>().AsSingle().NonLazy();
            
            Debug.Log($"Binding {GetType().Name}");
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
            Container.BindInterfacesAndSelfTo<GameRepository>().AsSingle();
        }

        private void BindSaveLoaders()
        {
            Container.BindInterfacesAndSelfTo<CharacterSaveLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExperienceSaveLoad>().AsSingle();
        }

        private void BindContext()
        {
            Container.Bind<IContext>().To<ZenjectContext>().AsSingle();
        }
    }
}