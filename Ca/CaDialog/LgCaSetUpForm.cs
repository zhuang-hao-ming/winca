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
    public partial class LgCaSetUpForm : Form
    {
        #region constructor
        public LgCaSetUpForm()
        {
            InitializeComponent();
        }
        #endregion

        #region property
        public string BeginLayerName { get; set; }
        public string EndLayerName { get; set; }
        public List<string> DriveLayerNames { get; set; }
        public LandUseClassificationInfo LandUseInfo { get; set; }        
        public int SizeOfNeighbour { get; set; }
        public double Threshold { get; set; }
        public double Alpha { get; set; }
        public int TimeOfSimulate { get; set; }
        public int NumOfSample { get; set; }
        public int EndCityCnt { get; set; }
        #endregion

        #region event handler
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
            this.LandUseInfo = form.LandUse;
        }

        private void buttonSetProperties_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;

            try
            {
                this.BeginLayerName = this.textBoxStartPath.Text;
                this.EndLayerName = this.textBoxEndPath.Text;

                string[] items = new string[this.listBoxDriveLayerNames.Items.Count];
                this.listBoxDriveLayerNames.Items.CopyTo(items, 0);
                this.DriveLayerNames = items.ToList<string>();

                this.SizeOfNeighbour = int.Parse(this.textBoxSizeOfNeighbour.Text);
                this.Threshold = Double.Parse(this.textBoxThreshold.Text);
                this.Alpha = double.Parse(this.textBoxAlpha.Text);
                this.TimeOfSimulate = int.Parse(this.textBoxTimeOfSimulate.Text);
                this.NumOfSample = int.Parse(this.textBoxNumOfSample.Text);
                this.EndCityCnt = int.Parse(this.textBoxEndCityCnt.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置不正确\n" + ex.ToString());
                this.DialogResult = DialogResult.No;
            }
        }
        #endregion
    }
}
