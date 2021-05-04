using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Txc.Services
{

    public abstract class AbstractFactory<T> : IFactory<T> where T : class
    {
        protected readonly IServiceProvider serviceProvider;

        public AbstractFactory(IServiceProvider serviceProvider)
        { 
            this.serviceProvider = serviceProvider;
        }

        public abstract T CreateNew(string name);
        public TService GetInstance<TService>() where TService : class => (TService)serviceProvider.GetService(typeof(TService)) ;
        public  object GetInstance(Type type) => serviceProvider.GetService(type);

        public abstract void Register<TImplementation>(string name, LifestyleCode? lifestyle = null) where TImplementation : class, T;

        public abstract void Register<TImplementation>(Func<TImplementation> instanceCreator, string name, LifestyleCode? lifestyle = null) where TImplementation : class, T;
    }

   
}