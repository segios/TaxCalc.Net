using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Txc.Services
{

    public class BaseFactory<T> : IFactory<T> where T : class
    {
        private readonly IFactory<T> concreateFactory;

        public BaseFactory(IFactory<T> concreateFactory) 
        {
            this.concreateFactory = concreateFactory;
        }

        public T CreateNew(string name)
        {
            return concreateFactory.CreateNew(name);
        }

        public TService GetInstance<TService>() where TService : class
        {
            return concreateFactory.GetInstance<TService>();
        }

        public object GetInstance(Type type)
        {
            return concreateFactory.GetInstance(type);
        }

        public void Register<TImplementation>(string name, LifestyleCode? lifestyle) where TImplementation : class, T
        {
            concreateFactory.Register<TImplementation>(name, lifestyle);
        }

        public void Register<TImplementation>(Func<TImplementation> instanceCreator, string name, LifestyleCode? lifestyle) where TImplementation : class, T
        {
            concreateFactory.Register<TImplementation>(instanceCreator, name, lifestyle);
        }
    }
}