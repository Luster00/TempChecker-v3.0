using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Windows.Forms;
using TempChecker.Core.Controllers;
using TempChecker.Core.Interfaces;
using TempChecker.Services.Analyzers;
using TempChecker.Services.Loggers;
using TempChecker.Services.Measurers;
using TempChecker.Services.Validators;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TempChecker.WindowsForms
{
    public partial class Form1 : Form  // ОБРАТИТЕ ВНИМАНИЕ: наследуемся от Form
    {
        private readonly MeltingAppController _appController;
        private readonly ITemperatureMeasurer _measurer;
        private readonly IMeasurementLogger _logger;
        private readonly IInputValidator _validator;
        private readonly ITemperatureAnalyzer _analyzer;

        public Form1()
        {
            InitializeComponent();  // ВАЖНО: этот метод должен быть вызван первым

            // Настройка DI контейнера
            var services = new ServiceCollection();
            services.AddSingleton<IInputValidator, MeltingNumberValidator>();
            services.AddSingleton<ITemperatureMeasurer, TemperatureMeasurer>();
            services.AddSingleton<IMeasurementLogger, FileMeasurementLogger>();
            services.AddSingleton<ITemperatureAnalyzer, TemperatureAnalyzer>();
            services.AddSingleton<MeltingAppController>();

            var serviceProvider = services.BuildServiceProvider();

            _validator = serviceProvider.GetService<IInputValidator>();
            _measurer = serviceProvider.GetService<ITemperatureMeasurer>();
            _logger = serviceProvider.GetService<IMeasurementLogger>();
            _appController = serviceProvider.GetService<MeltingAppController>();
            _analyzer = serviceProvider.GetService<ITemperatureAnalyzer>();

            _appController = serviceProvider.GetService<MeltingAppController>();
            // Подписываемся на события
            _measurer.MeasurementProgress += OnMeasurementProgress;

            SetupUI();
        }

        private void SetupUI()
        {
            Text = "TempChecker - Система контроля температуры плавки";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);
        }

        private void OnMeasurementProgress(object sender, MeasurementProgressEventArgs e)
        {
            // Обновляем прогресс в UI потоке
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(e)));
            }
            else
            {
                UpdateProgress(e);
            }
        }

        private void UpdateProgress(MeasurementProgressEventArgs e)
        {
            lblProgress.Text = e.Message;
            progressBar.Value = e.ProgressPercentage;

            if (e.ProgressPercentage == 100)
            {
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }

        private void btnStartMeasurement_Click(object sender, EventArgs e)
        {
            StartMeasurement();
        }

        private async void StartMeasurement()
        {
            try
            {
                // Блокируем UI элементы на время измерения
                SetControlsEnabled(false);

                // Валидация номера плавки
                var validationResult = _validator.Validate(txtMeltingNumber.Text);
                if (!validationResult.IsValid)
                {
                    MessageBox.Show(validationResult.ErrorMessage, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int meltingNumber = validationResult.Value!.Value;

                // Показываем прогресс
                progressBar.Style = ProgressBarStyle.Marquee;
                lblStatus.Text = "Измерение температуры...";

                // Измеряем температуру (асинхронно)
                int temperature = await System.Threading.Tasks.Task.Run(() =>
                    _measurer.MeasureTemperature());

                // Анализируем результат
                var analyzer = new TemperatureAnalyzer();
                var analysis = analyzer.Analyze(temperature);

                // Отображаем результаты
                DisplayResults(meltingNumber, temperature, analysis);

                // Логируем
                _logger.LogMeasurement(meltingNumber, temperature);

                // Проверяем историю
                CheckHistory(meltingNumber);

                MessageBox.Show("Измерение завершено успешно!", "Готово",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsEnabled(true);
                lblStatus.Text = "Готово";
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 0;
            }
        }

        private void DisplayResults(int meltingNumber, int temperature,
            Core.Interfaces.TemperatureAnalysis analysis)
        {
            txtResults.Clear();

            txtResults.AppendText($"РЕЗУЛЬТАТЫ АНАЛИЗА\n");
            txtResults.AppendText($"════════════════════════════════════════\n");
            txtResults.AppendText($"Номер плавки: {meltingNumber:D9}\n");
            txtResults.AppendText($"Температура: {temperature}°C\n\n");

            // Цветной текст в зависимости от статуса
            txtResults.SelectionColor = GetStatusColor(analysis.Status);
            txtResults.AppendText($"Статус: {analysis.Message}\n");
            txtResults.SelectionColor = Color.Black;

            // Получаем рекомендацию через экземпляр ITemperatureAnalyzer
            string recommendation = GetRecommendation(analysis.Status);
            txtResults.AppendText($"\nРекомендация: {recommendation}\n");
            txtResults.AppendText($"════════════════════════════════════════\n");
        }

        private string GetRecommendation(Core.Models.TemperatureStatus status)
        {
            // Вызываем метод GetRecommendation из ITemperatureAnalyzer
            return _analyzer.GetRecommendation(status);
        }

        private Color GetStatusColor(Core.Models.TemperatureStatus status)
        {
            return status switch
            {
                Core.Models.TemperatureStatus.Critical => Color.Red,
                Core.Models.TemperatureStatus.Normal => Color.Green,
                Core.Models.TemperatureStatus.Low => Color.Orange,
                _ => Color.Black
            };
        }

        private void CheckHistory(int meltingNumber)
        {
            if (_logger is FileMeasurementLogger fileLogger)
            {
                string historyInfo = fileLogger.GetMeltingHistoryInfo(meltingNumber);
                if (historyInfo.Contains("Найдено предыдущих замеров"))
                {
                    MessageBox.Show(historyInfo, "История замеров",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            txtMeltingNumber.Enabled = enabled;
            btnStartMeasurement.Enabled = enabled;
            btnViewHistory.Enabled = enabled;
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            ViewHistory();
        }

        private void ViewHistory()
        {
            try
            {
                var historyForm = new HistoryForm(_logger);
                historyForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Отписываемся от событий
            if (_measurer != null)
            {
                _measurer.MeasurementProgress -= OnMeasurementProgress;
            }
        }
    }
}