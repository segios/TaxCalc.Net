using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Txc.Model.Statement
{
    public class StatementData
    {
        public List<Section> Sections { get; set; } = new List<Section>();

        public IEnumerable<Section> GetSectionsByName(string name) 
        {
            return Sections.Where(x => x.Name == name);
        }
    }
}
