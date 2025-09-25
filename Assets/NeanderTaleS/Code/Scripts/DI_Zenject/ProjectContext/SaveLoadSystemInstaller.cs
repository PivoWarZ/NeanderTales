using NeanderTaleS.Code.Scripts.Systems.SaveLoad;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Character;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;
using UnityEngine.SceneManagement;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class SaveLoadSystemInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IContext>().To<ZenjectContext>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterSaveLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameRepository>().AsSingle();
            Container.Bind<IGameStateSaver>().To<SaveToFileGameStateSaver>().AsSingle();
            Container.Bind<SaveLoadManager>().AsSingle();
            
            SceneManager.LoadScene("MainMenu");
        }
    }
}