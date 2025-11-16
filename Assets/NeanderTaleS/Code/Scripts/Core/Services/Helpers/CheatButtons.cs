using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Core.Services.Helpers
{
    public sealed class CheatButtons: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        private ExperienceManager _experienceManager;
        
        [Inject]
        public void Construct(ExperienceManager experienceManager)
        {
            _experienceManager = experienceManager;
        }

        [Button]
        private void AddExperience()
        {
            _experienceManager.AddExpFromCheatButton(100);
        }
    }
}