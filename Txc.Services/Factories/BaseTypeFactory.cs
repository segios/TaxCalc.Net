using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Txc.Services
{

    public class BaseTypeFactory: ITypeFactory
    {
        private readonly IRealTypeFactory concreateFactory;

        public BaseTypeFactory(IRealTypeFactory concreateFactory) 
        {
            this.concreateFactory = concreateFactory;
        }

        public TService GetInstance<TService>(Type type) where TService : class 
        {
            return concreateFactory.GetInstance<TService>(type);
        }

        public object GetInstance(Type type)
        {
            return concreateFactory.GetInstance(type);
        }

        void Register(Type type, Type implementation, LifestyleCode? lifestyle = null)  
        {
            concreateFactory.Register(type, implementation, lifestyle);

        }

        void Register<TService, TConcrete>(LifestyleCode? lifestyle = null)
            where TConcrete : class, TService
            where TService : class
        {
            concreateFactory.Register<TService, TConcrete>(lifestyle);
        }

        
    }
}