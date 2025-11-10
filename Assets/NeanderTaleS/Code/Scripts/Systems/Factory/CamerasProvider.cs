using Unity.Cinemachine;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Factory
{
    public class CamerasProvider
    {
        private readonly Camera _camera;
        private readonly CinemachineCamera _cinemashine;

        public CamerasProvider(Camera camera, CinemachineCamera cinemashine)
        {
            _camera = camera;
            _cinemashine = cinemashine;
        }

        public Camera Camera => _camera;

        public CinemachineCamera Cinemashine => _cinemashine;

        public void Initialize(GameObject player)
        {
            _cinemashine.GetComponent<CinemachineCamera>().Follow = player.transform;
        }
    }
}