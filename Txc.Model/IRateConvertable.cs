namespace Txc.Model
{



    public interface IRateConvertable: IOperation
    {
        public string Symbol { get; }
        public decimal BasisAmount { get; }
        public decimal GrossAmount { get; }
        public decimal Comm { get; }
        public string Currency { get; }
    }

   
}
