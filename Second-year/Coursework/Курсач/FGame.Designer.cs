namespace Курсач
{
    partial class FGame
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVMainArea = new System.Windows.Forms.DataGridView();
            this.BExit = new System.Windows.Forms.Button();
            this.RTBConsole = new System.Windows.Forms.RichTextBox();
            this.RTBStatistics = new System.Windows.Forms.RichTextBox();
            this.BRefreshStatistics = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMainArea)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVMainArea
            // 
            this.DGVMainArea.AllowUserToAddRows = false;
            this.DGVMainArea.AllowUserToDeleteRows = false;
            this.DGVMainArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVMainArea.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVMainArea.Location = new System.Drawing.Point(12, 12);
            this.DGVMainArea.Name = "DGVMainArea";
            this.DGVMainArea.RowTemplate.Height = 24;
            this.DGVMainArea.Size = new System.Drawing.Size(369, 369);
            this.DGVMainArea.TabIndex = 12;
            this.DGVMainArea.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMainArea_CellValueChanged);
            this.DGVMainArea.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DGVMainArea_EditingControlShowing);
            // 
            // BExit
            // 
            this.BExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BExit.Location = new System.Drawing.Point(387, 329);
            this.BExit.Name = "BExit";
            this.BExit.Size = new System.Drawing.Size(308, 52);
            this.BExit.TabIndex = 13;
            this.BExit.Text = "Выйти в главное меню";
            this.BExit.UseVisualStyleBackColor = true;
            this.BExit.Click += new System.EventHandler(this.BExit_Click);
            // 
            // RTBConsole
            // 
            this.RTBConsole.Location = new System.Drawing.Point(387, 184);
            this.RTBConsole.Name = "RTBConsole";
            this.RTBConsole.ReadOnly = true;
            this.RTBConsole.Size = new System.Drawing.Size(308, 139);
            this.RTBConsole.TabIndex = 15;
            this.RTBConsole.Text = "";
            // 
            // RTBStatistics
            // 
            this.RTBStatistics.Location = new System.Drawing.Point(387, 12);
            this.RTBStatistics.Name = "RTBStatistics";
            this.RTBStatistics.ReadOnly = true;
            this.RTBStatistics.Size = new System.Drawing.Size(308, 137);
            this.RTBStatistics.TabIndex = 16;
            this.RTBStatistics.Text = "";
            // 
            // BRefreshStatistics
            // 
            this.BRefreshStatistics.Location = new System.Drawing.Point(387, 155);
            this.BRefreshStatistics.Name = "BRefreshStatistics";
            this.BRefreshStatistics.Size = new System.Drawing.Size(308, 23);
            this.BRefreshStatistics.TabIndex = 17;
            this.BRefreshStatistics.Text = "Обновить статистику";
            this.BRefreshStatistics.UseVisualStyleBackColor = true;
            this.BRefreshStatistics.Click += new System.EventHandler(this.BRefreshStatistics_Click);
            // 
            // FGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 392);
            this.ControlBox = false;
            this.Controls.Add(this.BRefreshStatistics);
            this.Controls.Add(this.RTBStatistics);
            this.Controls.Add(this.RTBConsole);
            this.Controls.Add(this.BExit);
            this.Controls.Add(this.DGVMainArea);
            this.Name = "FGame";
            this.Text = "Судоку";
            this.Load += new System.EventHandler(this.FGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMainArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVMainArea;
        private System.Windows.Forms.Button BExit;
        private System.Windows.Forms.RichTextBox RTBConsole;
        private System.Windows.Forms.RichTextBox RTBStatistics;
        private System.Windows.Forms.Button BRefreshStatistics;
    }
}