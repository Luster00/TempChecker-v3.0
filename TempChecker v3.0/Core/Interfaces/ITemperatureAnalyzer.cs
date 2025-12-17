using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempChecker.Core.Models;

namespace TempChecker.Core.Interfaces
{
    public interface ITemperatureAnalyzer
    {
        TemperatureAnalysis Analyze(int temperature);
        string GetRecommendation(TemperatureStatus status);
    }

    public class TemperatureAnalysis
    {
        public TemperatureStatus Status { get; }
        public string Message { get; }
        public ConsoleColor Color { get; }

        public TemperatureAnalysis(TemperatureStatus status, string message, ConsoleColor color)
        {
            Status = status;
            Message = message;
            Color = color;
        }
    }
}