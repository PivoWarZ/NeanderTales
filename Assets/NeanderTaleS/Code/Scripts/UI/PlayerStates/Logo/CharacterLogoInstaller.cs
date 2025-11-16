using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Logo
{
    public sealed class CharacterLogoInstaller: IInitializable
    {
        private LogoView _view;

        public CharacterLogoInstaller(HudUI hud)
        {
            _view = hud.LogoView;
        }

        void IInitializable.Initialize()
        {
            _view.Logo.sprite = Resources.Load<Sprite>("PlayerLogo");
        }
    }
}