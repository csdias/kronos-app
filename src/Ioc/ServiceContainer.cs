using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace Ioc
{
    public class ServiceContainer
    {
        private readonly Container container;

        internal ServiceContainer(Container container)
        {
            this.container = container;
        }

        public void ExecuteInContainerScope<TService>(Action<TService> action) where TService : class
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var service = container.GetInstance<TService>();
                action(service);
            }
        }

        public void WinServiceRegister()
        {
        }
    }

}

