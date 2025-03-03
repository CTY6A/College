namespace Курсач
{
    partial class FAuthorization
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
            this.LChooseNickname = new System.Windows.Forms.Label();
            this.TBNickname = new System.Windows.Forms.MaskedTextBox();
            this.CBColor = new System.Windows.Forms.ComboBox();
            this.LChooseColor = new System.Windows.Forms.Label();
            this.BChoose = new System.Windows.Forms.Button();
            this.BCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LChooseNickname
            // 
            this.LChooseNickname.AutoSize = true;
            this.LChooseNickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LChooseNickname.Location = new System.Drawing.Point(12, 9);
            this.LChooseNickname.Name = "LChooseNickname";
            this.LChooseNickname.Size = new System.Drawing.Size(259, 29);
            this.LChooseNickname.TabIndex = 0;
            this.LChooseNickname.Text = "Выберите прозвище:";
            // 
            // TBNickname
            // 
            this.TBNickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBNickname.Location = new System.Drawing.Point(17, 41);
            this.TBNickname.Name = "TBNickname";
            this.TBNickname.Size = new System.Drawing.Size(254, 34);
            this.TBNickname.TabIndex = 1;
            // 
            // CBColor
            // 
            this.CBColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBColor.FormattingEnabled = true;
            this.CBColor.Location = new System.Drawing.Point(17, 144);
            this.CBColor.Name = "CBColor";
            this.CBColor.Size = new System.Drawing.Size(254, 35);
            this.CBColor.TabIndex = 2;
            this.CBColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CBColor_DrawItem);
            // 
            // LChooseColor
            // 
            this.LChooseColor.AutoSize = true;
            this.LChooseColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LChooseColor.Location = new System.Drawing.Point(12, 99);
            this.LChooseColor.Name = "LChooseColor";
            this.LChooseColor.Size = new System.Drawing.Size(195, 29);
            this.LChooseColor.TabIndex = 3;
            this.LChooseColor.Text = "Выберите цвет:";
            // 
            // BChoose
            // 
            this.BChoose.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BChoose.Location = new System.Drawing.Point(17, 208);
            this.BChoose.Name = "BChoose";
            this.BChoose.Size = new System.Drawing.Size(254, 55);
            this.BChoose.TabIndex = 4;
            this.BChoose.Text = "Выбрать";
            this.BChoose.UseVisualStyleBackColor = true;
            this.BChoose.Click += new System.EventHandler(this.BChoose_Click);
            // 
            // BCancel
            // 
            this.BCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BCancel.Location = new System.Drawing.Point(17, 287);
            this.BCancel.Name = "BCancel";
            this.BCancel.Size = new System.Drawing.Size(254, 56);
            this.BCancel.TabIndex = 5;
            this.BCancel.Text = "Отмена";
            this.BCancel.UseVisualStyleBackColor = true;
            this.BCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // FAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 396);
            this.ControlBox = false;
            this.Controls.Add(this.BCancel);
            this.Controls.Add(this.BChoose);
            this.Controls.Add(this.LChooseColor);
            this.Controls.Add(this.CBColor);
            this.Controls.Add(this.TBNickname);
            this.Controls.Add(this.LChooseNickname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FAuthorization";
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.FAuthorization_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LChooseNickname;
        private System.Windows.Forms.MaskedTextBox TBNickname;
        private System.Windows.Forms.ComboBox CBColor;
        private System.Windows.Forms.Label LChooseColor;
        private System.Windows.Forms.Button BChoose;
        private System.Windows.Forms.Button BCancel;
    }
}