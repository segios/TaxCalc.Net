using System;

namespace Txc.Model.Statement
{
    public class Line
    {
        public Line(LineType lineType, string[] fields)
        {
            LineType = lineType;
            Fields = fields;
        }
        public Line(string lineType, string[] fields)
        {
            OriginalLineType = lineType;
            LineType lineTypeRes;
            
            if (Enum.TryParse<LineType>(lineType, out lineTypeRes))
            {
                LineType = lineTypeRes;
            }
            else
            {
                LineType = LineType.Unparsed;
            }
            Fields = fields;
        }
        public LineType LineType { get; set; }
        public string OriginalLineType { get; set; }
        public string[] Fields { get; set; }
    }
}
