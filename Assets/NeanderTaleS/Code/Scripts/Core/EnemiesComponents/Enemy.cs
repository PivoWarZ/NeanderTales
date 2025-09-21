using NeanderTaleS.Code.Configs;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.EnemySkills;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class Enemy: MonoBehaviour
    {
        [SerializeField] private EntityBootsTrap _bootsTrap;
        [SerializeField] private LocalProvider _provider;
        [SerializeField] private VelociraptorConfig _config;

        private void Awake()
        {
            _bootsTrap.EntityInitialize();
            InitializeLeapSkill(_provider);
           // Initialize(_config);
        }
        
        private void InitializeLeapSkill(LocalProvider localProvider)
        {
            var leapSkill = localProvider.GetService<LeapRaptorSkill>();
            leapSkill.Init();
        }

        private void Initialize(VelociraptorConfig config)
        {
            var setters = _provider.GetInterfaces<IStartValueSetter>();

            foreach (var startValueSetter in setters)
            {
                startValueSetter.SetStartValue(config.HitPoints, config.HitPoints);
            }
        }
    }
}