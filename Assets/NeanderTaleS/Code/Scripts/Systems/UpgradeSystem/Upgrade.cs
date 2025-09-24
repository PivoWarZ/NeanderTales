using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem
{
    public abstract class Upgrade
    {
        public string Id => _config.ID;
        public Sprite Logo => _config.Logo;

        public string Discription => _config.Discription;
        public ReadOnlyReactiveProperty<int> Level => _level;
        public int MaxLevel => _config.MaxLevel;
        public int NextPrice => _config.GetNextPrice(Level.CurrentValue +1);
        
        private ReactiveProperty<int> _level = new();
        private UpgradeConfig _config;
        
        public bool IsMaxLevel => _level.Value == _config.MaxLevel;

        protected Upgrade(UpgradeConfig config)
        {
            _config = config;
            _level.Value = 1;
        }

        public void LevelUp()
        {
            if (IsMaxLevel)
            {
                return;
            }
            
            _level.Value++;
            OnUpgrade();
        }

        protected abstract void OnUpgrade();
    }
}