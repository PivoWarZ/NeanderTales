using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.ExperienceSystem;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;

namespace NeanderTaleS.Code.Scripts.Systems.CoinsUpgradeSystem
{
    public class CoinsUpgradeSystem
    {
        private ICoinsStorage _storage;
        private IEventBus _eventBus;

        public CoinsUpgradeSystem(ICoinsStorage storage, IContext context)
        {
            _storage = storage;
            _eventBus = context.GetService<IEventBus>();
        }
        
        
    }
}