using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Application.Audit.Projections.Formatting
{
    public interface IAuditValueFormatter
    {
        string FormatAction(string action);
        string FormatBoolean(string value);
    }
}
