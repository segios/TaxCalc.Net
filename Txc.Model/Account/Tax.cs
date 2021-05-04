using System;

namespace Txc.Model.Account
{
    public class Tax
    {
        public Tax()
        {
        }
        public Tax(string name, decimal percent)
        {
            Name = name;
            Percent = percent;
        }

        public Tax(string code, string name, decimal percent)
        {
            Code = code;
            Name = name;
            Percent = percent;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Percent { get; set; }

        public decimal ApplyTax(decimal val) 
        {
            return Math.Round(val / 100 * Percent, 2);
        }
    }
}
