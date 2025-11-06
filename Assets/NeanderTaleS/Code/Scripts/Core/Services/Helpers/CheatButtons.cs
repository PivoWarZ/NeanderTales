using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Core.Services.Helpers
{
    public class CheatButtons: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        private IExperienceStorage _experienceStorage;
        
        [Inject]
        public void Construct(IExperienceStorage experienceStorage)
        {
            _experienceStorage = experienceStorage;
        }

        [Button]
        private void AddExperience()
        {
            _experienceStorage.AddExperience(500f);
        }
    }
}