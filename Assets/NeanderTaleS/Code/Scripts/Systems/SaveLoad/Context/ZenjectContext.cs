using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context
{
    public sealed class ZenjectContext: IContext
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