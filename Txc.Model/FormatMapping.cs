using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model.Account;

namespace Txc.Model
{
    public interface IFormatMapping<T> 
    {
        List<(string, Func<T, decimal>)> TotalKeys { get; }
        string NameKey { get; }
        List<(string, Func<Profile, string>[], Func<T, object>, FormatOptions)> Keys { get; }
        int GetKeyIndex(string key);
        string ResolveText(string key, string value);
    }

    public interface IFormatMapping
    {
        List<(string, Func<object, decimal>)> TotalKeys { get; }
        string NameKey { get; }
        List<(string, Func<Profile, string>[], Func<object, object>, bool)> Keys { get; }
        int GetKeyIndex(string key);
    }
    //"LocalCurrency Basis"

    public record FormatOptions(bool IsLocalizable, bool HasTime) 
    {
        public FormatOptions(bool IsLocalizable, bool HasTime, double width) : this(IsLocalizable, HasTime)
        {
            this.Width = width;
        }

        public double? Width { get; set; }

        public string DateTimeFormat => HasTime ?
            $"{CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern}, {CultureInfo.CurrentUICulture.DateTimeFormat.LongTimePattern}"
            : CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
    }

    public class FormatMapping<T> : IFormatMapping<T>//, IFormatMapping
//        where T : IRateConvertable
    {
        public List<(string, Func<Profile, string>[], Func<T, object>, FormatOptions)> Keys { get; }  =
            new List<(string, Func<Profile, string>[], Func<T, object>, FormatOptions)>();

        public List<(string, Func<T, decimal>)> TotalKeys { get; } =
           new List<(string, Func<T, decimal>)>();

        public string NameKey { get; } = "_Name_";

        protected Dictionary<string, string> texts = new Dictionary<string, string>()
        {
            { "_Name_", typeof(T).Name }
        };

        public string ResolveText(string key, string value) 
        {
            return texts.ContainsKey(key) ?
                key == value ? texts[key] : value : value;
        }

        protected void AddText(string key, string value) 
        {
            texts[key] = value;
        }

        protected (string, Func<T, decimal>)[] totalKeys = null;

        protected void AddKey((string, Func<Profile, string>[], Func<T, object>, FormatOptions) key)
        {
            Keys.Add(key);
        }
        protected void AddTotalKey((string, Func<T, decimal>) totalKey)
        {
            TotalKeys.Add(totalKey);
        }

        public int GetKeyIndex(string key) 
        {
            return Keys.FindIndex(0, x => x.Item1 == key);
        }
    }
}
