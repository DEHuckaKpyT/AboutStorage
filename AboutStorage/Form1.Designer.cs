namespace AboutStorage
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxWaitingProcesses = new System.Windows.Forms.ListBox();
            this.listBoxProcessingProcesses = new System.Windows.Forms.ListBox();
            this.listBoxExecutedProcesses = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.listBoxAllProcesses = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(501, 501);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(777, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "сгенерировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(669, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "78";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(540, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Количество сегментов";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(735, 83);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 42);
            this.button2.TabIndex = 4;
            this.button2.Text = "запустить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(566, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // listBoxWaitingProcesses
            // 
            this.listBoxWaitingProcesses.FormattingEnabled = true;
            this.listBoxWaitingProcesses.Location = new System.Drawing.Point(526, 188);
            this.listBoxWaitingProcesses.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxWaitingProcesses.Name = "listBoxWaitingProcesses";
            this.listBoxWaitingProcesses.Size = new System.Drawing.Size(216, 329);
            this.listBoxWaitingProcesses.TabIndex = 6;
            // 
            // listBoxProcessingProcesses
            // 
            this.listBoxProcessingProcesses.FormattingEnabled = true;
            this.listBoxProcessingProcesses.Location = new System.Drawing.Point(746, 188);
            this.listBoxProcessingProcesses.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxProcessingProcesses.Name = "listBoxProcessingProcesses";
            this.listBoxProcessingProcesses.Size = new System.Drawing.Size(216, 329);
            this.listBoxProcessingProcesses.TabIndex = 7;
            // 
            // listBoxExecutedProcesses
            // 
            this.listBoxExecutedProcesses.FormattingEnabled = true;
            this.listBoxExecutedProcesses.Location = new System.Drawing.Point(965, 188);
            this.listBoxExecutedProcesses.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxExecutedProcesses.Name = "listBoxExecutedProcesses";
            this.listBoxExecutedProcesses.Size = new System.Drawing.Size(216, 329);
            this.listBoxExecutedProcesses.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 168);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(568, 96);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 29);
            this.button3.TabIndex = 10;
            this.button3.Text = "пауза/пуск";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBoxAllProcesses
            // 
            this.listBoxAllProcesses.FormattingEnabled = true;
            this.listBoxAllProcesses.Location = new System.Drawing.Point(1185, 188);
            this.listBoxAllProcesses.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxAllProcesses.Name = "listBoxAllProcesses";
            this.listBoxAllProcesses.Size = new System.Drawing.Size(216, 329);
            this.listBoxAllProcesses.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1182, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Все процессы";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 550);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBoxAllProcesses);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxExecutedProcesses);
            this.Controls.Add(this.listBoxProcessingProcesses);
            this.Controls.Add(this.listBoxWaitingProcesses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxWaitingProcesses;
        private System.Windows.Forms.ListBox listBoxProcessingProcesses;
        private System.Windows.Forms.ListBox listBoxExecutedProcesses;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBoxAllProcesses;
        private System.Windows.Forms.Label label4;
    }
}

