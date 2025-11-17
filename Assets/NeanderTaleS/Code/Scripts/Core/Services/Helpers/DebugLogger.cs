using NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.Experience;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services.Helpers
{
    public sealed class DebugLogger
    {
        private static readonly DebugLogger _instance = new DebugLogger();
        
        public static DebugLogger Instance => _instance;

        public static void PrintBinding<T>(T bind)
        {
            Debug.Log($"<color=yellow> Binding: </color><color=white>{bind.GetType().Name}</color>");
        }

        public static void PrintExperienseData(ExperienceData data, string saveOrLoad, string color)
        {
            Debug.Log($"<color={color}>Experience Data {saveOrLoad}:</color>");
            Debug.Log($"<color={color}>Character Level: {data.CharacterLevel}</color>");
            Debug.Log($"<color={color}>Health Level: {data.HealthUpgradeLevel}</color>");
            Debug.Log($"<color={color}>Stamina Level: {data.StaminaUpgradeLevel}</color>");
            Debug.Log($"<color={color}>PowerUpgrade Level: {data.PowerUpgradeLevel}</color>");
            Debug.Log($"<color={color}>Experience : {data.Experience}</color>");
        }
    }
}