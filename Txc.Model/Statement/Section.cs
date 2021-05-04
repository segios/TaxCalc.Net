using System.Collections.Generic;

namespace Txc.Model.Statement
{
    public class Section
    {
        public string Name { get; set; }
       
        public List<string> Headers { get; set; }
        public List<Line> Lines { get; set; } = new List<Line>();
    }
}
