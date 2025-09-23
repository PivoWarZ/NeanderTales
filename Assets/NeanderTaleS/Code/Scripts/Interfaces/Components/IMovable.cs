using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IMovable
    {
        void Move(Vector3 direction);
        void SetSpeed(float speed);
    }
}