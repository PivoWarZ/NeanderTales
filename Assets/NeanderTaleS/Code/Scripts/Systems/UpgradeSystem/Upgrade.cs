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
        
        private readonly ReactiveProperty<int> _level = new();
        private readonly UpgradeConfig _config;
        
        public bool IsMaxLevel => _level.Value == _config.MaxLevel;

        protected Upgrade(UpgradeConfig config)
        {
            _config = config;
            _level.Value = 1;
        }

        public bool TryLevelUp()
        {
            if (IsMaxLevel)
            {
                return false;
            }
            
            _level.Value++;
            OnUpgrade();
            
            return true;
        }

        public void Reset()
        {
            _level.Value = 1;
        }

        protected abstract void OnUpgrade();
        public abstract void Construct(GameObject player);
    }
}