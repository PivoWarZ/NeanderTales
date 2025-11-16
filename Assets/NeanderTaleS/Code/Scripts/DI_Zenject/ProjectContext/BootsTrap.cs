using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class BootsTrap: MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> _installers;
        

        private void Awake()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}