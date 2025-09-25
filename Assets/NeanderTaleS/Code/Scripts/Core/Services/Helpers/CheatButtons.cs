using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services.Helpers
{
    public class CheatButtons: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        [ShowInInspector] private IExperienceStorage _experienceStorage;

        private void AddExperience()
        {
            _experienceStorage.AddExperience(500f);
        }
    }
}