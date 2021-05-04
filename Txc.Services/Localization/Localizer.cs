using Microsoft.Extensions.Localization;
using System;
using System.Text;
using Txc.Model;


namespace Txc.Services.Localization
{
    public class Localizer: ILocalizer
    {
        private readonly IStringLocalizerFactory stringLocalizerFactory;

        public Localizer(IStringLocalizerFactory stringLocalizerFactory) 
        {
            this.stringLocalizerFactory = stringLocalizerFactory;
        }

        public string T<T>(string key, string[] args = null) 
        {
            var localizer = stringLocalizerFactory.Create(typeof(T));
            return args != null ? localizer.GetString(key, args) : localizer.GetString(key);
        }

        public string T(Type type, string key, string[] args = null) 
        {
            var localizer = stringLocalizerFactory.Create(type);
            return args != null ? localizer.GetString(key, args) : localizer.GetString(key);
        }

        public string T(string key, string[] args = null)
        {
            var localizer = stringLocalizerFactory.Create(typeof(Shared));
            return args != null ? localizer.GetString(key, args) : localizer.GetString(key);
        }


        public string this[string index] 
        {
            get
            {
                var localizer = stringLocalizerFactory.Create(typeof(Shared));
                return localizer.GetString(index);
            }
        }
    }
}
