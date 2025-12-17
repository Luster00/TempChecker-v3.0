namespace TempChecker.WindowsForms
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtMeltingNumber = new TextBox();
            btnStartMeasurement = new Button();
            btnViewHistory = new Button();
            txtResults = new RichTextBox();
            progressBar = new ProgressBar();
            lblProgress = new Label();
            lblStatus = new Label();
            lblMeltingNumber = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txtMeltingNumber
            // 
            txtMeltingNumber.Font = new Font("Segoe UI", 10F);
            txtMeltingNumber.Location = new Point(150, 17);
            txtMeltingNumber.Name = "txtMeltingNumber";
            txtMeltingNumber.Size = new Size(200, 25);
            txtMeltingNumber.TabIndex = 1;
            // 
            // btnStartMeasurement
            // 
            btnStartMeasurement.Font = new Font("Segoe UI", 10F);
            btnStartMeasurement.Location = new Point(370, 15);
            btnStartMeasurement.Name = "btnStartMeasurement";
            btnStartMeasurement.Size = new Size(180, 30);
            btnStartMeasurement.TabIndex = 2;
            btnStartMeasurement.Text = "Начать измерение";
            btnStartMeasurement.UseVisualStyleBackColor = true;
            btnStartMeasurement.Click += btnStartMeasurement_Click;
            // 
            // btnViewHistory
            // 
            btnViewHistory.Font = new Font("Segoe UI", 10F);
            btnViewHistory.Location = new Point(570, 15);
            btnViewHistory.Name = "btnViewHistory";
            btnViewHistory.Size = new Size(180, 30);
            btnViewHistory.TabIndex = 3;
            btnViewHistory.Text = "Просмотреть историю";
            btnViewHistory.UseVisualStyleBackColor = true;
            btnViewHistory.Click += btnViewHistory_Click;
            // 
            // txtResults
            // 
            txtResults.Dock = DockStyle.Fill;
            txtResults.Font = new Font("Consolas", 10F);
            txtResults.Location = new Point(0, 70);
            txtResults.Name = "txtResults";
            txtResults.ReadOnly = true;
            txtResults.Size = new Size(784, 410);
            txtResults.TabIndex = 6;
            txtResults.Text = "";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 20);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(740, 23);
            progressBar.TabIndex = 0;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Font = new Font("Segoe UI", 9F);
            lblProgress.Location = new Point(20, 50);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(123, 15);
            lblProgress.TabIndex = 1;
            lblProgress.Text = "Готов к измерению...";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.Location = new Point(20, 75);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(45, 15);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Статус:";
            // 
            // lblMeltingNumber
            // 
            lblMeltingNumber.AutoSize = true;
            lblMeltingNumber.Font = new Font("Segoe UI", 10F);
            lblMeltingNumber.Location = new Point(20, 20);
            lblMeltingNumber.Name = "lblMeltingNumber";
            lblMeltingNumber.Size = new Size(103, 19);
            lblMeltingNumber.TabIndex = 0;
            lblMeltingNumber.Text = "Номер плавки:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(lblMeltingNumber);
            panel1.Controls.Add(txtMeltingNumber);
            panel1.Controls.Add(btnStartMeasurement);
            panel1.Controls.Add(btnViewHistory);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 70);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(progressBar);
            panel2.Controls.Add(lblProgress);
            panel2.Controls.Add(lblStatus);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 480);
            panel2.Name = "panel2";
            panel2.Size = new Size(784, 100);
            panel2.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 580);
            Controls.Add(txtResults);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "TempChecker v3.0 - Система контроля температуры плавки";
            FormClosing += Form1_FormClosing;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtMeltingNumber;
        private System.Windows.Forms.Button btnStartMeasurement;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMeltingNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}