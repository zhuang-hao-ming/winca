namespace Ca.CommonDialog
{
    partial class TransformControlForm
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
            this.dataGridViewControl = new System.Windows.Forms.DataGridView();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewControl)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewControl
            // 
            this.dataGridViewControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewControl.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewControl.Name = "dataGridViewControl";
            this.dataGridViewControl.RowTemplate.Height = 23;
            this.dataGridViewControl.Size = new System.Drawing.Size(711, 430);
            this.dataGridViewControl.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(591, 454);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // TransformControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 489);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.dataGridViewControl);
            this.Name = "TransformControlForm";
            this.Text = "TransformControlForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewControl;
        private System.Windows.Forms.Button buttonOk;
    }
}