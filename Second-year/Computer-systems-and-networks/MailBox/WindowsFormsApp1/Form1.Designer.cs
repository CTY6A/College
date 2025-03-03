namespace WindowsFormsApp1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox = new System.Windows.Forms.TextBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnAuthorizate = new System.Windows.Forms.Button();
            this.BtnStat = new System.Windows.Forms.Button();
            this.BtnQuit = new System.Windows.Forms.Button();
            this.BtnRetr = new System.Windows.Forms.Button();
            this.TextBoxMessage = new System.Windows.Forms.TextBox();
            this.BtnList = new System.Windows.Forms.Button();
            this.TextBoxList = new System.Windows.Forms.TextBox();
            this.BtnNoop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(-1, -3);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox.Size = new System.Drawing.Size(299, 524);
            this.TextBox.TabIndex = 0;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(354, 24);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(125, 45);
            this.BtnConnect.TabIndex = 1;
            this.BtnConnect.Text = "Подключиться к серверу";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnAuthorizate
            // 
            this.BtnAuthorizate.Location = new System.Drawing.Point(354, 75);
            this.BtnAuthorizate.Name = "BtnAuthorizate";
            this.BtnAuthorizate.Size = new System.Drawing.Size(125, 23);
            this.BtnAuthorizate.TabIndex = 2;
            this.BtnAuthorizate.Text = "Авторизация";
            this.BtnAuthorizate.UseVisualStyleBackColor = true;
            this.BtnAuthorizate.Click += new System.EventHandler(this.BtnAuthorizate_Click);
            // 
            // BtnStat
            // 
            this.BtnStat.Location = new System.Drawing.Point(354, 104);
            this.BtnStat.Name = "BtnStat";
            this.BtnStat.Size = new System.Drawing.Size(125, 69);
            this.BtnStat.TabIndex = 3;
            this.BtnStat.Text = "Получить количество сообщений";
            this.BtnStat.UseVisualStyleBackColor = true;
            this.BtnStat.Click += new System.EventHandler(this.BtnStat_Click);
            // 
            // BtnQuit
            // 
            this.BtnQuit.Location = new System.Drawing.Point(354, 375);
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.Size = new System.Drawing.Size(125, 45);
            this.BtnQuit.TabIndex = 4;
            this.BtnQuit.Text = "Закрыть соединение";
            this.BtnQuit.UseVisualStyleBackColor = true;
            this.BtnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
            // 
            // BtnRetr
            // 
            this.BtnRetr.Location = new System.Drawing.Point(354, 264);
            this.BtnRetr.Name = "BtnRetr";
            this.BtnRetr.Size = new System.Drawing.Size(125, 50);
            this.BtnRetr.TabIndex = 5;
            this.BtnRetr.Text = "Прочитать сообщение";
            this.BtnRetr.UseVisualStyleBackColor = true;
            this.BtnRetr.Click += new System.EventHandler(this.BtnRetr_Click);
            // 
            // TextBoxMessage
            // 
            this.TextBoxMessage.Location = new System.Drawing.Point(485, 264);
            this.TextBoxMessage.Multiline = true;
            this.TextBoxMessage.Name = "TextBoxMessage";
            this.TextBoxMessage.Size = new System.Drawing.Size(100, 50);
            this.TextBoxMessage.TabIndex = 6;
            // 
            // BtnList
            // 
            this.BtnList.Location = new System.Drawing.Point(354, 189);
            this.BtnList.Name = "BtnList";
            this.BtnList.Size = new System.Drawing.Size(125, 69);
            this.BtnList.TabIndex = 7;
            this.BtnList.Text = "Количество символов в сообщении";
            this.BtnList.UseVisualStyleBackColor = true;
            this.BtnList.Click += new System.EventHandler(this.BtnList_Click);
            // 
            // TextBoxList
            // 
            this.TextBoxList.Location = new System.Drawing.Point(485, 189);
            this.TextBoxList.Multiline = true;
            this.TextBoxList.Name = "TextBoxList";
            this.TextBoxList.Size = new System.Drawing.Size(100, 69);
            this.TextBoxList.TabIndex = 8;
            // 
            // BtnNoop
            // 
            this.BtnNoop.Location = new System.Drawing.Point(354, 320);
            this.BtnNoop.Name = "BtnNoop";
            this.BtnNoop.Size = new System.Drawing.Size(125, 45);
            this.BtnNoop.TabIndex = 9;
            this.BtnNoop.Text = "Проверить соединение";
            this.BtnNoop.UseVisualStyleBackColor = true;
            this.BtnNoop.Click += new System.EventHandler(this.BtnNoop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 519);
            this.Controls.Add(this.BtnNoop);
            this.Controls.Add(this.TextBoxList);
            this.Controls.Add(this.BtnList);
            this.Controls.Add(this.TextBoxMessage);
            this.Controls.Add(this.BtnRetr);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.BtnStat);
            this.Controls.Add(this.BtnAuthorizate);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.TextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnAuthorizate;
        private System.Windows.Forms.Button BtnStat;
        private System.Windows.Forms.Button BtnQuit;
        private System.Windows.Forms.Button BtnRetr;
        private System.Windows.Forms.TextBox TextBoxMessage;
        private System.Windows.Forms.Button BtnList;
        private System.Windows.Forms.TextBox TextBoxList;
        private System.Windows.Forms.Button BtnNoop;
    }
}

