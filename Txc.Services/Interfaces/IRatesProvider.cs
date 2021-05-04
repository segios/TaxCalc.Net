using Txc.Model;

namespace Txc.Services
{
    public interface IRatesProvider
    {
        Rates GetRates(string baseCurrency, string currency, int year);
    }
}
