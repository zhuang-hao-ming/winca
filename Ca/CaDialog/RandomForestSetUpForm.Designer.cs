namespace Ca.CaDialog
{
    partial class RandomForestSetUpForm
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonSet = new System.Windows.Forms.Button();
            this.buttonSetProperties = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxTargetCityCellCnt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxAdjust = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxRandomFactor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxNeedSignificants = new System.Windows.Forms.CheckBox();
            this.textBoxSampleRatio = new System.Windows.Forms.TextBox();
            this.textBoxNumOfTree = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSampleRatio = new System.Windows.Forms.Label();
            this.labelNumOfTree = new System.Windows.Forms.Label();
            this.textBoxNumOfSample = new System.Windows.Forms.TextBox();
            this.textBoxSizeOfNeighbour = new System.Windows.Forms.TextBox();
            this.textBoxNumOfSimulate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonOpenEnd = new System.Windows.Forms.Button();
            this.buttonOpenStart = new System.Windows.Forms.Button();
            this.textBoxEndPath = new System.Windows.Forms.TextBox();
            this.textBoxStartPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveDrive = new System.Windows.Forms.Button();
            this.buttonAddDrive = new System.Windows.Forms.Button();
            this.listBoxDriveLayerNames = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonTransformControlSet = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonSet);
            this.groupBox4.Location = new System.Drawing.Point(31, 629);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(256, 59);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "土地利用类型对应关系";
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(69, 22);
            this.buttonSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(100, 29);
            this.buttonSet.TabIndex = 0;
            this.buttonSet.Text = "设置";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // buttonSetProperties
            // 
            this.buttonSetProperties.Location = new System.Drawing.Point(412, 696);
            this.buttonSetProperties.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSetProperties.Name = "buttonSetProperties";
            this.buttonSetProperties.Size = new System.Drawing.Size(100, 29);
            this.buttonSetProperties.TabIndex = 13;
            this.buttonSetProperties.Text = "确定";
            this.buttonSetProperties.UseVisualStyleBackColor = true;
            this.buttonSetProperties.Click += new System.EventHandler(this.buttonSetProperties_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxTargetCityCellCnt);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBoxAdjust);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxRandomFactor);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.checkBoxNeedSignificants);
            this.groupBox3.Controls.Add(this.textBoxSampleRatio);
            this.groupBox3.Controls.Add(this.textBoxNumOfTree);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.labelSampleRatio);
            this.groupBox3.Controls.Add(this.labelNumOfTree);
            this.groupBox3.Controls.Add(this.textBoxNumOfSample);
            this.groupBox3.Controls.Add(this.textBoxSizeOfNeighbour);
            this.groupBox3.Controls.Add(this.textBoxNumOfSimulate);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(31, 368);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(508, 238);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数";
            // 
            // textBoxTargetCityCellCnt
            // 
            this.textBoxTargetCityCellCnt.Location = new System.Drawing.Point(365, 172);
            this.textBoxTargetCityCellCnt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxTargetCityCellCnt.Name = "textBoxTargetCityCellCnt";
            this.textBoxTargetCityCellCnt.Size = new System.Drawing.Size(105, 25);
            this.textBoxTargetCityCellCnt.TabIndex = 10;
            this.textBoxTargetCityCellCnt.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 172);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "目标城市栅格数目";
            // 
            // textBoxAdjust
            // 
            this.textBoxAdjust.Location = new System.Drawing.Point(365, 130);
            this.textBoxAdjust.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxAdjust.Name = "textBoxAdjust";
            this.textBoxAdjust.Size = new System.Drawing.Size(105, 25);
            this.textBoxAdjust.TabIndex = 8;
            this.textBoxAdjust.Text = "0";
            this.textBoxAdjust.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 138);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "城市发展概率修正";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBoxRandomFactor
            // 
            this.textBoxRandomFactor.Location = new System.Drawing.Point(83, 162);
            this.textBoxRandomFactor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxRandomFactor.Name = "textBoxRandomFactor";
            this.textBoxRandomFactor.Size = new System.Drawing.Size(105, 25);
            this.textBoxRandomFactor.TabIndex = 6;
            this.textBoxRandomFactor.Text = "2";
            this.textBoxRandomFactor.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 165);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "随机因子";
            // 
            // checkBoxNeedSignificants
            // 
            this.checkBoxNeedSignificants.AutoSize = true;
            this.checkBoxNeedSignificants.Location = new System.Drawing.Point(364, 89);
            this.checkBoxNeedSignificants.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxNeedSignificants.Name = "checkBoxNeedSignificants";
            this.checkBoxNeedSignificants.Size = new System.Drawing.Size(18, 17);
            this.checkBoxNeedSignificants.TabIndex = 4;
            this.checkBoxNeedSignificants.UseVisualStyleBackColor = true;
            // 
            // textBoxSampleRatio
            // 
            this.textBoxSampleRatio.Location = new System.Drawing.Point(365, 40);
            this.textBoxSampleRatio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSampleRatio.Name = "textBoxSampleRatio";
            this.textBoxSampleRatio.Size = new System.Drawing.Size(101, 25);
            this.textBoxSampleRatio.TabIndex = 3;
            this.textBoxSampleRatio.Text = "0.66";
            // 
            // textBoxNumOfTree
            // 
            this.textBoxNumOfTree.Location = new System.Drawing.Point(83, 205);
            this.textBoxNumOfTree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNumOfTree.Name = "textBoxNumOfTree";
            this.textBoxNumOfTree.Size = new System.Drawing.Size(105, 25);
            this.textBoxNumOfTree.TabIndex = 3;
            this.textBoxNumOfTree.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "是否计算变量重要性";
            // 
            // labelSampleRatio
            // 
            this.labelSampleRatio.AutoSize = true;
            this.labelSampleRatio.Location = new System.Drawing.Point(214, 47);
            this.labelSampleRatio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSampleRatio.Name = "labelSampleRatio";
            this.labelSampleRatio.Size = new System.Drawing.Size(122, 15);
            this.labelSampleRatio.TabIndex = 2;
            this.labelSampleRatio.Text = "样本使用率(0-1)";
            // 
            // labelNumOfTree
            // 
            this.labelNumOfTree.AutoSize = true;
            this.labelNumOfTree.Location = new System.Drawing.Point(8, 208);
            this.labelNumOfTree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNumOfTree.Name = "labelNumOfTree";
            this.labelNumOfTree.Size = new System.Drawing.Size(52, 15);
            this.labelNumOfTree.TabIndex = 2;
            this.labelNumOfTree.Text = "树数目";
            // 
            // textBoxNumOfSample
            // 
            this.textBoxNumOfSample.Location = new System.Drawing.Point(83, 37);
            this.textBoxNumOfSample.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNumOfSample.Name = "textBoxNumOfSample";
            this.textBoxNumOfSample.Size = new System.Drawing.Size(105, 25);
            this.textBoxNumOfSample.TabIndex = 1;
            this.textBoxNumOfSample.Text = "10000";
            // 
            // textBoxSizeOfNeighbour
            // 
            this.textBoxSizeOfNeighbour.Location = new System.Drawing.Point(83, 128);
            this.textBoxSizeOfNeighbour.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSizeOfNeighbour.Name = "textBoxSizeOfNeighbour";
            this.textBoxSizeOfNeighbour.Size = new System.Drawing.Size(105, 25);
            this.textBoxSizeOfNeighbour.TabIndex = 1;
            this.textBoxSizeOfNeighbour.Text = "5";
            // 
            // textBoxNumOfSimulate
            // 
            this.textBoxNumOfSimulate.Location = new System.Drawing.Point(83, 86);
            this.textBoxNumOfSimulate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNumOfSimulate.Name = "textBoxNumOfSimulate";
            this.textBoxNumOfSimulate.Size = new System.Drawing.Size(105, 25);
            this.textBoxNumOfSimulate.TabIndex = 1;
            this.textBoxNumOfSimulate.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "采样数目";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(529, 40);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "训练次数";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 130);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "邻域大小";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 85);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "模拟次数";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonOpenEnd);
            this.groupBox2.Controls.Add(this.buttonOpenStart);
            this.groupBox2.Controls.Add(this.textBoxEndPath);
            this.groupBox2.Controls.Add(this.textBoxStartPath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(31, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(508, 142);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "起始和终止年份数据";
            // 
            // buttonOpenEnd
            // 
            this.buttonOpenEnd.Location = new System.Drawing.Point(381, 78);
            this.buttonOpenEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOpenEnd.Name = "buttonOpenEnd";
            this.buttonOpenEnd.Size = new System.Drawing.Size(100, 29);
            this.buttonOpenEnd.TabIndex = 2;
            this.buttonOpenEnd.Text = "打开";
            this.buttonOpenEnd.UseVisualStyleBackColor = true;
            this.buttonOpenEnd.Click += new System.EventHandler(this.buttonOpenEnd_Click);
            // 
            // buttonOpenStart
            // 
            this.buttonOpenStart.Location = new System.Drawing.Point(379, 36);
            this.buttonOpenStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOpenStart.Name = "buttonOpenStart";
            this.buttonOpenStart.Size = new System.Drawing.Size(100, 29);
            this.buttonOpenStart.TabIndex = 2;
            this.buttonOpenStart.Text = "打开";
            this.buttonOpenStart.UseVisualStyleBackColor = true;
            this.buttonOpenStart.Click += new System.EventHandler(this.buttonOpenStart_Click);
            // 
            // textBoxEndPath
            // 
            this.textBoxEndPath.Location = new System.Drawing.Point(127, 78);
            this.textBoxEndPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxEndPath.Name = "textBoxEndPath";
            this.textBoxEndPath.Size = new System.Drawing.Size(231, 25);
            this.textBoxEndPath.TabIndex = 1;
            // 
            // textBoxStartPath
            // 
            this.textBoxStartPath.Location = new System.Drawing.Point(127, 36);
            this.textBoxStartPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxStartPath.Name = "textBoxStartPath";
            this.textBoxStartPath.Size = new System.Drawing.Size(231, 25);
            this.textBoxStartPath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "起始年份影像";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "终止年份影像";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRemoveDrive);
            this.groupBox1.Controls.Add(this.buttonAddDrive);
            this.groupBox1.Controls.Add(this.listBoxDriveLayerNames);
            this.groupBox1.Location = new System.Drawing.Point(31, 165);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(509, 195);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "驱动数据";
            // 
            // buttonRemoveDrive
            // 
            this.buttonRemoveDrive.Location = new System.Drawing.Point(11, 119);
            this.buttonRemoveDrive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRemoveDrive.Name = "buttonRemoveDrive";
            this.buttonRemoveDrive.Size = new System.Drawing.Size(100, 29);
            this.buttonRemoveDrive.TabIndex = 2;
            this.buttonRemoveDrive.Text = "移除";
            this.buttonRemoveDrive.UseVisualStyleBackColor = true;
            this.buttonRemoveDrive.Click += new System.EventHandler(this.buttonRemoveDrive_Click);
            // 
            // buttonAddDrive
            // 
            this.buttonAddDrive.Location = new System.Drawing.Point(11, 62);
            this.buttonAddDrive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddDrive.Name = "buttonAddDrive";
            this.buttonAddDrive.Size = new System.Drawing.Size(100, 29);
            this.buttonAddDrive.TabIndex = 1;
            this.buttonAddDrive.Text = "添加";
            this.buttonAddDrive.UseVisualStyleBackColor = true;
            this.buttonAddDrive.Click += new System.EventHandler(this.buttonAddDrive_Click);
            // 
            // listBoxDriveLayerNames
            // 
            this.listBoxDriveLayerNames.FormattingEnabled = true;
            this.listBoxDriveLayerNames.ItemHeight = 15;
            this.listBoxDriveLayerNames.Location = new System.Drawing.Point(128, 25);
            this.listBoxDriveLayerNames.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxDriveLayerNames.Name = "listBoxDriveLayerNames";
            this.listBoxDriveLayerNames.Size = new System.Drawing.Size(352, 154);
            this.listBoxDriveLayerNames.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonTransformControlSet);
            this.groupBox5.Location = new System.Drawing.Point(307, 629);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(232, 59);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "转换控制矩阵";
            // 
            // buttonTransformControlSet
            // 
            this.buttonTransformControlSet.Location = new System.Drawing.Point(78, 22);
            this.buttonTransformControlSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonTransformControlSet.Name = "buttonTransformControlSet";
            this.buttonTransformControlSet.Size = new System.Drawing.Size(100, 29);
            this.buttonTransformControlSet.TabIndex = 0;
            this.buttonTransformControlSet.Text = "设置";
            this.buttonTransformControlSet.UseVisualStyleBackColor = true;
            this.buttonTransformControlSet.Click += new System.EventHandler(this.buttonTransformControlSet_Click);
            // 
            // RandomForestSetUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 738);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.buttonSetProperties);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RandomForestSetUpForm";
            this.Text = "RandomForestSetUpForm";
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Button buttonSetProperties;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxNumOfSample;
        private System.Windows.Forms.TextBox textBoxSizeOfNeighbour;
        private System.Windows.Forms.TextBox textBoxNumOfSimulate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOpenEnd;
        private System.Windows.Forms.Button buttonOpenStart;
        private System.Windows.Forms.TextBox textBoxEndPath;
        private System.Windows.Forms.TextBox textBoxStartPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRemoveDrive;
        private System.Windows.Forms.Button buttonAddDrive;
        private System.Windows.Forms.ListBox listBoxDriveLayerNames;
        private System.Windows.Forms.TextBox textBoxNumOfTree;
        private System.Windows.Forms.Label labelNumOfTree;
        private System.Windows.Forms.TextBox textBoxSampleRatio;
        private System.Windows.Forms.Label labelSampleRatio;
        private System.Windows.Forms.CheckBox checkBoxNeedSignificants;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRandomFactor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAdjust;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonTransformControlSet;
        private System.Windows.Forms.TextBox textBoxTargetCityCellCnt;
        private System.Windows.Forms.Label label9;
    }
}