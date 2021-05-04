using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txc.Model;
using Txc.Model.Account;
using Txc.Services.Localization;

namespace Txc.Services.Export
{
    public class ExcelDataFromatter 
    {
        private ILocalizer localizer;
        public ExcelDataFromatter(ILocalizer localizer)
        {
            this.localizer = localizer;
        }

        public void WriteHeader<T>(ExcelWorksheet ws, IFormatMapping<T> formatMapper, Profile profile, 
            ref int row) 
//            where T : IRateConvertable
        {
            var keys = formatMapper.Keys;
            
            ws.Row(row).Style.Font.Bold = true;
            
            for (var i = 0; i < keys.Count; i++) 
            {
                ws.Column(i + 1).Width = 15;

                (var key, var argsFunc, var func, var formatOptions) = keys[i];

                ws.Column(i + 1).Width = formatOptions.Width ?? 15;

                var args = argsFunc != null ? argsFunc.Select(x => x(profile)).ToArray() : null;
                ws.Cells[row, i+1].Value = localizer.T<T>(key, args);
            }
            row ++;
        }

        public void EmptyLine(ref int row)
        {
            row ++;
        }

        public void DataLine<T>(ExcelWorksheet ws, IFormatMapping<T> formatMapper, T entity,
            ref int row) 
//            where T : IRateConvertable
        {
            var keys = formatMapper.Keys;
            var i = 0;
            for (i = 0; i < keys.Count; i++)
            {
                (var key, var argsFunc, var func, var formatOptions) = keys[i];
                var value = FormatValue(entity, func(entity), formatOptions);

                ws.Cells[row, i+1].Value = value;
            }

            var validable = entity as IValidable;
            if (validable != null) 
            {
                if (!validable.IsValid) 
                {
                    ws.Row(row).Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Row(row).Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    ws.Cells[row, i + 1].Value = validable.Error;
                }
            }

            row ++;
        }

        public object FormatValue<T>(T entity, object val, FormatOptions formatOptions) 
//            where T : IRateConvertable
        {
            if (val == null)
            {
                return string.Empty;
            }

            if (val.GetType() == typeof(DateTime)) 
            {
                
                var dt = ((DateTime)val);
                return dt.ToString(formatOptions.DateTimeFormat);
            }

            object value = val;

            if (val.GetType().IsEnum)
            {
                value = val.ToString();
            }

            if (formatOptions.IsLocalizable) 
            {
                if (val.GetType().IsEnum)
                {
                    value = localizer.T(val.GetType(), val.ToString());
                }
                else 
                {
                    value = localizer.T(value.ToString());
                }
            }

            return value;
        }

        public void TotalLine<T>(ExcelWorksheet ws, IFormatMapping<T> formatMapper, 
            IEnumerable<T> entities, ref int row)
        {
            if (formatMapper.TotalKeys == null || formatMapper.TotalKeys.Count == 0)
                return;

            ws.Row(row).Style.Font.Bold = true;

            ws.Cells[row, 1].Value = localizer.T("Total");

            foreach (var totalKey in formatMapper.TotalKeys) 
            {
                (var key, var func) = totalKey;

                var totalValue = Math.Round(entities.Sum(func), 2);

                var idxOfTotal = formatMapper.GetKeyIndex(key);
                if (idxOfTotal < 0)
                {
                    continue;
                }
                ws.Cells[row, idxOfTotal + 1].Formula = "=SUM(" + ws.Cells[1, idxOfTotal + 1].Address + ":" + ws.Cells[row-1, idxOfTotal + 1].Address + ")";
//                ws.Cells[row, idxOfTotal+1].Value = totalValue;
            }

            row++;
        }

        public void WriteData<T>(ExcelWorksheet ws, int row, int col, T val)
        {
            ws.Cells[row, col].Value = val;
        }
    }
}
