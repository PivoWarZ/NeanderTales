using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.Services;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Skills.Installers
{
    public class SkillsInstaller: IInitializable
    {
        private PlayerService _playerService;

        public SkillsInstaller(PlayerService playerService)
        {
            _playerService = playerService;
        }

        public void Initialize()
        {
            ComboAttack combo = new ComboAttack(_playerService);
            combo.Init();
        }
    }
}