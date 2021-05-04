using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Txc.Model;
using Txc.Model.Account;
using Txc.Model.Dividends;
using Txc.Model.Fees;
using Txc.Model.FinInstruments;
using Txc.Model.Interests;
using Txc.Model.SecuritiesLent;

namespace Txc.Services.TaxReportServices
{

    public interface IReportCreator
    {
        ITaxReport BuildReport(ReportOptions reportOptions);
    }
}
