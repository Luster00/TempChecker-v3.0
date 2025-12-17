using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TempChecker.Core.Interfaces;
using TempChecker.Core.Models;

namespace TempChecker.Services.Analyzers
{
    public class TemperatureAnalyzer : ITemperatureAnalyzer
    {
        public TemperatureAnalysis Analyze(int temperature)
        {
            return temperature switch
            {
                > 1610 => new TemperatureAnalysis(
                    TemperatureStatus.Critical,
                    "КРИТИЧЕСКИЙ - Температура превышает 1610°C",
                    ConsoleColor.Red),
                >= 1580 and <= 1610 => new TemperatureAnalysis(
                    TemperatureStatus.Normal,
                    "НОРМАЛЬНЫЙ - Температура в допустимом диапазоне",
                    ConsoleColor.Green),
                _ => new TemperatureAnalysis(
                    TemperatureStatus.Low,
                    "НИЗКАЯ - Температура ниже оптимального диапазона",
                    ConsoleColor.Yellow)
            };
        }

        public string GetRecommendation(TemperatureStatus status)
        {
            return status switch
            {
                TemperatureStatus.Critical => "Немедленно снизить температуру!",
                TemperatureStatus.Normal => "Продолжать процесс в штатном режиме.",
                TemperatureStatus.Low => "Увеличить мощность нагрева.",
                _ => "Нет рекомендаций."
            };
        }
    }
}