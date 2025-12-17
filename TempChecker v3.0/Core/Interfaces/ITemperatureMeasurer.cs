using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempChecker.Core.Interfaces
{
    public interface ITemperatureMeasurer
    {
        int MeasureTemperature();
        event EventHandler<MeasurementProgressEventArgs> MeasurementProgress;
    }

    public class MeasurementProgressEventArgs : EventArgs
    {
        public string Message { get; }
        public int ProgressPercentage { get; }

        public MeasurementProgressEventArgs(string message, int progressPercentage = 0)
        {
            Message = message;
            ProgressPercentage = progressPercentage;
        }
    }
}