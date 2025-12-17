using System;
using TempChecker.Core.Interfaces;
using TempChecker.Core.Models;
using TempChecker.Services.Loggers;
using TempChecker.Utils;

namespace TempChecker.Core.Controllers
{
    public class MeltingAppController
    {
        private readonly IInputValidator _validator;
        private readonly ITemperatureMeasurer _measurer;
        private readonly IMeasurementLogger _logger;
        private readonly ITemperatureAnalyzer _analyzer;

        public MeltingAppController(
            IInputValidator validator,
            ITemperatureMeasurer measurer,
            IMeasurementLogger logger,
            ITemperatureAnalyzer analyzer)
        {
            _validator = validator;
            _measurer = measurer;
            _logger = logger;
            _analyzer = analyzer;

            _measurer.MeasurementProgress += OnMeasurementProgress;
        }

        public void Run()
        {
            try
            {
                ConsoleHelper.DisplayHeader("СИСТЕМА КОНТРОЛЯ ТЕМПЕРАТУРЫ ПЛАВКИ");

                int meltingNumber = GetValidMeltingNumber();

                // Проверяем историю по плавке
                CheckMeltingHistory(meltingNumber);

                int temperature = MeasureTemperature(meltingNumber);

                DisplayAnalysisResults(meltingNumber, temperature);
                _logger.LogMeasurement(meltingNumber, temperature);

                ConsoleHelper.DisplaySuccess("Операция завершена успешно!");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            finally
            {
                // 🔴 Ожидание перед закрытием консоли
                WaitForExit();
            }
        }

        private int GetValidMeltingNumber()
        {
            while (true)
            {
                Console.Write("\nВведите номер плавки (1-9 цифр): ");
                string input = Console.ReadLine() ?? string.Empty;

                var validationResult = _validator.Validate(input);

                if (validationResult.IsValid)
                {
                    ConsoleHelper.DisplaySuccess($"Номер плавки принят: {validationResult.Value}");
                    return validationResult.Value!.Value;
                }

                ConsoleHelper.DisplayError(validationResult.ErrorMessage);
            }
        }

        private void CheckMeltingHistory(int meltingNumber)
        {
            // Используем кастинг к конкретному типу для доступа к новому методу
            if (_logger is FileMeasurementLogger fileLogger)
            {
                string historyInfo = fileLogger.GetMeltingHistoryInfo(meltingNumber);
                if (historyInfo.Contains("Найдено предыдущих замеров"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n⚠  {historyInfo}");
                    Console.ResetColor();
                }
            }
        }

        private int MeasureTemperature(int meltingNumber)
        {
            Console.WriteLine($"\nИзмерение температуры для плавки #{meltingNumber}...");
            return _measurer.MeasureTemperature();
        }

        private void OnMeasurementProgress(object? sender, MeasurementProgressEventArgs e)
        {
            ConsoleHelper.DisplayProgress(e.Message, e.ProgressPercentage);
        }

        private void DisplayAnalysisResults(int meltingNumber, int temperature)
        {
            var analysis = _analyzer.Analyze(temperature);

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine($"📊 РЕЗУЛЬТАТЫ АНАЛИЗА");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"🔢 Номер плавки: {meltingNumber:D9}");
            Console.WriteLine($"🌡️  Температура: {temperature}°C");

            Console.ForegroundColor = analysis.Color;
            Console.WriteLine($"📈 Статус: {analysis.Message}");
            Console.ResetColor();

            Console.WriteLine($"💡 Рекомендация: {_analyzer.GetRecommendation(analysis.Status)}");
            Console.WriteLine(new string('=', 60));
        }

        private void WaitForExit()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.WriteLine("   Нажмите любую клавишу для выхода из программы");
            Console.WriteLine("═══════════════════════════════════════════════════");
            Console.ResetColor();

            // Скрываем курсор для красоты
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }

        private void HandleError(Exception ex)
        {
            ConsoleHelper.DisplayError($"Произошла ошибка: {ex.Message}");
        }
    }
}