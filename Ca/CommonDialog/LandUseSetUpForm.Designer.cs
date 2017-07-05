namespace Ca.CommonDialog
{
    partial class LandUseSetUpForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewLandUse = new System.Windows.Forms.DataGridView();
            this.columnLandValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLandUse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripLandUse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.城市ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.水体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.森林ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.果园ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.农田ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.非城市用地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnLandProperty = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonTest = new System.Windows.Forms.Button();
            this.未利用地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.草地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLandUse)).BeginInit();
            this.contextMenuStripLandUse.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewLandUse);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 339);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "value of land use";
            // 
            // dataGridViewLandUse
            // 
            this.dataGridViewLandUse.AllowUserToAddRows = false;
            this.dataGridViewLandUse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLandUse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnLandValue,
            this.columnLandUse,
            this.columnLandProperty,
            this.columnColor});
            this.dataGridViewLandUse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewLandUse.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewLandUse.Name = "dataGridViewLandUse";
            this.dataGridViewLandUse.RowTemplate.Height = 23;
            this.dataGridViewLandUse.Size = new System.Drawing.Size(619, 319);
            this.dataGridViewLandUse.TabIndex = 0;
            this.dataGridViewLandUse.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewLandUse_CellMouseDown);
            // 
            // columnLandValue
            // 
            this.columnLandValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnLandValue.HeaderText = "Value";
            this.columnLandValue.Name = "columnLandValue";
            this.columnLandValue.ReadOnly = true;
            // 
            // columnLandUse
            // 
            this.columnLandUse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnLandUse.ContextMenuStrip = this.contextMenuStripLandUse;
            this.columnLandUse.HeaderText = "land use";
            this.columnLandUse.Name = "columnLandUse";
            this.columnLandUse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextMenuStripLandUse
            // 
            this.contextMenuStripLandUse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.城市ToolStripMenuItem,
            this.水体ToolStripMenuItem,
            this.森林ToolStripMenuItem,
            this.果园ToolStripMenuItem,
            this.空值ToolStripMenuItem,
            this.农田ToolStripMenuItem,
            this.非城市用地ToolStripMenuItem,
            this.未利用地ToolStripMenuItem,
            this.草地ToolStripMenuItem});
            this.contextMenuStripLandUse.Name = "contextMenuStripLandUse";
            this.contextMenuStripLandUse.Size = new System.Drawing.Size(153, 224);
            this.contextMenuStripLandUse.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStripLandUse_ItemClicked);
            // 
            // 城市ToolStripMenuItem
            // 
            this.城市ToolStripMenuItem.Name = "城市ToolStripMenuItem";
            this.城市ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.城市ToolStripMenuItem.Text = "城市";
            // 
            // 水体ToolStripMenuItem
            // 
            this.水体ToolStripMenuItem.Name = "水体ToolStripMenuItem";
            this.水体ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.水体ToolStripMenuItem.Text = "水体";
            // 
            // 森林ToolStripMenuItem
            // 
            this.森林ToolStripMenuItem.Name = "森林ToolStripMenuItem";
            this.森林ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.森林ToolStripMenuItem.Text = "森林";
            // 
            // 果园ToolStripMenuItem
            // 
            this.果园ToolStripMenuItem.Name = "果园ToolStripMenuItem";
            this.果园ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.果园ToolStripMenuItem.Text = "果园";
            // 
            // 空值ToolStripMenuItem
            // 
            this.空值ToolStripMenuItem.Name = "空值ToolStripMenuItem";
            this.空值ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.空值ToolStripMenuItem.Text = "空值";
            // 
            // 农田ToolStripMenuItem
            // 
            this.农田ToolStripMenuItem.Name = "农田ToolStripMenuItem";
            this.农田ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.农田ToolStripMenuItem.Text = "农田";
            // 
            // 非城市用地ToolStripMenuItem
            // 
            this.非城市用地ToolStripMenuItem.Name = "非城市用地ToolStripMenuItem";
            this.非城市用地ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.非城市用地ToolStripMenuItem.Text = "非城市用地";
            // 
            // columnLandProperty
            // 
            this.columnLandProperty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.columnLandProperty.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnLandProperty.HeaderText = "Property";
            this.columnLandProperty.Name = "columnLandProperty";
            // 
            // columnColor
            // 
            this.columnColor.HeaderText = "Color";
            this.columnColor.Name = "columnColor";
            this.columnColor.ReadOnly = true;
            this.columnColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnColor.Width = 128;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(475, 366);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(568, 366);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(383, 366);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "加载";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(290, 366);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 3;
            this.buttonTest.Text = "测试";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // 未利用地ToolStripMenuItem
            // 
            this.未利用地ToolStripMenuItem.Name = "未利用地ToolStripMenuItem";
            this.未利用地ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.未利用地ToolStripMenuItem.Text = "未利用地";
            // 
            // 草地ToolStripMenuItem
            // 
            this.草地ToolStripMenuItem.Name = "草地ToolStripMenuItem";
            this.草地ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.草地ToolStripMenuItem.Text = "草地";
            // 
            // LandUseSetUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 413);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Name = "LandUseSetUpForm";
            this.Text = "LandUseSetUpForm";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLandUse)).EndInit();
            this.contextMenuStripLandUse.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewLandUse;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLandUse;
        private System.Windows.Forms.ToolStripMenuItem 城市ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 水体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 森林ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 果园ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空值ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLandValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLandUse;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnLandProperty;
        private System.Windows.Forms.DataGridViewImageColumn columnColor;
        private System.Windows.Forms.ToolStripMenuItem 农田ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 非城市用地ToolStripMenuItem;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.ToolStripMenuItem 未利用地ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 草地ToolStripMenuItem;
    }
}