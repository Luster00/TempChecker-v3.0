using System;
using System.Windows.Forms;
using TempChecker.Core.Interfaces;

namespace TempChecker.WindowsForms
{
    public partial class HistoryForm : Form
    {
        private readonly IMeasurementLogger _logger;

        public HistoryForm(IMeasurementLogger logger)
        {
            InitializeComponent();
            _logger = logger;
            LoadHistory();
        }

        private void LoadHistory()
        {
            try
            {
                var recentLogs = _logger.GetRecentLogs(20);
                foreach (var log in recentLogs)
                {
                    listBoxHistory.Items.Add(log);
                }

                lblLogFilePath.Text = $"Путь к логам: {_logger.GetLogFilePath()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}