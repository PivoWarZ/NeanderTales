using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Factory
{
    public interface IPlayerCreator
    {
        void CreatePlayer(Vector3 position);
    }
}