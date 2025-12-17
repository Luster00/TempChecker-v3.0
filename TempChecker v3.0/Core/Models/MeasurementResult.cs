using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempChecker.Core.Models
{
    public class MeasurementResult
    {
        public int MeltingNumber { get; }
        public int Temperature { get; }
        public DateTime Timestamp { get; }
        public TemperatureStatus Status { get; }

        public MeasurementResult(int meltingNumber, int temperature)
        {
            MeltingNumber = meltingNumber;
            Temperature = temperature;
            Timestamp = DateTime.Now;
            Status = DetermineStatus(temperature);
        }

        private TemperatureStatus DetermineStatus(int temperature)
        {
            if (temperature > 1610) return TemperatureStatus.Critical;
            if (temperature >= 1580 && temperature <= 1610) return TemperatureStatus.Normal;
            return TemperatureStatus.Low;
        }

        public override string ToString()
        {
            return $"Плавка: {MeltingNumber:D9} | Температура: {Temperature}°C | Статус: {Status}";
        }
    }
}