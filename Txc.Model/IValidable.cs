namespace Txc.Model
{
    public interface IValidable
    {
        bool IsValid { get; set; }
        string Error { get; set; }
    }

}
