namespace Txc.Model
{
    public interface IConvertedEntity
    {
        decimal ConvertedBasis { get; }
        decimal ConvertedComm { get; }
        decimal ConvertedGrossAmount { get; }
    }
    
}
