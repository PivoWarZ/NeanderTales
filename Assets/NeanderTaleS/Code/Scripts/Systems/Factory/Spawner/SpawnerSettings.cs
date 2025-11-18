using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Factory.Spawner
{
    [Serializable]
    public struct SpawnerSettings
    {
        public Transform WorldTransform;
        public List<Transform> SpawnPoints;
        public int Level;
        public float MinSize;
        public float MaxSize;
    }
}