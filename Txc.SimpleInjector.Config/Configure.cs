using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using System;
using Txc.Model;
using Txc.Model.Trades;
using Txc.Services;
using Txc.Services.Export;
using Txc.Services.IB;
using Txc.Services.IB.AutoMapperConfig;
using Txc.Services.IB.Mappings;
using Txc.Services.Localization;
using Txc.Services.SimpleInjector;
using Txc.Services.TaxReportServices;

namespace Txc.SimpleInjector.Config
{
    public static class Configure
    {
        public static void InitContaier(Container container, bool registerLogger = false)
        {
            string currenciesBaseFolder = AppContext.BaseDirectory;
            container.Register<IRatesProvider>(() => new FileRatesProvider(currenciesBaseFolder));
            container.Register<IRateService, RateService>();

            container.Register(typeof(IEntityConvertor<>), typeof(EntityConvertor<>));


            IStatementParserFactory statementParserFactory = new StatementParserFactory(new SimpleInjectorFactory<IStatementParser>(container));
            statementParserFactory.Register<IBStatementParser>(BrokerCode.IB.ToString());

            container.RegisterInstance<IStatementParserFactory>(statementParserFactory);

            IEntitiesDataReaderFactory entitiesDataReaderFactory = new EntitiesDataReaderFactory(new SimpleInjectorFactory<IEntitiesDataReader>(container));
            entitiesDataReaderFactory.Register<IBEntitiesDataReader>(BrokerCode.IB.ToString());

            container.RegisterInstance<IEntitiesDataReaderFactory>(entitiesDataReaderFactory);

            IBrokerParserProviderFactory brokerParserProviderFactory = new BrokerParserProviderFactory(new SimpleInjectorFactory<IBrokerParserProvider>(container));
            brokerParserProviderFactory.Register<IBBrokerParserProvider>(BrokerCode.IB.ToString());

            container.RegisterInstance<IBrokerParserProviderFactory>(brokerParserProviderFactory);

            IDataTransformerFactory dataTransformerFactory = new DataTransformerFactory(new SimpleInjectorTypeFactory(container));
            container.Register(typeof(IDataTransformer<,>), typeof(IDataTransformer<,>).Assembly);
            //container.Register<IDataTransformer<Trade, ClosedOperation<Trade>>, TradeTransformer>();
            container.RegisterInstance<IDataTransformerFactory>(dataTransformerFactory);

            IClassMappingFactory classMappingFactory = new ClassMappingFactory(new SimpleInjectorTypeFactory(container));
            container.RegisterInstance<IClassMappingFactory>(classMappingFactory);
            container.Register(typeof(IClassMapping<>), typeof(TradeMapping).Assembly);


            IFormatMappingFactory formatMappingFactory = new FormatMappingFactory(new SimpleInjectorTypeFactory(container));
            container.RegisterInstance<IFormatMappingFactory>(formatMappingFactory);
            container.Register(typeof(IFormatMapping<>), typeof(TradeFormatMapping).Assembly);


            container.Register<ILocalizer, Localizer>();

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            container.RegisterInstance<ILoggerFactory>(loggerFactory);

            LocalizationOptions localizationOptions = new LocalizationOptions()
            {
                ResourcesPath = "Localizations"
            };
            IOptions<LocalizationOptions> options = Options.Create(localizationOptions);
            container.RegisterInstance<IOptions<LocalizationOptions>>(options);
            container.RegisterSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();

            container.Register<IDataExporter, ExcelExporter>();
            container.Register<IReportCreator, ReportCreator>();

            container.Register(typeof(IEntityExtrator<>), typeof(EntityExtrator<>));
            container.Register(typeof(IEntityExtrator<,>), typeof(EntityExtrator<,>));

            container.RegisterInstance(new IBAutomapperConfig());
            container.Register<IBAutomapperFactory, IBAutomapperFactory>(Lifestyle.Singleton);

            container.Verify();
        }
    }
}
