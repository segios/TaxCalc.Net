using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Txc.Model;
using Txc.Model.Statement;
using Txc.Services.Extensions;


namespace Txc.Services
{

    public class EntityDataReader: IEntityDataReader
    {
        private readonly IClassMappingFactory classMappingFactory;

        public EntityDataReader(IClassMappingFactory classMappingFactory) 
        {
            this.classMappingFactory = classMappingFactory;
        }

        protected IList<object> ReadSection(Type type, StatementData dataContainer) 
        {
            var classMappingTypeGeneric = typeof(IClassMapping<>);
            Type[] typeArgs = { type };
            Type classMappingType = classMappingTypeGeneric.MakeGenericType(typeArgs);
            
            var classMapping = classMappingFactory.GetInstance(classMappingType) as IClassMapping;
            if (classMapping == null)
                return null;

            var result = ReadSection(type, dataContainer, classMapping.Section, classMapping);

            return result;
        }

        protected IList<T> ReadSection<T>(StatementData dataContainer) where T : new()
        {
            var classMapping = classMappingFactory.GetInstance<IClassMapping<T>, T>();
            if (classMapping == null) 
                return null;

            var result = ReadSection<T>(dataContainer, classMapping.Section, classMapping as IClassMapping);

            return result;
        }

        private IList<T> ReadSection<T>(StatementData dataContainer, string section, IClassMapping classMapping) where T : new()
        {
            var list = ReadSection(typeof(T), dataContainer, section, classMapping);

            return list?.Select(x => (T)x).ToList();
        }


        private IList<object> ReadSection(Type type, StatementData dataContainer, string section,
                        IClassMapping classMapping) 
        {

            IDictionary<string, string> fieldMapper = classMapping.FieldMappings;

            var tradeSections = dataContainer.GetSectionsByName(section);
            if (tradeSections == null || !tradeSections.Any())
            {
                return null;
            }

            IList<object> res = new List<object>();

            foreach (var tradeSection in tradeSections)
            {
                Dictionary<string, int> fieldToIndex = new Dictionary<string, int>();

                var lineIdx = 0;
                tradeSection.Lines.ForEach(tr =>
                {
                    if (tr.LineType != LineType.Data)
                    {
                        return;
                    }

                    var entity = Activator.CreateInstance(type);
                    res.Add(entity);

                    foreach (var field in fieldMapper.Keys)
                    {
                        if (!fieldToIndex.ContainsKey(field))
                        {
                            var idx = tradeSection.Headers.IndexOf(field);
                            fieldToIndex[field] = idx;
                        }

                        var val = "";
                        var index = fieldToIndex[field];
                        if (index >= 0)
                        {
                            val = tr.Fields[index];
                        }
                        else
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(val))
                        {
                            continue;
                        }

                        var propertyName = fieldMapper[field];
                        var propInfo = type.GetProperty(propertyName);
                        if (propInfo.PropertyType == typeof(string))
                        {
                            propInfo.SetValue(entity, val);
                        }
                        else
                        {
                            if (val.CanChangeType(propInfo.PropertyType))
                            {
                                object convertedValue = Convert.ChangeType(val, propInfo.PropertyType, CultureInfo.InvariantCulture);
                                propInfo.SetValue(entity, convertedValue);
                            }
                            else
                            {
                                throw new TaxCalcException($"line: {lineIdx} - field {field} value cannot be set from {val}");
                            }
                        }
                    }
                    lineIdx++;
                });
            }

            res = classMapping.Filter != null ? res?.Where(classMapping.Filter.Compile()).ToList() : res;

            return res;
        }
    }
}
