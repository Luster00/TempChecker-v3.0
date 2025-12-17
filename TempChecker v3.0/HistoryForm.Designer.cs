namespace TempChecker.WindowsForms
{
    partial class HistoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxHistory = new System.Windows.Forms.ListBox();
            this.lblLogFilePath = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxHistory
            // 
            this.listBoxHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxHistory.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxHistory.FormattingEnabled = true;
            this.listBoxHistory.ItemHeight = 14;
            this.listBoxHistory.Location = new System.Drawing.Point(12, 12);
            this.listBoxHistory.Name = "listBoxHistory";
            this.listBoxHistory.Size = new System.Drawing.Size(760, 382);
            this.listBoxHistory.TabIndex = 0;
            // 
            // lblLogFilePath
            // 
            this.lblLogFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogFilePath.AutoEllipsis = true;
            this.lblLogFilePath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLogFilePath.Location = new System.Drawing.Point(12, 410);
            this.lblLogFilePath.Name = "lblLogFilePath";
            this.lblLogFilePath.Size = new System.Drawing.Size(760, 23);
            this.lblLogFilePath.TabIndex = 1;
            this.lblLogFilePath.Text = "Путь к логам:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClose.Location = new System.Drawing.Point(672, 440);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 481);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblLogFilePath);
            this.Controls.Add(this.listBoxHistory);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "HistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "История замеров температуры";
            this.ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxHistory;
        private Label lblLogFilePath;
        private Button btnClose;
    }
}