using TMPro;
using UnityEngine.UI;

namespace NeanderTaleS.Code.UIScripts
{
    public interface IPlayerStatsView
    {
        Image Portrait { get; set; }
        TMP_Text Name { get; set; }
        TMP_Text Level { get; set; }
        Slider Health { get; set; }
        Slider Stamina { get; set; }
        Slider Experience { get; set; }
        Button LevelUpMarker { get; set; }
    }
}