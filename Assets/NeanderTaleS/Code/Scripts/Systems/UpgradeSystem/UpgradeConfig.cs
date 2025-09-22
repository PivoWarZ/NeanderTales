using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem
{
    public abstract class UpgradeConfig: ScriptableObject
    {
        [PreviewField]
        public Sprite Logo;
        public string ID;
        public int MaxLevel;
        public UpgradePriceTable PriceTable;

        public abstract Upgrade Create();

        public int GetNextPrice(int level)
        {
            return PriceTable.GetNextPrice(level);
        }
        
        public virtual void OnValidate()
        {
            Debug.Log("OnValidate вызывается, MaxLevel = " + MaxLevel);
            PriceTable.OnValidate(MaxLevel);
        }
    }
}