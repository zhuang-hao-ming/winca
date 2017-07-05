using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ca.CommonClass;

namespace Ca.CommonDialog
{
    /// <summary>
    /// 土地利用信息设置form
    /// </summary>
    public partial class LandUseSetUpForm : Form
    {

        #region fields

        double[] buffer = null;
        int width = 0;
        int height = 0;
        LandUseClassificationInfo landUse = null;

        int count = 0;
        #endregion

        #region constructor

        public LandUseSetUpForm()
        {
            InitializeComponent();
        }

        #endregion

        #region private methods
        /// <summary>
        /// 和GetData功能相同，但是使用GDAL直接从文件中读取数据。
        /// </summary>
        /// <param name="fileName">文件名字</param>
        /// <param name="width">ref用于返回数据的宽度</param>
        /// <param name="height">ref用于返回数据的高度</param>
        /// <returns>一维数据数组,按行优先</returns>
        private double[] GdalGetData(string fileName, ref int width, ref int height)
        {
            OSGeo.GDAL.Dataset dataset = OSGeo.GDAL.Gdal.Open(fileName, OSGeo.GDAL.Access.GA_ReadOnly);
            width = dataset.RasterXSize;
            height = dataset.RasterYSize;
            double[] imageBuffer = new double[width * height];
            OSGeo.GDAL.Band b = dataset.GetRasterBand(1);
            b.ReadRaster(0, 0, width, height, imageBuffer, width, height, 0, 0);
            return imageBuffer;
        }

        #endregion

        #region event handlers
        /// <summary>
        /// 载入要设置的栅格数据，初始化设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var name = fileDialog.FileName;
                this.buffer = GdalGetData(name, ref this.width, ref this.height);
            }
            UpdateDataGridView();
        }

        private void dataGridViewLandUse_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.ColumnIndex == 3)
                {
                    dataGridViewLandUse.ClearSelection();
                    dataGridViewLandUse.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    if (this.colorDialog1.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewLandUse.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = GetBitmap(15, 15, colorDialog1.Color);
                    }
                }
            }
        }

        private void contextMenuStripLandUse_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (dataGridViewLandUse.CurrentCell.ColumnIndex == 1)
            {
                dataGridViewLandUse.CurrentCell.Value = e.ClickedItem.Text;
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.landUse = new LandUseClassificationInfo();

            if (IsDataGridViewCellsNotNull(this.count, 4))
            {
                for (int i = 0; i < this.count; i++)
                {
                    string landUseType = dataGridViewLandUse.Rows[i].Cells[2].Value.ToString();
                    StructLanduseInfo info = new StructLanduseInfo();
                    info.LandUseTypeName = dataGridViewLandUse.Rows[i].Cells[1].Value.ToString();
                    info.LandUseTypeValue = Convert.ToDouble(dataGridViewLandUse.Rows[i].Cells[0].Value);
                    info.LandUseTypeColor = ((Bitmap)dataGridViewLandUse.Rows[i].Cells[3].Value).GetPixel(1, 1).ToArgb();
                    if (landUseType == "城市用地")
                    {
                        this.landUse.UrbanInfos.Add(info);
                        this.landUse.AllTypes.Add(info);
                    }
                    else if (landUseType == "可转换为城市用地")
                    {
                        this.landUse.ConvertableInfos.Add(info);
                        this.landUse.AllTypes.Add(info);
                    }
                    else if (landUseType == "不可转换为城市用地")
                    {
                        this.landUse.UnConvertableInfos.Add(info);
                        this.landUse.AllTypes.Add(info);
                    }
                    else if (landUseType == "数据空值")
                    {
                        this.landUse.NullInfo = info;
                    }

                }
                this.Close();
            }
            else
            {
                MessageBox.Show("数据不完整");
            }
        }
        /// <summary>
        /// 测试时，自动填充默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTest_Click(object sender, EventArgs e)
        {
            dataGridViewLandUse.Rows[0].Cells[1].Value = "农田";
            dataGridViewLandUse.Rows[0].Cells[2].Value = "可转换为城市用地";
            dataGridViewLandUse.Rows[0].Cells[3].Value = GetBitmap(15, 15, Color.Yellow);

            dataGridViewLandUse.Rows[1].Cells[1].Value = "森林";
            dataGridViewLandUse.Rows[1].Cells[2].Value = "可转换为城市用地";
            dataGridViewLandUse.Rows[1].Cells[3].Value = GetBitmap(15, 15, Color.Green);

            dataGridViewLandUse.Rows[2].Cells[1].Value = "草地";
            dataGridViewLandUse.Rows[2].Cells[2].Value = "可转换为城市用地";
            dataGridViewLandUse.Rows[2].Cells[3].Value = GetBitmap(15, 15, Color.LightGreen);

            dataGridViewLandUse.Rows[3].Cells[1].Value = "水体";
            dataGridViewLandUse.Rows[3].Cells[2].Value = "不可转换为城市用地";
            dataGridViewLandUse.Rows[3].Cells[3].Value = GetBitmap(15, 15, Color.Blue);

            dataGridViewLandUse.Rows[4].Cells[1].Value = "城市";
            dataGridViewLandUse.Rows[4].Cells[2].Value = "城市用地";
            dataGridViewLandUse.Rows[4].Cells[3].Value = GetBitmap(15, 15, Color.Black);

            dataGridViewLandUse.Rows[5].Cells[1].Value = "未利用地";
            dataGridViewLandUse.Rows[5].Cells[2].Value = "可转换为城市用地";
            dataGridViewLandUse.Rows[5].Cells[3].Value = GetBitmap(15, 15, Color.WhiteSmoke);

            dataGridViewLandUse.Rows[6].Cells[1].Value = "空值";
            dataGridViewLandUse.Rows[6].Cells[2].Value = "数据空值";
            dataGridViewLandUse.Rows[6].Cells[3].Value = GetBitmap(15, 15, Color.Transparent);



        }

        #endregion

        #region private methods



        private void UpdateDataGridView()
        {
            HashSet<double> set = new HashSet<double>();
            for (int i = 0; i < buffer.Length; i++)
            {
                if (set.Contains(this.buffer[i]))
                {
                    continue;
                }
                else
                {
                    set.Add(this.buffer[i]);
                }
                
            }

            List<double> landUseTypes = set.ToList<double>();
            landUseTypes.Sort();
            this.count = landUseTypes.Count;
            while (dataGridViewLandUse.Rows.Count > 0)
            {
                dataGridViewLandUse.Rows.RemoveAt(dataGridViewLandUse.Rows.Count - 1);
            }
            for (int i = 0; i < count; i++)
            {
                dataGridViewLandUse.Rows.Add();
                dataGridViewLandUse.Rows[i].Cells[0].Value = landUseTypes[i];
            }

            this.columnLandProperty.Items.AddRange(new object[] {
                "城市用地",
                "可转换为城市用地",
                "不可转换为城市用地",
                "数据空值"
            });
        }

        private Bitmap GetBitmap(int width, int height, Color color)
        {
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                    bitmap.SetPixel(i, j, color);
            }
            return bitmap;
        }




        /// <summary>
        /// 判断矩阵中的值是否为空。
        /// </summary>
        /// <returns></returns>
        private bool IsDataGridViewCellsNotNull(int matrixRowLength, int matrixColumnLength)
        {
            for (int i = 0; i < matrixRowLength; i++)
            {
                for (int j = 0; j < matrixColumnLength; j++)
                {
                    if (this.dataGridViewLandUse.Rows[i].Cells[j].Value == null)
                        return false;
                }
            }
            return true;
        }



        #endregion


        #region properties

        public LandUseClassificationInfo LandUse
        {
            get
            {
                return this.landUse;
            }
        }

        #endregion


    }
}
