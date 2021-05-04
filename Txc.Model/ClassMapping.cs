using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Txc.Model
{
    public interface IClassMapping<T>  
    {
        Dictionary<string, string> FieldMappings { get; }
        string Section { get; }
        Expression<Func<object, bool>> Filter { get; }
    }

    public interface IClassMapping
    {
        Dictionary<string, string> FieldMappings { get; }
        string Section { get; }
        Expression<Func<object, bool>>  Filter { get; }
    }

    public class ClassMapping<T> : IClassMapping<T>, IClassMapping where T : class, new()
    {
        protected Dictionary<string, string> fieldMappings = new Dictionary<string, string>();
        protected string section;
        protected Expression<Func<object, bool>> filter;

        protected ClassMapping<T> SetFilter(Expression<Func<object, bool>> filter)
        {
            this.filter = filter;
            //filter1 = new Expression(filter);
            return this;
        }

        protected ClassMapping<T> Map(string field, string property)
        {
            FieldMappings.Add(field, property);
            return this;
        }

        protected ClassMapping<T> MapSection(string section)
        {
            this.section = section;
            return this;
        }

        public Dictionary<string, string> FieldMappings 
        {
            get 
            { 
                return fieldMappings; 
            }
        }

        public string Section
        {
            get
            {
                return section;
            }
        }
        public Expression<Func<object, bool>> Filter
        {
            get
            {
                return filter;
            }
        }

    }

}
