using System;
using Txc.Model.Trades;
using Xunit;

namespace Txc.Tests.Model
{
    public class TradeTests
    {
        
        public TradeTests()
        {
        }

        [Fact]
        public void Partial_Split_Buy()
        {
            var trade = new Trade()
            {
                Symbol = "AAA",
                Currency = "USD",
                Quantity = 20,
                Comm = -2,
                TPrice = 2M,
                Proceeds = -1 * 2M * 20,
                TradeType = TradeType.Open,
                TradeOperation = TradeOperation.Buy
            };
            
            var newTrade = trade.Split(4);

            Assert.Equal(4M, newTrade.Quantity);
            Assert.Equal(16M, trade.Quantity);

            var newComm = -1 * 2M * 4M / 20M;
            Assert.Equal(newComm, newTrade.Comm);
            Assert.Equal(-2M - newComm, trade.Comm);

            Assert.Equal(-2M, newTrade.Comm + trade.Comm);

            Assert.Equal(newTrade.Quantity * newTrade.TPrice * -1, newTrade.Proceeds);
            Assert.Equal(trade.Quantity * trade.TPrice * -1, trade.Proceeds);
        }


        [Fact]
        public void Partial_Split_Sell()
        {
            var trade = new Trade()
            {
                Symbol = "AAA",
                Currency = "USD",
                Quantity = -20,
                Comm = -2,
                TPrice = 2M,
                Proceeds = 1 * 2M * 20,
                TradeType = TradeType.Open,
                TradeOperation = TradeOperation.Sell
            };

            var newTrade = trade.Split(4);

            Assert.Equal(-4M, newTrade.Quantity);
            Assert.Equal(-16M, trade.Quantity);

            var newComm = -1 * 2M * 4M / 20M;
            Assert.Equal(newComm, newTrade.Comm);
            Assert.Equal(-2M - newComm, trade.Comm);
            Assert.Equal(-2M, newTrade.Comm + trade.Comm);

            Assert.Equal(newTrade.Quantity * newTrade.TPrice * -1, newTrade.Proceeds);
            Assert.Equal(trade.Quantity * trade.TPrice * -1, trade.Proceeds);
        }

        [Fact]
        public void BasisAmount_Should_Be_Proceed_Plus_Fee_Buy()
        {
            var trade = new Trade()
            {
                Symbol = "AAA",
                Currency = "USD",
                Quantity = 20,
                Comm = -2,
                TPrice = 2M,
                Proceeds = -2M * 20,
                TradeType = TradeType.Open,
                TradeOperation = TradeOperation.Buy
            };

            Assert.Equal(trade.Proceeds + trade.Comm, trade.BasisAmount);
        }

        [Fact]
        public void BasisAmount_Should_Be_Proceed_Plus_Fee_Sell()
        {
            var trade = new Trade()
            {
                Symbol = "AAA",
                Currency = "USD",
                Quantity = -20,
                Comm = -2,
                TPrice = 2M,
                Proceeds = 2M * 20,
                TradeType = TradeType.Open,
                TradeOperation = TradeOperation.Sell
            };

            Assert.Equal(trade.Proceeds + trade.Comm, trade.BasisAmount);
        }


    }
}
