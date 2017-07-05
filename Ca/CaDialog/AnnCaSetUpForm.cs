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
    public partial class AnnCaSetUpForm : Form
    {
        #region properties
        public string beginLayerName { get; set; }
        public string endLayerName { get; set; }
        public List<string> driveLayerNames { get; set; }
        public int NumOfTrain { get;set;}
        public int NumOfSample { get; set; }
        public double Threshold { get; set; }
        public double Alpha { get; set; }
        public int NumOfSimulate { get; set; }
        public int SizeOfNeighbour { get; set; }
        public LandUseClassificationInfo LandUse { get; set; }
        public int EndCityCnt { get; set; }
        /// <summary>
        /// 各种土地利用类型互相转换的控制矩阵
        /// </summary>
        public int[,] TransformControlMatrix { get; set; }

        #endregion

        #region constructor

        public AnnCaSetUpForm()
        {
            InitializeComponent();
        }
        #endregion

        #region event handlers
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

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                this.NumOfSample = int.Parse(this.textBoxNumOfSample.Text);
                this.NumOfTrain = int.Parse(this.textBoxNumOfTrain.Text);
                this.NumOfSimulate = int.Parse(this.textBoxNumOfSimulate.Text);
                this.Threshold = Double.Parse(this.textBoxThreshold.Text);
                this.Alpha = Double.Parse(this.textBoxAlpha.Text);
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

        private void buttonSet_Click(object sender, EventArgs e)
        {
            var form = new LandUseSetUpForm();
            form.ShowDialog();
            this.LandUse = form.LandUse;
        }
        private void buttonTransformControlSet_Click(object sender, EventArgs e)
        {
            if (this.LandUse == null)
            {
                MessageBox.Show("请先设置土地利用类型值对应表");
                return;
            }
            var form = new TransformControlForm(this.LandUse);
            form.ShowDialog();
            this.TransformControlMatrix = form.ControlMatrix;
        }
        #endregion


    }
}
