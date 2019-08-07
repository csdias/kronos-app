using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace Ioc
{
    public class HostConfigurator
    {
        public static ServiceContainer ConfigWinService(Action<ServiceContainer> cfg)
        {
            var container = new Container();
            container.Options.DefaultLifestyle = Lifestyle.Scoped;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var svcContainer = new ServiceContainer(container);
            cfg(svcContainer);

#if DEBUG
            container.Verify();
#endif

            return svcContainer;
        }

    }

}

