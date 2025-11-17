using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Health;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Power;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Stamina;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.Experience
{
    public sealed class ExperienceSaveLoad: ISaveLoader
    {
        void ISaveLoader.LoadGame(IContext context, IGameRepository gameRepository)
        {
            
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

            var storage = context.GetService<IExperienceSetter>();
            var healthUpgrade = context.GetService<HealthUpgrade>();
            var staminaUpgrade = context.GetService<StaminaUpgrade>();
            var powerUpgrade = context.GetService<PowerUpgrade>();
            var characterUpgrade = context.GetService<CharacterUpgrade>();
            
            Upgrade(characterUpgrade, data.CharacterLevel);
            Upgrade(healthUpgrade, data.HealthUpgradeLevel);
            Upgrade(staminaUpgrade, data.StaminaUpgradeLevel);
            Upgrade(powerUpgrade, data.StaminaUpgradeLevel);
            storage.AddExperience(data.Experience);
            
            DebugLogger.PrintExperienseData(data, "Loaded", "yellow");
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
            
            DebugLogger.PrintExperienseData(data, "Saved", "green");
        }

        private void Upgrade(Upgrade upgrade, int level)
        {
            upgrade.Reset();
            
            for (int i = 0; i < level - 1; i++)
            {
                upgrade.TryLevelUp();
            }
        }
    }
}