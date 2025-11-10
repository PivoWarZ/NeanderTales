using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    public sealed class StatsUpgradesInstaller
    {
        private readonly List<Upgrade> _upgrades = new();
        private PlayerService _servise;
        private IEventBus _eventBus;

        public StatsUpgradesInstaller(Upgrade[] upgrades, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _upgrades = upgrades.ToList();
        }
        
        public void Initialize(GameObject player)
        {
            for (var index = 0; index < _upgrades.Count; index++)
            {
                var upgrade = _upgrades[index];
                
                if (upgrade is IUpgradeSystemConstruct construct)
                {
                    construct.Construct(player);
                }
            }
        }

        public List<Upgrade> Upgrades => _upgrades;
    }
}