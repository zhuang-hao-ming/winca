using Ca.CommonClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ca.CommonDialog
{
    public partial class TransformControlForm : Form
    {
        #region properties
        public LandUseClassificationInfo LandUseInfo { get; set; }
        public int[,] ControlMatrix { get; set; }
        #endregion
        public TransformControlForm()
        {
            InitializeComponent();
        }
        public TransformControlForm(LandUseClassificationInfo landUseInfo)
        {
            this.LandUseInfo = landUseInfo;
            InitializeComponent();
            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            for(int i = 0; i < LandUseInfo.AllTypes.Count; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.ReadOnly = false;
                column.HeaderText = LandUseInfo.AllTypes[i].LandUseTypeName;
                dataGridViewControl.Columns.Add(column);
                DataGridViewRow row = new DataGridViewRow();
                row.HeaderCell.Value = LandUseInfo.AllTypes[i].LandUseTypeName;
                dataGridViewControl.Rows.Add(row);
            }
            dataGridViewControl.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            DataGridViewTextBoxCell cell = null;
            
            for (int i = 0; i < LandUseInfo.AllTypes.Count; i++)
            {
                for(int j = 0; j < LandUseInfo.AllTypes.Count; j++)
                {
                    cell = dataGridViewControl.Rows[i].Cells[j] as DataGridViewTextBoxCell;
                    cell.Value = "1";
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int len = this.LandUseInfo.NumOfLandUseTypes;
            int[,] controlMatix = new int[len, len];
            for (int row = 0; row < len; row++)
            {
                for (int col = 0; col < len; col++)
                {
                    controlMatix[row, col] = int.Parse(dataGridViewControl.Rows[row].Cells[col].Value.ToString()) ;
                }
            }
            this.ControlMatrix = controlMatix;
            this.Close();
        }
    }
}
