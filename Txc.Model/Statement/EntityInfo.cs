using System.Collections.Generic;

namespace Txc.Model.Statement
{
    public class EntityInfo
    {
        public EntityInfo(string section)
        {
            Section = section;
        }
        public string Section { get; set; }
        public IDictionary<string, string> FieldMapper { get; set; }

    }
}
