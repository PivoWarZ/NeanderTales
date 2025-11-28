using TMPro;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.EquipPopup
{
    public sealed class StatsView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _hitPoints;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _dexterity;
        [SerializeField] private TMP_Text _armor;

        public void Init (StatsStruct stats)
        {
            _hitPoints.text = stats.Health;
            _damage.text = stats.Damage;
            _dexterity.text = stats.Dexterity;
            _armor.text = stats.Armor;
        }
    }
}