using System;

namespace Txc.Model
{

    public class ConvertedEntity<T>: IConvertedEntity, IValidable, IRateConvertable
        where T : IRateConvertable
    {
        protected ConvertedEntity()
        {
        }

        public ConvertedEntity(T entity, string convertCurrency, decimal exchangeRate)
        {
            ConvertCurrency = convertCurrency;
            Entity = entity;
            ExchangeRate = exchangeRate;
        }

        public T Entity { get; set; }
        public string ConvertCurrency { get; set; }
        public decimal ExchangeRate { get; set; }

        public decimal ConvertedBasis
        {
            get
            {
                return Math.Round(BasisAmount * ExchangeRate, 2);
            }
        }
        public decimal ConvertedComm 
        {
            get
            {
                return Math.Round(Comm * ExchangeRate, 2);
            }
        }

        public decimal ConvertedGrossAmount 
        {
            get
            {
                return Math.Round(GrossAmount * ExchangeRate, 2);
            }
        }

        public string Symbol => Entity.Symbol;
        public decimal BasisAmount => Entity.BasisAmount;

        public decimal GrossAmount => Entity.GrossAmount;

        public decimal Comm => Entity.Comm;

        public string Currency => Entity.Currency;

        public DateTime OperationDate => Entity.OperationDate;


        // IValidable
        public bool IsValid { get; set; } = true;
        public string Error { get; set; }
    }
}
