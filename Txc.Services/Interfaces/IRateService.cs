using System;

namespace Txc.Services
{
    public interface IRateService
    {
        decimal? GetRate(string baseCurrency, string currency, DateTime date);
    }
}
