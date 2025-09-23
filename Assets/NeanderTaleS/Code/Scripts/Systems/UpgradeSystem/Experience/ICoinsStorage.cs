using R3;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience
{
    public interface ICoinsStorage
    {
        ReactiveProperty<int> Coins { get; set; }
    }
}