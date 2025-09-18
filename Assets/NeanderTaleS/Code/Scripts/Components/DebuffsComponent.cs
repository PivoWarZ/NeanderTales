using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class DebuffsComponent: MonoBehaviour
    {
        [SerializeField] public SerializableReactiveProperty<bool> Pushing = new(false);

        public bool IsStun()
        {
            return Pushing.Value;
        }
    }
}