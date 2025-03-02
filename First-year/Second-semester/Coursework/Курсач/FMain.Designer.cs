namespace Курсач
{
    partial class FMain
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MMMenu = new System.Windows.Forms.MenuStrip();
            this.N1File = new System.Windows.Forms.ToolStripMenuItem();
            this.N11Open = new System.Windows.Forms.ToolStripMenuItem();
            this.N12Save = new System.Windows.Forms.ToolStripMenuItem();
            this.N2Account = new System.Windows.Forms.ToolStripMenuItem();
            this.N21Change = new System.Windows.Forms.ToolStripMenuItem();
            this.DlgOpenMain = new System.Windows.Forms.OpenFileDialog();
            this.DlgSaveMain = new System.Windows.Forms.SaveFileDialog();
            this.LAdmonition = new System.Windows.Forms.Label();
            this.LRecursively = new System.Windows.Forms.Label();
            this.LSingles = new System.Windows.Forms.Label();
            this.BReset = new System.Windows.Forms.Button();
            this.BInput = new System.Windows.Forms.Button();
            this.BExit = new System.Windows.Forms.Button();
            this.CBWay = new System.Windows.Forms.ComboBox();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.MMMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.SuspendLayout();
            // 
            // MMMenu
            // 
            this.MMMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MMMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.N1File,
            this.N2Account});
            this.MMMenu.Location = new System.Drawing.Point(0, 0);
            this.MMMenu.Name = "MMMenu";
            this.MMMenu.Size = new System.Drawing.Size(696, 28);
            this.MMMenu.TabIndex = 0;
            this.MMMenu.Text = "Menu";
            // 
            // N1File
            // 
            this.N1File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.N11Open,
            this.N12Save});
            this.N1File.Name = "N1File";
            this.N1File.Size = new System.Drawing.Size(57, 24);
            this.N1File.Text = "Файл";
            // 
            // N11Open
            // 
            this.N11Open.Name = "N11Open";
            this.N11Open.Size = new System.Drawing.Size(207, 26);
            this.N11Open.Text = "Извлечь судоку";
            this.N11Open.Click += new System.EventHandler(this.N11Open_Click);
            // 
            // N12Save
            // 
            this.N12Save.Name = "N12Save";
            this.N12Save.Size = new System.Drawing.Size(207, 26);
            this.N12Save.Text = "Сохранить судоку";
            this.N12Save.Click += new System.EventHandler(this.N12Save_Click);
            // 
            // N2Account
            // 
            this.N2Account.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.N21Change});
            this.N2Account.Name = "N2Account";
            this.N2Account.Size = new System.Drawing.Size(129, 24);
            this.N2Account.Text = "Учетная запись";
            // 
            // N21Change
            // 
            this.N21Change.Name = "N21Change";
            this.N21Change.Size = new System.Drawing.Size(244, 26);
            this.N21Change.Text = "Сменить пользователя";
            this.N21Change.Click += new System.EventHandler(this.N21Change_Click);
            // 
            // DlgOpenMain
            // 
            this.DlgOpenMain.DefaultExt = "txt";
            this.DlgOpenMain.FileName = "openFileDialog1";
            this.DlgOpenMain.Filter = "Текстовый файл|*.txt";
            // 
            // DlgSaveMain
            // 
            this.DlgSaveMain.DefaultExt = "txt";
            this.DlgSaveMain.Filter = "Текстовый файл|*.txt";
            // 
            // LAdmonition
            // 
            this.LAdmonition.AutoSize = true;
            this.LAdmonition.Location = new System.Drawing.Point(387, 31);
            this.LAdmonition.Name = "LAdmonition";
            this.LAdmonition.Size = new System.Drawing.Size(302, 51);
            this.LAdmonition.TabIndex = 2;
            this.LAdmonition.Text = "Введите судоку вручную или\r\nпри помощи файла. Затем выберите способ\r\nее решения и" +
    " после нажмите кнопку Ввод.";
            // 
            // LRecursively
            // 
            this.LRecursively.AutoSize = true;
            this.LRecursively.Location = new System.Drawing.Point(387, 112);
            this.LRecursively.Name = "LRecursively";
            this.LRecursively.Size = new System.Drawing.Size(243, 34);
            this.LRecursively.TabIndex = 5;
            this.LRecursively.Text = "неэкономный способ относительно\r\nпамяти и времени";
            this.LRecursively.Visible = false;
            // 
            // LSingles
            // 
            this.LSingles.AutoSize = true;
            this.LSingles.Location = new System.Drawing.Point(387, 112);
            this.LSingles.Name = "LSingles";
            this.LSingles.Size = new System.Drawing.Size(212, 34);
            this.LSingles.TabIndex = 6;
            this.LSingles.Text = "более экономный способ, чем \r\n\"Перебор всех цифр\"";
            this.LSingles.Visible = false;
            // 
            // BReset
            // 
            this.BReset.Location = new System.Drawing.Point(390, 253);
            this.BReset.Name = "BReset";
            this.BReset.Size = new System.Drawing.Size(299, 45);
            this.BReset.TabIndex = 7;
            this.BReset.Text = "Сброс";
            this.BReset.UseVisualStyleBackColor = true;
            this.BReset.Click += new System.EventHandler(this.BReset_Click);
            // 
            // BInput
            // 
            this.BInput.Location = new System.Drawing.Point(390, 304);
            this.BInput.Name = "BInput";
            this.BInput.Size = new System.Drawing.Size(299, 45);
            this.BInput.TabIndex = 8;
            this.BInput.Text = "Ввод";
            this.BInput.UseVisualStyleBackColor = true;
            this.BInput.Click += new System.EventHandler(this.BInput_Click);
            // 
            // BExit
            // 
            this.BExit.Location = new System.Drawing.Point(390, 355);
            this.BExit.Name = "BExit";
            this.BExit.Size = new System.Drawing.Size(299, 45);
            this.BExit.TabIndex = 9;
            this.BExit.Text = "Выход";
            this.BExit.UseVisualStyleBackColor = true;
            this.BExit.Click += new System.EventHandler(this.BExit_Click);
            // 
            // CBWay
            // 
            this.CBWay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBWay.FormattingEnabled = true;
            this.CBWay.Items.AddRange(new object[] {
            "Перебор всех цифр",
            "С поиском одиночек"});
            this.CBWay.Location = new System.Drawing.Point(390, 85);
            this.CBWay.Name = "CBWay";
            this.CBWay.Size = new System.Drawing.Size(299, 24);
            this.CBWay.TabIndex = 10;
            this.CBWay.SelectedIndexChanged += new System.EventHandler(this.CBWay_SelectedIndexChanged);
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
            this.DGV1.TabIndex = 11;
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 408);
            this.ControlBox = false;
            this.Controls.Add(this.DGV1);
            this.Controls.Add(this.CBWay);
            this.Controls.Add(this.BExit);
            this.Controls.Add(this.BInput);
            this.Controls.Add(this.BReset);
            this.Controls.Add(this.LSingles);
            this.Controls.Add(this.LRecursively);
            this.Controls.Add(this.LAdmonition);
            this.Controls.Add(this.MMMenu);
            this.MainMenuStrip = this.MMMenu;
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.Text = "Sudoku";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.Load += new System.EventHandler(this.FMain_Load);
            this.Shown += new System.EventHandler(this.FMain_Shown);
            this.MMMenu.ResumeLayout(false);
            this.MMMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MMMenu;
        private System.Windows.Forms.ToolStripMenuItem N1File;
        private System.Windows.Forms.ToolStripMenuItem N11Open;
        private System.Windows.Forms.ToolStripMenuItem N12Save;
        private System.Windows.Forms.ToolStripMenuItem N2Account;
        private System.Windows.Forms.ToolStripMenuItem N21Change;
        private System.Windows.Forms.OpenFileDialog DlgOpenMain;
        private System.Windows.Forms.SaveFileDialog DlgSaveMain;
        private System.Windows.Forms.Label LAdmonition;
        private System.Windows.Forms.Label LRecursively;
        private System.Windows.Forms.Label LSingles;
        private System.Windows.Forms.Button BReset;
        private System.Windows.Forms.Button BInput;
        private System.Windows.Forms.Button BExit;
        private System.Windows.Forms.ComboBox CBWay;
        private System.Windows.Forms.DataGridView DGV1;
    }
}

