using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using TempChecker.Core.Interfaces;

namespace TempChecker.Services.Measurers
{
    public class TemperatureMeasurer : ITemperatureMeasurer
    {
        private readonly Random _random = new();
        public event EventHandler<MeasurementProgressEventArgs>? MeasurementProgress;

        public int MeasureTemperature()
        {
            SimulateMeasurementProcess();
            return _random.Next(1550, 1651);
        }

        private void SimulateMeasurementProcess()
        {
            var steps = new[]
            {
                ("Инициализация пирометра...", 10),
                ("Калибровка прибора...", 30),
                ("Наведение на цель...", 60),
                ("Измерение температуры...", 100)
            };

            foreach (var (message, progress) in steps)
            {
                OnMeasurementProgress(message, progress);
                Thread.Sleep(1000);
            }
        }

        protected virtual void OnMeasurementProgress(string message, int progress)
        {
            MeasurementProgress?.Invoke(this, new MeasurementProgressEventArgs(message, progress));
        }
    }
}