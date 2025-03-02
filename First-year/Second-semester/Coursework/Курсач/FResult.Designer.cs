namespace Курсач
{
    partial class FResult
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
            this.MMMenu = new System.Windows.Forms.MenuStrip();
            this.N1File = new System.Windows.Forms.ToolStripMenuItem();
            this.N12Save = new System.Windows.Forms.ToolStripMenuItem();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.BBack = new System.Windows.Forms.Button();
            this.LAdmonition1 = new System.Windows.Forms.Label();
            this.ENumOfResults = new System.Windows.Forms.MaskedTextBox();
            this.BStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LNumOfDecision = new System.Windows.Forms.Label();
            this.LNumOfSteps = new System.Windows.Forms.Label();
            this.LNumOfDecisionShown = new System.Windows.Forms.Label();
            this.ESearch = new System.Windows.Forms.MaskedTextBox();
            this.BSearch = new System.Windows.Forms.Button();
            this.BPrev = new System.Windows.Forms.Button();
            this.BNext = new System.Windows.Forms.Button();
            this.DlgSaveResult = new System.Windows.Forms.SaveFileDialog();
            this.MMMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // MMMenu
            // 
            this.MMMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MMMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.N1File});
            this.MMMenu.Location = new System.Drawing.Point(0, 0);
            this.MMMenu.Name = "MMMenu";
            this.MMMenu.Size = new System.Drawing.Size(696, 28);
            this.MMMenu.TabIndex = 1;
            this.MMMenu.Text = "Menu";
            // 
            // N1File
            // 
            this.N1File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.N12Save});
            this.N1File.Name = "N1File";
            this.N1File.Size = new System.Drawing.Size(57, 24);
            this.N1File.Text = "Файл";
            // 
            // N12Save
            // 
            this.N12Save.Name = "N12Save";
            this.N12Save.Size = new System.Drawing.Size(207, 26);
            this.N12Save.Text = "Сохранить судоку";
            this.N12Save.Click += new System.EventHandler(this.N12Save_Click);
            // 
            // DGV1
            // 
            this.DGV1.AllowUserToAddRows = false;
            this.DGV1.AllowUserToDeleteRows = false;
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(12, 31);
            this.DGV1.Name = "DGV1";
            this.DGV1.RowTemplate.Height = 24;
            this.DGV1.Size = new System.Drawing.Size(369, 369);
            this.DGV1.TabIndex = 12;
            // 
            // BBack
            // 
            this.BBack.Location = new System.Drawing.Point(390, 357);
            this.BBack.Name = "BBack";
            this.BBack.Size = new System.Drawing.Size(299, 39);
            this.BBack.TabIndex = 13;
            this.BBack.Text = "Назад";
            this.BBack.UseVisualStyleBackColor = true;
            this.BBack.Click += new System.EventHandler(this.BExit_Click);
            // 
            // LAdmonition1
            // 
            this.LAdmonition1.AutoSize = true;
            this.LAdmonition1.Location = new System.Drawing.Point(387, 31);
            this.LAdmonition1.Name = "LAdmonition1";
            this.LAdmonition1.Size = new System.Drawing.Size(280, 34);
            this.LAdmonition1.TabIndex = 14;
            this.LAdmonition1.Text = "Введите желаемое количество решений,\r\nа затем Старт.";
            // 
            // ENumOfResults
            // 
            this.ENumOfResults.Location = new System.Drawing.Point(390, 68);
            this.ENumOfResults.Name = "ENumOfResults";
            this.ENumOfResults.Size = new System.Drawing.Size(299, 22);
            this.ENumOfResults.TabIndex = 15;
            // 
            // BStart
            // 
            this.BStart.Location = new System.Drawing.Point(390, 96);
            this.BStart.Name = "BStart";
            this.BStart.Size = new System.Drawing.Size(299, 39);
            this.BStart.TabIndex = 16;
            this.BStart.Text = "Старт";
            this.BStart.UseVisualStyleBackColor = true;
            this.BStart.Click += new System.EventHandler(this.BStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(387, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Найдено количество решений:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Количество действий:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Номре показываемого решения:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(387, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Поиск решения по номеру:";
            // 
            // LNumOfDecision
            // 
            this.LNumOfDecision.AutoSize = true;
            this.LNumOfDecision.Location = new System.Drawing.Point(387, 155);
            this.LNumOfDecision.Name = "LNumOfDecision";
            this.LNumOfDecision.Size = new System.Drawing.Size(0, 17);
            this.LNumOfDecision.TabIndex = 21;
            // 
            // LNumOfSteps
            // 
            this.LNumOfSteps.AutoSize = true;
            this.LNumOfSteps.Location = new System.Drawing.Point(387, 189);
            this.LNumOfSteps.Name = "LNumOfSteps";
            this.LNumOfSteps.Size = new System.Drawing.Size(0, 17);
            this.LNumOfSteps.TabIndex = 22;
            // 
            // LNumOfDecisionShown
            // 
            this.LNumOfDecisionShown.AutoSize = true;
            this.LNumOfDecisionShown.Location = new System.Drawing.Point(387, 221);
            this.LNumOfDecisionShown.Name = "LNumOfDecisionShown";
            this.LNumOfDecisionShown.Size = new System.Drawing.Size(0, 17);
            this.LNumOfDecisionShown.TabIndex = 23;
            // 
            // ESearch
            // 
            this.ESearch.Location = new System.Drawing.Point(390, 258);
            this.ESearch.Name = "ESearch";
            this.ESearch.Size = new System.Drawing.Size(196, 22);
            this.ESearch.TabIndex = 24;
            // 
            // BSearch
            // 
            this.BSearch.Location = new System.Drawing.Point(592, 258);
            this.BSearch.Name = "BSearch";
            this.BSearch.Size = new System.Drawing.Size(97, 22);
            this.BSearch.TabIndex = 25;
            this.BSearch.Text = "Поиск";
            this.BSearch.UseVisualStyleBackColor = true;
            this.BSearch.Click += new System.EventHandler(this.BSearch_Click);
            // 
            // BPrev
            // 
            this.BPrev.Location = new System.Drawing.Point(390, 297);
            this.BPrev.Name = "BPrev";
            this.BPrev.Size = new System.Drawing.Size(141, 39);
            this.BPrev.TabIndex = 26;
            this.BPrev.Text = "Пред.";
            this.BPrev.UseVisualStyleBackColor = true;
            this.BPrev.Click += new System.EventHandler(this.BPrev_Click);
            // 
            // BNext
            // 
            this.BNext.Location = new System.Drawing.Point(558, 297);
            this.BNext.Name = "BNext";
            this.BNext.Size = new System.Drawing.Size(131, 39);
            this.BNext.TabIndex = 27;
            this.BNext.Text = "След.";
            this.BNext.UseVisualStyleBackColor = true;
            this.BNext.Click += new System.EventHandler(this.BNext_Click);
            // 
            // DlgSaveResult
            // 
            this.DlgSaveResult.DefaultExt = "txt";
            this.DlgSaveResult.Filter = "Текстовый файл|*.txt";
            // 
            // FResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 408);
            this.ControlBox = false;
            this.Controls.Add(this.BNext);
            this.Controls.Add(this.BPrev);
            this.Controls.Add(this.BSearch);
            this.Controls.Add(this.ESearch);
            this.Controls.Add(this.LNumOfDecisionShown);
            this.Controls.Add(this.LNumOfSteps);
            this.Controls.Add(this.LNumOfDecision);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BStart);
            this.Controls.Add(this.ENumOfResults);
            this.Controls.Add(this.LAdmonition1);
            this.Controls.Add(this.BBack);
            this.Controls.Add(this.DGV1);
            this.Controls.Add(this.MMMenu);
            this.Name = "FResult";
            this.Text = "Result";
            this.Load += new System.EventHandler(this.FResult_Load);
            this.MMMenu.ResumeLayout(false);
            this.MMMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MMMenu;
        private System.Windows.Forms.ToolStripMenuItem N1File;
        private System.Windows.Forms.ToolStripMenuItem N12Save;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.Button BBack;
        private System.Windows.Forms.Label LAdmonition1;
        private System.Windows.Forms.MaskedTextBox ENumOfResults;
        private System.Windows.Forms.Button BStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LNumOfDecision;
        private System.Windows.Forms.Label LNumOfSteps;
        private System.Windows.Forms.Label LNumOfDecisionShown;
        private System.Windows.Forms.MaskedTextBox ESearch;
        private System.Windows.Forms.Button BSearch;
        private System.Windows.Forms.Button BPrev;
        private System.Windows.Forms.Button BNext;
        private System.Windows.Forms.SaveFileDialog DlgSaveResult;
    }
}