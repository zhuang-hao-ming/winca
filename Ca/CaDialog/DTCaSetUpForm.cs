using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ca.CommonClass;
using Ca.CommonDialog;

namespace Ca.CaDialog
{
    public partial class DTCaSetUpForm : Form
    {

        #region properties
        /// <summary>
        /// 目标城市栅格数目
        /// </summary>
        public int EndCityCnt { get; set; }
        /// <summary>
        /// 起始图层名
        /// </summary>
        public string beginLayerName { get; set; }
        /// <summary>
        /// 终止图层名
        /// </summary>
        public string endLayerName { get; set; }
        /// <summary>
        /// 驱动因素图层名列表
        /// </summary>
        public List<string> driveLayerNames { get; set; }
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
        /// 土地利用和栅格数值对应关系
        /// </summary>
        public LandUseClassificationInfo LandUse { get; set; }

        #endregion

        #region constructor
        public DTCaSetUpForm()
        {
            InitializeComponent();
        }
        #endregion

        #region event handlers
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
            this.DialogResult = DialogResult.No;
            try
            {
                this.beginLayerName = this.textBoxStartPath.Text;
                this.endLayerName = this.textBoxEndPath.Text;

                string[] list = new string[this.listBoxDriveLayerNames.Items.Count];
                this.listBoxDriveLayerNames.Items.CopyTo(list, 0);
                this.driveLayerNames = list.ToList<string>();

                this.RateOfSample = double.Parse(this.textBoxRateOfSample.Text);
                this.NumOfSimulate = int.Parse(this.textBoxNumOfSimulate.Text);
                this.SizeOfNeighbour = int.Parse(this.textBoxSizeOfNeighbour.Text);
                this.EndCityCnt = int.Parse(this.textBoxEndCityCnt.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常" + ex.ToString());
                this.DialogResult = DialogResult.No;
            }
            

        }
        #endregion
    }
}
