using System;
using Txc.Services;
using Xunit;

namespace Txc.Tests.Services
{
    public class RateServiceTests
    {
        RateService rateService;
        public RateServiceTests()
        {
            var dir = AppContext.BaseDirectory;
            rateService = new RateService(new FileRatesProvider(dir));
        }

        [Fact]
        public void Get_Existed_Rate()
        {
            var rate = rateService.GetRate("USD", "UAH", new DateTime(2017, 1, 1));
            Assert.Equal(27.190858M, rate);
        }

        [Fact]
        public void Get_NonExisted_Rate()
        {
            var rate = rateService.GetRate("USD", "UAH", new DateTime(2016, 1, 1));
            Assert.Null(rate);
        }
    }
}
