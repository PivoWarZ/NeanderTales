using Zenject;

namespace Inventory.Scripts.DI
{
    public class ZenjectContext: IContext
    {
        private readonly DiContainer _container;

        public ZenjectContext(DiContainer container)
        { 
            _container = container;
        }
        
        T IContext.GetService<T>()
        {
            return _container.Resolve<T>();
        }
    }
}