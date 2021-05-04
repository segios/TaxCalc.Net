using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model;
using Txc.Model.Account;
using Txc.Services.Localization;
using Txc.Model.Extensions;
using System.Globalization;
using Txc.Model.Trades;
using Txc.Model.Dividends;
using Txc.Services.TaxReportServices;

namespace Txc.Services.Export
{
    public class ExcelExporter : IDataExporter
    {
        private ILocalizer localizer;
        private readonly IFormatMappingFactory formatMappingFactory;
        private ExcelDataFromatter excelDataFromatter ;
        public ExcelExporter(ILocalizer localizer, IFormatMappingFactory formatMappingFactory)
        {
            this.localizer = localizer;
            this.formatMappingFactory = formatMappingFactory;
            excelDataFromatter = new ExcelDataFromatter(localizer);
        }

        public void Export(Stream stream, Profile profile, ITaxReport taxReport)
        {

            var currentCulture = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = new CultureInfo(profile.CultureCode, false);

                using (var p = new ExcelPackage())
                {

                    if (taxReport.TradeData.TradeAggregations.Any())
                    {
                        (ExcelWorksheet ws, int row) = ExportEntities(p, profile, taxReport.TradeData.TradeAggregations);
                        ExportTaxes(ws, row, taxReport.CalcTradesTaxes());
                    }

                    if (taxReport.TradeData.Trades.Any())
                    {
                        ExportEntities(p, profile, taxReport.TradeData.FlatConvertedTrades);
                    }

                    if (taxReport.ForexTrades.Any())
                    {
                        ExportEntities(p, profile, taxReport.ForexTrades);
                    }

                    if (taxReport.Dividends.Any())
                    {
                        (ExcelWorksheet ws, int row) = ExportEntities(p, profile, taxReport.Dividends);
                        ExportTaxes(ws, row, taxReport.CalcDividendsTaxes());
                    }
                    
                    if (taxReport.SecuritiesLentInterests.Any())
                    {
                        ExportEntities(p, profile, taxReport.SecuritiesLentInterests);
                    }

                    if (taxReport.Interests.Any())
                    {
                        (ExcelWorksheet ws, int row) = ExportEntities(p, profile, taxReport.Interests);
                        ExportTaxes(ws, row, taxReport.CalcInterestsTaxes());
                    }

                    if (taxReport.Fees.Any())
                    {
                        ExportEntities(p, profile, taxReport.Fees);
                    }

                    if (taxReport.Deposits.Any())
                    {
                        ExportEntities(p, profile, taxReport.Deposits);
                    }

                    p.SaveAs(stream);
                }
            }
            finally 
            {
                CultureInfo.CurrentUICulture = currentCulture;
            }
        }

        private void ExportTaxes(ExcelWorksheet ws, int row, IEnumerable<(Tax tax, decimal taxValue)> taxData)
        {
            excelDataFromatter.EmptyLine(ref row);
            foreach (var taxResult in taxData)
            {
                (Tax tax, decimal taxValue) = taxResult;
                excelDataFromatter.WriteData<string>(ws, row, 1, tax.Name);
                excelDataFromatter.WriteData<decimal>(ws, row, 2, tax.Percent);
                //ws.Cells[row, 2].Style.Numberformat.Format = "#0\\.00%";
                excelDataFromatter.WriteData<decimal>(ws, row, 3, taxValue);
                row++;
            }
        }

        private (ExcelWorksheet, int) ExportEntities<T>(ExcelPackage excelPackage, 
            Profile profile, 
            IEnumerable<T> entities)
        {
            IFormatMapping<T> formatMapper = formatMappingFactory.GetInstance<IFormatMapping<T>, T>();

            var tabName = formatMapper.ResolveText(formatMapper.NameKey, localizer.T<T>(formatMapper.NameKey));

            var ws = excelPackage.Workbook.Worksheets.Add(tabName);
            int row = 1;
            excelDataFromatter.WriteHeader<T>(ws, formatMapper, profile, ref row);
            foreach (var entity in entities)
            {
                excelDataFromatter.DataLine<T>(ws, formatMapper, entity, ref row);
            }

            excelDataFromatter.EmptyLine(ref row);

            excelDataFromatter.TotalLine<T>(ws, formatMapper, entities, ref row);

            return (ws, row);
        }


    }
}
