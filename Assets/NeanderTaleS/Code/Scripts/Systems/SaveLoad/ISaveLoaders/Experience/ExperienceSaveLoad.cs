using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Health;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Stamina;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience
{
    public class ExperienceSaveLoad: ISaveLoader
    {
        void ISaveLoader.LoadGame(IContext context, IGameRepository gameRepository)
        {
            var playerUpgrade = context.GetService<ICharacterUpgrade>();
            
            playerUpgrade.ResetValues();
            
            bool isDataFound = gameRepository.TryGetData<ExperienceDataStorage>(out var dataStorage);
            ExperienceData data = new ExperienceData();

            if (isDataFound)
            {
                data.Experience = dataStorage.Data.Experience;
                data.CharacterLevel = dataStorage.Data.CharacterLevel;
                data.HealthUpgradeLevel = dataStorage.Data.HealthUpgradeLevel;
                data.StaminaUpgradeLevel = dataStorage.Data.StaminaUpgradeLevel;
                data.PowerUpgradeLevel = dataStorage.Data.PowerUpgradeLevel;
            }
            else
            {
                Debug.Log($"<color=red>Load Game Error: {GetType().Name} DATA not found</color>");
                return;
            }

            var storage = context.GetService<IExperienceStorage>();
            var healthUpgrade = context.GetService<HealthUpgrade>();
            var staminaUpgrade = context.GetService<StaminaUpgrade>();
            var powerUpgrade = context.GetService<PowerUpgrade>();
            var characterUpgrade = context.GetService<CharacterUpgrade>();
            
            Upgrade(characterUpgrade, data.CharacterLevel);
            Upgrade(healthUpgrade, data.HealthUpgradeLevel);
            Upgrade(staminaUpgrade, data.StaminaUpgradeLevel);
            Upgrade(powerUpgrade, data.StaminaUpgradeLevel);
            storage.AddExperience(data.Experience);
            
            Debug.Log($"<color=yellow>Experience Data Loaded:</color>");
            Debug.Log($"<color=yellow>Character Level: {data.CharacterLevel}</color>");
            Debug.Log($"<color=yellow>Health Level: {data.HealthUpgradeLevel}</color>");
            Debug.Log($"<color=yellow>Stamina Level: {data.StaminaUpgradeLevel}</color>");
            Debug.Log($"<color=yellow>PowerUpgrade Level: {data.PowerUpgradeLevel}</color>");
            Debug.Log($"<color=yellow>Experience : {data.Experience}</color>");
        }

        void ISaveLoader.SaveGame(IContext context, IGameRepository gameRepository)
        {
            var experienseStorage = context.GetService<ExperienceStorage>();
            var experience = experienseStorage.CurrentExperience.CurrentValue;
            
            var characterUpgrade = context.GetService<CharacterUpgrade>();
            var characterLevel = characterUpgrade.Level.CurrentValue;
            
            var healthUpgrade = context.GetService<HealthUpgrade>();
            var healthLevel = healthUpgrade.Level.CurrentValue;
            
            var staminaUpgrade = context.GetService<StaminaUpgrade>();
            var staminaLevel = staminaUpgrade.Level.CurrentValue;
            
            var powerUpgrade = context.GetService<PowerUpgrade>();
            var powerLevel = powerUpgrade.Level.CurrentValue;

            ExperienceData data = new ExperienceData
            {
                Experience = experience,
                CharacterLevel = characterLevel,
                HealthUpgradeLevel = healthLevel,
                StaminaUpgradeLevel = staminaLevel,
                PowerUpgradeLevel = powerLevel
            };

            ExperienceDataStorage saveData = new ExperienceDataStorage(data);
            
            gameRepository.SaveData(saveData);
            Debug.Log($"<color=green>Experience Data Saved:</color>");
            Debug.Log($"<color=green>Character Level: {characterLevel}</color>");
            Debug.Log($"<color=green>Experience: {experience}</color>");
            Debug.Log($"<color=green>Health Level: {healthLevel}</color>");
            Debug.Log($"<color=green>Stamina Level: {staminaLevel}</color>");
            Debug.Log($"<color=green>Power Level: {powerLevel}</color>");
        }

        private void Upgrade(Upgrade upgrade, int level)
        {
            upgrade.Reset();
            
            for (int i = 0; i < level - 1; i++)
            {
                upgrade.LevelUp();
            }
        }
    }
}