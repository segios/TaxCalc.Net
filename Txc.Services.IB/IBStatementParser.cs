using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Txc.Model.Statement;

namespace Txc.Services.IB
{

    public class IBStatementParser : IStatementParser
    {
        public StatementData ParseStatement(Stream stream)
        {
            var res = new StatementData();
            using (TextFieldParser parser = new TextFieldParser(stream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                Section currentSection = null;
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    
                    if (fields.Length < 2) 
                    {
                        continue;
                    }

                    if (currentSection == null || currentSection.Name != fields[0] || fields[1] == LineType.Header.ToString())
                    {
                        currentSection = new Section()
                        {
                            Name = fields[0],
                            Headers = fields[1] == LineType.Header.ToString() ? fields.Skip(2).ToList() : null
                        };
                        res.Sections.Add(currentSection);
                    }
                    else
                    {
                        var line = new Line(fields[1], fields.Skip(2).ToArray());
                        currentSection.Lines.Add(line);
                    }
                }
            }
            return res;
        }

        public StatementData ParseStatement(string filePath) 
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return ParseStatement(fs);
            }
        }

        public StatementData ParseStatement(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return ParseStatement(ms);
            }
        }
        
    }
}
