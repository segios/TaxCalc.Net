
using System;

namespace Txc.Services
{
    public interface ITypeFactory 
    {
        TService GetInstance<TService>(Type type) where TService : class;
        object GetInstance(Type type);
        //protected void Register(Type type, Type implementation, LifestyleCode? lifestyle = null);

        //protected void Register<TService, TConcrete>(LifestyleCode? lifestyle = null) 
        //    where TConcrete : class, TService
        //    where TService : class;
    }

    public interface IRealTypeFactory : ITypeFactory
    {
        //TService GetInstance<TService>(Type type) where TService : class;

        void Register(Type type, Type implementation, LifestyleCode? lifestyle = null);

        void Register<TService, TConcrete>(LifestyleCode? lifestyle = null)
            where TConcrete : class, TService
            where TService : class;
    }
}