using System.Text;
using TempChecker.Core.Interfaces;
using TempChecker.Core.Models;

namespace TempChecker.Services.Loggers
{
    public class FileMeasurementLogger : IMeasurementLogger
    {
        private readonly string _logDirectory;
        private readonly string _logFilePath;
        private readonly string _txtLogFilePath;

        public FileMeasurementLogger()
        {
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            _logFilePath = Path.Combine(_logDirectory, "measurements.csv");
            _txtLogFilePath = Path.Combine(_logDirectory, "measurements.txt");
            InitializeLogFiles();
        }

        private void InitializeLogFiles()
        {
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);

            // Инициализируем CSV файл с разделителем-точкой-с-запятой для Excel
            if (!File.Exists(_logFilePath))
            {
                // UTF-8 с BOM для корректного отображения в Excel
                using (var writer = new StreamWriter(_logFilePath, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine("Дата;Время;Номер плавки;Температура (°C);Статус;Примечание");
                }
            }

            // Инициализируем TXT файл
            if (!File.Exists(_txtLogFilePath))
            {
                File.WriteAllText(_txtLogFilePath,
                    "=== ЖУРНАЛ ЗАМЕРОВ ТЕМПЕРАТУРЫ ===\n" +
                    "Дата        Время      Плавка       Температура  Статус      Примечание\n" +
                    "------------------------------------------------------------------------\n",
                    Encoding.UTF8);
            }
        }

        public void LogMeasurement(int meltingNumber, int temperature)
        {
            var result = new MeasurementResult(meltingNumber, temperature);

            // Проверяем, есть ли предыдущие замеры по этой плавке
            bool hasPreviousMeasurements = CheckPreviousMeasurements(meltingNumber);
            string note = hasPreviousMeasurements ? "ПОВТОРНЫЙ ЗАМЕР" : "ПЕРВЫЙ ЗАМЕР";

            // Запись в основной CSV файл
            WriteToCsv(result, note);

            // Запись в основной TXT файл
            WriteToTxt(result, note);

            // Создание/обновление файла конкретной плавки
            UpdateMeltingLog(result, hasPreviousMeasurements);

            // Создание отдельного отчета
            CreateDetailedReport(result, note);
        }

        private bool CheckPreviousMeasurements(int meltingNumber)
        {
            string meltingFilePath = GetMeltingFilePath(meltingNumber);
            return File.Exists(meltingFilePath) &&
                   File.ReadAllLines(meltingFilePath).Length > 1; // Больше 1, т.к. есть заголовок
        }

        private string GetMeltingFilePath(int meltingNumber)
        {
            string meltingDir = Path.Combine(_logDirectory, "ByMelting");
            if (!Directory.Exists(meltingDir))
                Directory.CreateDirectory(meltingDir);

            return Path.Combine(meltingDir, $"melting_{meltingNumber:D9}.csv");
        }

        private void WriteToCsv(MeasurementResult result, string note)
        {
            // Формат для Excel: разделитель точка с запятой, дата в формате дд.мм.гггг
            string csvEntry = $"\"{result.Timestamp:dd.MM.yyyy}\";" +
                            $"\"{result.Timestamp:HH:mm:ss}\";" +
                            $"\"{result.MeltingNumber:D9}\";" +
                            $"\"{result.Temperature}\";" +
                            $"\"{GetStatusInRussian(result.Status)}\";" +
                            $"\"{note}\"";

            // Записываем с UTF-8 BOM для корректного открытия в Excel
            using (var writer = new StreamWriter(_logFilePath, true, new UTF8Encoding(true)))
            {
                writer.WriteLine(csvEntry);
            }
        }

        private void WriteToTxt(MeasurementResult result, string note)
        {
            // Форматируем с фиксированной шириной столбцов
            string txtEntry = $"{result.Timestamp:dd.MM.yyyy}  " +
                            $"{result.Timestamp:HH:mm:ss}  " +
                            $"{result.MeltingNumber:D9}  " +
                            $"{result.Temperature,4}°C  " +
                            $"{GetStatusInRussian(result.Status),-10}  " +
                            $"{note,-15}";

            File.AppendAllText(_txtLogFilePath, txtEntry + Environment.NewLine, Encoding.UTF8);
        }

        private void UpdateMeltingLog(MeasurementResult result, bool hasPreviousMeasurements)
        {
            string meltingFilePath = GetMeltingFilePath(result.MeltingNumber);

            // Если файл не существует, создаем его с заголовком
            if (!File.Exists(meltingFilePath))
            {
                using (var writer = new StreamWriter(meltingFilePath, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine("Дата;Время;Температура (°C);Статус;Номер замера");
                }
            }

            // Определяем номер замера
            int measurementNumber = 1;
            if (hasPreviousMeasurements)
            {
                var lines = File.ReadAllLines(meltingFilePath);
                if (lines.Length > 1)
                {
                    var lastLine = lines.Last();
                    var parts = lastLine.Split(';');
                    if (parts.Length >= 5 && int.TryParse(parts[4], out int lastNumber))
                    {
                        measurementNumber = lastNumber + 1;
                    }
                }
            }

            // Добавляем новый замер
            string logEntry = $"\"{result.Timestamp:dd.MM.yyyy}\";" +
                            $"\"{result.Timestamp:HH:mm:ss}\";" +
                            $"\"{result.Temperature}\";" +
                            $"\"{GetStatusInRussian(result.Status)}\";" +
                            $"\"{measurementNumber}\"";

            File.AppendAllText(meltingFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
        }

        private void CreateDetailedReport(MeasurementResult result, string note)
        {
            string reportDir = Path.Combine(_logDirectory, "Reports");
            if (!Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);

            string fileName = $"Report_{result.MeltingNumber:D9}.txt";
            string filePath = Path.Combine(reportDir, fileName);

            var report = new StringBuilder();

            // Если файл уже существует, читаем его содержимое
            if (File.Exists(filePath))
            {
                report.AppendLine(File.ReadAllText(filePath, Encoding.UTF8));
                report.AppendLine(new string('-', 50));
            }
            else
            {
                report.AppendLine("========================================");
                report.AppendLine($"   ИСТОРИЯ ЗАМЕРОВ ПЛАВКИ #{result.MeltingNumber:D9}");
                report.AppendLine("========================================");
                report.AppendLine();
            }

            report.AppendLine($"ЗАМЕР #{GetNextMeasurementNumber(filePath)} - {note}");
            report.AppendLine($"Дата и время:    {result.Timestamp:dd.MM.yyyy HH:mm:ss}");
            report.AppendLine($"Температура:     {result.Temperature}°C");
            report.AppendLine($"Статус:          {GetStatusInRussian(result.Status)}");
            report.AppendLine($"Рекомендация:    {GetRecommendation(result.Status)}");
            report.AppendLine();

            File.WriteAllText(filePath, report.ToString(), Encoding.UTF8);
        }

        private int GetNextMeasurementNumber(string filePath)
        {
            if (!File.Exists(filePath))
                return 1;

            var content = File.ReadAllText(filePath);
            int count = 0;
            int index = 0;

            while ((index = content.IndexOf("ЗАМЕР #", index)) != -1)
            {
                count++;
                index += 7; // Длина "ЗАМЕР #"
            }

            return count + 1;
        }

        private string GetStatusInRussian(TemperatureStatus status)
        {
            return status switch
            {
                TemperatureStatus.Critical => "Критический",
                TemperatureStatus.Normal => "Норма",
                TemperatureStatus.Low => "Низкий",
                _ => "Неизвестно"
            };
        }

        private string GetRecommendation(TemperatureStatus status)
        {
            return status switch
            {
                TemperatureStatus.Critical => "Немедленно снизить температуру!",
                TemperatureStatus.Normal => "Продолжать процесс в штатном режиме.",
                TemperatureStatus.Low => "Увеличить мощность нагрева.",
                _ => "Нет рекомендаций."
            };
        }

        public IEnumerable<string> GetRecentLogs(int count = 10)
        {
            if (!File.Exists(_txtLogFilePath))
                return Enumerable.Empty<string>();

            var lines = File.ReadAllLines(_txtLogFilePath, Encoding.UTF8);
            return lines.Skip(Math.Max(3, lines.Length - count));
        }

        public string GetLogFilePath() => _txtLogFilePath;

        // Новый метод для получения информации о повторных замерах
        public string GetMeltingHistoryInfo(int meltingNumber)
        {
            string meltingFilePath = GetMeltingFilePath(meltingNumber);

            if (!File.Exists(meltingFilePath))
                return "По этой плавке нет предыдущих замеров.";

            var lines = File.ReadAllLines(meltingFilePath);
            int measurementCount = lines.Length - 1; // Минус заголовок

            if (measurementCount <= 0)
                return "По этой плавке нет предыдущих замеров.";

            // Получаем последний замер
            var lastLine = lines.Last();
            var parts = lastLine.Split(';');

            if (parts.Length >= 4)
            {
                return $"Найдено предыдущих замеров: {measurementCount}. " +
                       $"Последний был {parts[0].Trim('"')} в {parts[1].Trim('"')} " +
                       $"с температурой {parts[2].Trim('"')}°C";
            }

            return $"Найдено предыдущих замеров: {measurementCount}";
        }
    }
}