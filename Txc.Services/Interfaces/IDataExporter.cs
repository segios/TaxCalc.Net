using System.Collections.Generic;
using System.IO;
using Txc.Model;
using Txc.Model.Account;
using Txc.Services.TaxReportServices;

namespace Txc.Services
{
    public interface IDataExporter 
    {
        void Export(Stream stream, Profile profile, ITaxReport taxReport) ;
    }
}
