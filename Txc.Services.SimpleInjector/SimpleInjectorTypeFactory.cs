using SimpleInjector;
using System;

namespace Txc.Services.SimpleInjector
{

    public class SimpleInjectorTypeFactory : IRealTypeFactory
    {
        protected readonly Container container;

        public SimpleInjectorTypeFactory(IServiceProvider serviceProvider)
        { 
            this.container = (Container)serviceProvider;
        }


        //        public TService GetInstance<TService>() where TService : class => container.GetInstance<TService>() ;
        public object GetInstance(Type type)
                  => container.GetInstance(type);

        public TService GetInstance<TService>(Type type) 
            where TService : class 
          => container.GetInstance(type) as TService;

        private Lifestyle ResolveRegister(LifestyleCode? lifestyle)
        {
            if (lifestyle == null)
                return container.Options.DefaultLifestyle;

            switch (lifestyle.Value) {
                case LifestyleCode.Scoped:
                    return Lifestyle.Scoped;
                case LifestyleCode.Singleton:
                    return Lifestyle.Singleton;
                case LifestyleCode.Transient:
                    return Lifestyle.Transient;
                default:
                    return container.Options.DefaultLifestyle;
            }
        }

        public void Register(Type type, Type implementation, LifestyleCode? lifestyle = null) 
        {
            container.Register(type, implementation, ResolveRegister(lifestyle));
        }
        public void Register<TService, TConcrete>(LifestyleCode? lifestyle = null)
            where TConcrete : class, TService
            where TService : class
        {
            container.Register<TService, TConcrete>(ResolveRegister(lifestyle));
        }

    }
}