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
            this.BPlay = new System.Windows.Forms.Button();
            this.BExit = new System.Windows.Forms.Button();
            this.BChangePlayer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BPlay
            // 
            this.BPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BPlay.Location = new System.Drawing.Point(227, 73);
            this.BPlay.Name = "BPlay";
            this.BPlay.Size = new System.Drawing.Size(176, 70);
            this.BPlay.TabIndex = 8;
            this.BPlay.Text = "Играть";
            this.BPlay.UseVisualStyleBackColor = true;
            this.BPlay.Click += new System.EventHandler(this.BPlay_Click);
            // 
            // BExit
            // 
            this.BExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BExit.Location = new System.Drawing.Point(227, 253);
            this.BExit.Name = "BExit";
            this.BExit.Size = new System.Drawing.Size(176, 70);
            this.BExit.TabIndex = 9;
            this.BExit.Text = "Выход";
            this.BExit.UseVisualStyleBackColor = true;
            this.BExit.Click += new System.EventHandler(this.BExit_Click);
            // 
            // BChangePlayer
            // 
            this.BChangePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BChangePlayer.Location = new System.Drawing.Point(227, 160);
            this.BChangePlayer.Name = "BChangePlayer";
            this.BChangePlayer.Size = new System.Drawing.Size(176, 70);
            this.BChangePlayer.TabIndex = 10;
            this.BChangePlayer.Text = "Сменить игрока";
            this.BChangePlayer.UseVisualStyleBackColor = true;
            this.BChangePlayer.Click += new System.EventHandler(this.BChangePlayer_Click);
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 407);
            this.ControlBox = false;
            this.Controls.Add(this.BChangePlayer);
            this.Controls.Add(this.BExit);
            this.Controls.Add(this.BPlay);
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.Text = "Главное меню";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BPlay;
        private System.Windows.Forms.Button BExit;
        private System.Windows.Forms.Button BChangePlayer;
    }
}

