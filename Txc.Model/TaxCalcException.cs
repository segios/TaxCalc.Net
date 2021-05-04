using System;

namespace Txc.Model
{
    public class TaxCalcException : Exception
    {
        public TaxCalcException() { }
        public TaxCalcException(string msg): base(msg) { }
        public TaxCalcException(string msg, Exception innerEx) : base(msg, innerEx) { }
    }
}
