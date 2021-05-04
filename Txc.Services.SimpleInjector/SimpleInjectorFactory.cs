using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Txc.Services.SimpleInjector
{

    public class SimpleInjectorFactory<T> : AbstractFactory<T>, IFactory<T> where T : class
    {
        protected readonly Container container;
        protected readonly Dictionary<string, InstanceProducer<T>> producers = new Dictionary<string, InstanceProducer<T>>(StringComparer.OrdinalIgnoreCase);

        public SimpleInjectorFactory(IServiceProvider serviceProvider): base(serviceProvider)
        { 
            this.container = (Container)serviceProvider;
        }

        public override T CreateNew(string name) => this.producers.ContainsKey(name) ? this.producers[name].GetInstance() : null;
        //public TService GetInstance<TService>() where TService : class => container.GetInstance<TService>() ;
        //public object GetInstance(Type type) => container.GetInstance(type);

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

        public override void Register<TImplementation>(string name, LifestyleCode? lifestyle = null) 
        {
            var producer = ResolveRegister(lifestyle).CreateProducer<T, TImplementation>(container);

            this.producers.Add(name, producer);
        }

        public override void Register<TImplementation>(Func<TImplementation> instanceCreator, string name, LifestyleCode? lifestyle = null) 
        {
            var producer = ResolveRegister(lifestyle).CreateProducer<T>(instanceCreator, container);

            this.producers.Add(name, producer);
        }
    }
}