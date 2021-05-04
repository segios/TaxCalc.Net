using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Txc.Services.Localization
{
    public interface ILocalizer
    {
        string T<T>(string key, string[] args = null);
        string T(string key, string[] args = null);
        string T(Type type, string key, string[] args = null);
        string this[string index] { get; }
    }
}
