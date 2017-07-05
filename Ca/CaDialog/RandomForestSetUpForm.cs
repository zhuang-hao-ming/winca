using Ca.CommonClass;
using Ca.CommonDialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ca.CaDialog
{
    public partial class RandomForestSetUpForm : Form
    {
        #region properties
        /// <summary>
        /// 目标城市栅格数目
        /// </summary>
        public int TargetCityCnt { get; set; }
        /// <summary>
        /// 各种土地利用类型互相转换的控制矩阵
        /// </summary>
        public int[,] TransformControlMatrix { get; set; }

        public bool IsNeedSignificant { get; set; }
        /// <summary>
        /// 起始图层名
        /// </summary>
        public string BeginLayerName { get; set; }
        /// <summary>
        /// 终止图层名
        /// </summary>
        public string EndLayerName { get; set; }
        /// <summary>
        /// 驱动因素图层名列表
        /// </summary>
        public List<string> DriveLayerNames { get; set; }
        /// <summary>
        /// 模拟次数
        /// </summary>
        public int NumOfSimulate { get; set; }
        /// <summary>
        /// 采样率
        /// </summary>
        public double RateOfSample { get; set; }
        /// <summary>
        /// 邻域大小
        /// </summary>
        public int SizeOfNeighbour { get; set; }
        /// <summary>
        /// 树的数目
        /// </summary>
        public int NumOfTree { get; set; }
        /// <summary>
        /// 训练一棵树使用的数据比例
        /// </summary>
        public double SampleRatio { get; set; }
        /// <summary>
        /// 训练一棵树覆盖的属性比例
        /// </summary>
        public double CoverageRatio { get; set; }
        /// <summary>
        /// 土地利用和栅格数值对应关系
        /// </summary>
        public LandUseClassificationInfo LandUse { get; set; }

        public double RandomFactor { get; set; }

        public int NumOfSample { get; set; }

        public double cityPropAdjust { get; set; }
        #endregion

        #region constructor
        public RandomForestSetUpForm()
        {
            InitializeComponent();
        }
        #endregion

        private void buttonOpenStart_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxStartPath.Text = fileDialog.FileName;
            }
        }

        private void buttonOpenEnd_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxEndPath.Text = fileDialog.FileName;
            }
        }

        private void buttonAddDrive_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in fileDialog.FileNames)
                {
                    this.listBoxDriveLayerNames.Items.Add(filename);
                }
            }
        }

        private void buttonRemoveDrive_Click(object sender, EventArgs e)
        {
            int selected = this.listBoxDriveLayerNames.SelectedIndex;
            if (selected != -1)
            {
                this.listBoxDriveLayerNames.Items.RemoveAt(selected);
            }
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            var form = new LandUseSetUpForm();
            form.ShowDialog();
            this.LandUse = form.LandUse;
        }

        private void buttonSetProperties_Click(object sender, EventArgs e)
        {
            try
            {
                this.BeginLayerName = this.textBoxStartPath.Text;
                this.EndLayerName = this.textBoxEndPath.Text;

                string[] list = new string[this.listBoxDriveLayerNames.Items.Count];
                this.listBoxDriveLayerNames.Items.CopyTo(list, 0);
                this.DriveLayerNames = list.ToList<string>();
                // 样本率
                this.NumOfSample = int.Parse(this.textBoxNumOfSample.Text);
                // ca参数
                this.NumOfSimulate = int.Parse(this.textBoxNumOfSimulate.Text);
                this.SizeOfNeighbour = int.Parse(this.textBoxSizeOfNeighbour.Text);
                this.RandomFactor = Double.Parse(this.textBoxRandomFactor.Text);
                this.TargetCityCnt = int.Parse(this.textBoxTargetCityCellCnt.Text);
                // 随机森林参数
                this.NumOfTree = int.Parse(this.textBoxNumOfTree.Text);
                this.SampleRatio = double.Parse(this.textBoxSampleRatio.Text);
                this.IsNeedSignificant = this.checkBoxNeedSignificants.Checked;
                this.DialogResult = DialogResult.OK;
                this.Close();
                // 
                this.cityPropAdjust = int.Parse(this.textBoxAdjust.Text);

            }
            catch(Exception ex)
            {
                this.DialogResult = DialogResult.No;
                this.Close();    
            }
            
            

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void buttonTransformControlSet_Click(object sender, EventArgs e)
        {
            if(this.LandUse == null)
            {
                MessageBox.Show("请先设置土地利用类型值对应表");
                return;
            }
            var form = new TransformControlForm(this.LandUse);
            form.ShowDialog();
            this.TransformControlMatrix = form.ControlMatrix;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
