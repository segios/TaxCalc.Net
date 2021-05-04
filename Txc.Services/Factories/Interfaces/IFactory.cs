
using System;

namespace Txc.Services
{

    public interface IFactory<T> where T : class
    {
        T CreateNew(string name);
        TService GetInstance<TService>() where TService : class;
        object GetInstance(Type type);

        void Register<TImplementation>(string name, LifestyleCode? lifestyle = null) where TImplementation : class, T;

        void Register<TImplementation>(Func<TImplementation> instanceCreator, string name, LifestyleCode? lifestyle = null) where TImplementation : class, T;
    }
}