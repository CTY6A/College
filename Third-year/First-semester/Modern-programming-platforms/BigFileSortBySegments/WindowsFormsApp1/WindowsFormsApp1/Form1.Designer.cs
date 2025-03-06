namespace WindowsFormsApp1
{
    partial class FormMain
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
            this.ButtonSortFile = new System.Windows.Forms.Button();
            this.ButtonGenerateFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ButtonSortFile
            // 
            this.ButtonSortFile.Location = new System.Drawing.Point(221, 50);
            this.ButtonSortFile.Name = "ButtonSortFile";
            this.ButtonSortFile.Size = new System.Drawing.Size(124, 59);
            this.ButtonSortFile.TabIndex = 0;
            this.ButtonSortFile.Text = "Сортировать файл";
            this.ButtonSortFile.UseVisualStyleBackColor = true;
            this.ButtonSortFile.Click += new System.EventHandler(this.ButtonSortFile_Click);
            // 
            // ButtonGenerateFile
            // 
            this.ButtonGenerateFile.Location = new System.Drawing.Point(24, 50);
            this.ButtonGenerateFile.Name = "ButtonGenerateFile";
            this.ButtonGenerateFile.Size = new System.Drawing.Size(138, 59);
            this.ButtonGenerateFile.TabIndex = 1;
            this.ButtonGenerateFile.Text = "Сгенерировать файл";
            this.ButtonGenerateFile.UseVisualStyleBackColor = true;
            this.ButtonGenerateFile.Click += new System.EventHandler(this.ButtonGenerateFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 149);
            this.Controls.Add(this.ButtonGenerateFile);
            this.Controls.Add(this.ButtonSortFile);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSortFile;
        private System.Windows.Forms.Button ButtonGenerateFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

