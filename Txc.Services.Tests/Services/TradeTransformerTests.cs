using System;
using System.Linq;
using System.Collections.Generic;
using Txc.Model.Trades;
using Xunit;
using Txc.Services;

namespace Txc.Tests.Services
{
    public class TradeTransformerTests
    {
        public TradeTransformerTests()
        {
        }

        [Fact]
        public void Trade_One_Closed()
        {
            List<Trade> entities = new List<Trade>() { 
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 2, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell }
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Equal(1, result.Count);
            
            var closeOperation = result.First();

            Assert.True(closeOperation.NoOpenTrade);

            Assert.True(closeOperation.IsClosed);
        }

        [Fact]
        public void Trade_One_Open_One_Closed()
        {
            List<Trade> entities = new List<Trade>() {
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 2, TradeType = TradeType.Open, TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 2, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell }
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Single(result);

            var closeOperation = result.First();

            Assert.Single(closeOperation.OpenTrades);

            Assert.True(closeOperation.IsClosed);
        }

        [Fact]
        public void Trade_Two_Open_One_Closed()
        {
            List<Trade> entities = new List<Trade>() {
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 1, TradeType = TradeType.Open, TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 1, TradeType = TradeType.Open, TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 2, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell }
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Single(result);

            var closeOperation = result.First();

            Assert.Equal(2, closeOperation.OpenTrades.Count);

            Assert.True(closeOperation.IsClosed);
        }

        [Fact]
        public void Trade_One_Open_One_Closed_Less()
        {
            List<Trade> entities = new List<Trade>() {
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 20, Comm=1.5M, TradeType = TradeType.Open, TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell }
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Single(result);

            var closeOperation = result.First();

            Assert.True(closeOperation.IsClosed);
            Assert.Single(closeOperation.OpenTrades);

            var openTrade = closeOperation.OpenTrades.First();
            Assert.Equal(5, openTrade.Quantity);
            Assert.Equal(1.5M / 4, openTrade.Comm);
        }

        [Fact]
        public void Trade_One_Open_Two_Closed()
        {
            List<Trade> entities = new List<Trade>() {
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 20, Comm=1.5M, TradeType = TradeType.Open, TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = -4, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = -16, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Equal(2, result.Count);

            var closeOperation1 = result.First();

            Assert.True(closeOperation1.IsClosed);
            Assert.Single(closeOperation1.OpenTrades);

            var openTrade1 = closeOperation1.OpenTrades.First();
            Assert.Equal(4, openTrade1.Quantity);
            Assert.Equal(1.5M * 4M / 20M, openTrade1.Comm);

            var closeOperation2 = result.Skip(1).Take(1).First();

            Assert.True(closeOperation2.IsClosed);
            Assert.Single(closeOperation2.OpenTrades);

            var openTrade2 = closeOperation2.OpenTrades.First();
            Assert.Equal(16, openTrade2.Quantity);
            Assert.Equal((1.5M - 1.5M * (4M / 20M)) * (16M / 16M), openTrade2.Comm);
        }

        [Fact]
        public void Trade_One_Open_Two_Closed_Sell()
        {
            List<Trade> entities = new List<Trade>() {
                new Trade(){ Symbol = "AAA", Currency = "USD", 
                    Quantity = -500, 
                    Comm=-1M, 
                    TradeType = TradeType.Open, 
                    TradeOperation = TradeOperation.Sell },
                new Trade(){ Symbol = "AAA", 
                    Currency = "USD", 
                    Quantity = 100, 
                    Comm=-1M, 
                    TradeType = TradeType.Close, 
                    TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", 
                    Currency = "USD", 
                    Quantity = 400, 
                    Comm=-1M, TradeType = TradeType.Close, 
                    TradeOperation = TradeOperation.Buy },
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Equal(2, result.Count);

            var closeOperation1 = result.First();

            Assert.True(closeOperation1.IsClosed);
            Assert.Single(closeOperation1.OpenTrades);

            var openTrade1 = closeOperation1.OpenTrades.First();
            Assert.Equal(-100, openTrade1.Quantity);
            var comm1 = -1M * 100M / 500M;
            Assert.Equal(comm1, openTrade1.Comm);

            var closeOperation2 = result.Skip(1).Take(1).First();

            Assert.True(closeOperation2.IsClosed);
            Assert.Single(closeOperation2.OpenTrades);

            var openTrade2 = closeOperation2.OpenTrades.First();
            Assert.Equal(-400, openTrade2.Quantity);
            Assert.Equal((-1M - comm1) * (400M / 400M), openTrade2.Comm);
        }

        [Fact]
        public void Trade_One_Open_Three_Closed()
        {
            List<Trade> entities = new List<Trade>() {
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 20, Comm=1.5M, TradeType = TradeType.Open, TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Equal(3, result.Count);

            var closeOperation1 = result.First();

            Assert.True(closeOperation1.IsClosed);
            Assert.Single(closeOperation1.OpenTrades);

            var openTrade1 = closeOperation1.OpenTrades.First();
            Assert.Equal(5, openTrade1.Quantity);
            Assert.Equal(1.5M * 5M / 20M, openTrade1.Comm);

            // 2 trade
            var closeOperation2 = result.Skip(1).Take(1).First();

            Assert.True(closeOperation2.IsClosed);
            Assert.Single(closeOperation2.OpenTrades);

            var openTrade2 = closeOperation2.OpenTrades.First();
            Assert.Equal(5, openTrade2.Quantity);
            Assert.Equal((1.5M - openTrade1.Comm) * (5M / 15M), openTrade2.Comm);

            // 3 trade
            var closeOperation3 = result.Skip(2).Take(1).First();

            Assert.True(closeOperation3.IsClosed);
            Assert.Single(closeOperation3.OpenTrades);

            var openTrade3 = closeOperation3.OpenTrades.First();
            Assert.Equal(5, openTrade3.Quantity);
            Assert.Equal((1.5M - openTrade2.Comm - openTrade1.Comm) * (5M / 10M), openTrade3.Comm);

        }

        [Fact]
        public void Trade_One_Open_Four_Closed()
        {
            List<Trade> entities = new List<Trade>() {
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 20, Comm=1.5M, TradeType = TradeType.Open, TradeOperation = TradeOperation.Buy },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
                new Trade(){ Symbol = "AAA", Currency = "USD", Quantity = 5, Comm=1.5M, TradeType = TradeType.Close, TradeOperation = TradeOperation.Sell },
            };

            var tradeTransformer = new TradeTransformer();

            var result = tradeTransformer.Transform(entities);

            Assert.Equal(4, result.Count);

            var closeOperation1 = result.First();

            Assert.True(closeOperation1.IsClosed);
            Assert.Single(closeOperation1.OpenTrades);

            var openTrade1 = closeOperation1.OpenTrades.First();
            Assert.Equal(5, openTrade1.Quantity);
            Assert.Equal(1.5M * 5M / 20M, openTrade1.Comm);

            // 2 trade
            var closeOperation2 = result.Skip(1).Take(1).First();

            Assert.True(closeOperation2.IsClosed);
            Assert.Single(closeOperation2.OpenTrades);

            var openTrade2 = closeOperation2.OpenTrades.First();
            Assert.Equal(5, openTrade2.Quantity);
            Assert.Equal((1.5M - openTrade1.Comm) * (5M / 15M), openTrade2.Comm);

            // 3 trade
            var closeOperation3 = result.Skip(2).Take(1).First();

            Assert.True(closeOperation3.IsClosed);
            Assert.Single(closeOperation3.OpenTrades);

            var openTrade3 = closeOperation3.OpenTrades.First();
            Assert.Equal(5, openTrade3.Quantity);
            Assert.Equal((1.5M - openTrade2.Comm - openTrade1.Comm) * (5M / 10M), openTrade3.Comm);

            // 4 trade
            var closeOperation4 = result.Skip(2).Take(1).First();

            Assert.True(closeOperation4.IsClosed);
            Assert.Single(closeOperation4.OpenTrades);

            var openTrade4 = closeOperation4.OpenTrades.First();
            Assert.Equal(5, openTrade4.Quantity);
            Assert.Equal((1.5M - openTrade3.Comm - openTrade2.Comm - openTrade1.Comm) * (5M / 5M), openTrade4.Comm);

            Assert.Equal(1.5M, openTrade4.Comm + openTrade3.Comm + openTrade2.Comm + openTrade1.Comm);

        }
    }
}
