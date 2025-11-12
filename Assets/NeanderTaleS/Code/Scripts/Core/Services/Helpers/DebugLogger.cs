using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services.Helpers
{
    public class DebugLogger
    {
        private static readonly DebugLogger _instance = new DebugLogger();
        
        public static DebugLogger Instance => _instance;

        public static void PrintBinding<T>(T bind)
        {
            Debug.Log($"<color=yellow> Binding: </color><color=white>{bind.GetType().Name}</color>");
        }
    }
}