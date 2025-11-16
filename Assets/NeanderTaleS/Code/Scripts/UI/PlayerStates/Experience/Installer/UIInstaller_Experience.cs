using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience;
using Zenject;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Experience.Installer
{
    public sealed class UIInstaller_Experience: IInitializable
    {
        private ExperienceView _view;
        private IExperienceStorage _experienceStorage;
        
        public UIInstaller_Experience(HudUI hudUI, IExperienceStorage experienceStorage)
        {
            _experienceStorage = experienceStorage;
            _view = hudUI.ExperienceView;
        }
        
        void IInitializable.Initialize()
        {
            Install();
        }

        private void Install()
        {
            ExperienceModel model = new ExperienceModel(_experienceStorage.CurrentExperience, _experienceStorage.RequiredExperience);
            ExperienceViewModel viewModel = new ExperienceViewModel(model);
            _view.Construct(viewModel);
        }
    }
}