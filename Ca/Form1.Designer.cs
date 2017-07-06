namespace Ca
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.randomForestCa = new System.Windows.Forms.Button();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.logisticCa = new System.Windows.Forms.Button();
            this.dtCa = new System.Windows.Forms.Button();
            this.annCa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // randomForestCa
            // 
            this.randomForestCa.Location = new System.Drawing.Point(45, 26);
            this.randomForestCa.Name = "randomForestCa";
            this.randomForestCa.Size = new System.Drawing.Size(145, 54);
            this.randomForestCa.TabIndex = 0;
            this.randomForestCa.Text = "随机森林ca";
            this.randomForestCa.UseVisualStyleBackColor = true;
            this.randomForestCa.Click += new System.EventHandler(this.randomForestCa_Click);
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.Location = new System.Drawing.Point(45, 217);
            this.textBoxConsole.Multiline = true;
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.Size = new System.Drawing.Size(642, 377);
            this.textBoxConsole.TabIndex = 1;
            // 
            // logisticCa
            // 
            this.logisticCa.Location = new System.Drawing.Point(380, 27);
            this.logisticCa.Name = "logisticCa";
            this.logisticCa.Size = new System.Drawing.Size(145, 53);
            this.logisticCa.TabIndex = 2;
            this.logisticCa.Text = "逻辑回归ca";
            this.logisticCa.UseVisualStyleBackColor = true;
            this.logisticCa.Click += new System.EventHandler(this.logisticCa_Click);
            // 
            // dtCa
            // 
            this.dtCa.Location = new System.Drawing.Point(542, 27);
            this.dtCa.Name = "dtCa";
            this.dtCa.Size = new System.Drawing.Size(145, 53);
            this.dtCa.TabIndex = 3;
            this.dtCa.Text = "决策树ca";
            this.dtCa.UseVisualStyleBackColor = true;
            this.dtCa.Click += new System.EventHandler(this.dtCa_Click);
            // 
            // annCa
            // 
            this.annCa.Location = new System.Drawing.Point(211, 27);
            this.annCa.Name = "annCa";
            this.annCa.Size = new System.Drawing.Size(145, 53);
            this.annCa.TabIndex = 4;
            this.annCa.Text = "神经网络ca";
            this.annCa.UseVisualStyleBackColor = true;
            this.annCa.Click += new System.EventHandler(this.annCa_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 606);
            this.Controls.Add(this.annCa);
            this.Controls.Add(this.dtCa);
            this.Controls.Add(this.logisticCa);
            this.Controls.Add(this.textBoxConsole);
            this.Controls.Add(this.randomForestCa);
            this.Name = "Form1";
            this.Text = "winca";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button randomForestCa;
        private System.Windows.Forms.TextBox textBoxConsole;
        private System.Windows.Forms.Button logisticCa;
        private System.Windows.Forms.Button dtCa;
        private System.Windows.Forms.Button annCa;
    }
}

