using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempChecker.Core.Interfaces
{
    public interface IMeasurementLogger
    {
        void LogMeasurement(int meltingNumber, int temperature);
        IEnumerable<string> GetRecentLogs(int count = 10);
        string GetLogFilePath();
    }
}