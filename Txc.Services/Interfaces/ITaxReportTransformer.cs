using System.IO;
using Txc.Services.TaxReportServices;

namespace Txc.Services
{
    public interface ITaxReportTransformer
    {
        public void Transform(Stream stream, ITaxReport report);
    }
}
