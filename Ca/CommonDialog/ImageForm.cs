using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ca.CommonClass;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace Ca.CommonDialog
{
    public partial class ImageForm : Form
    {

        private Bitmap bitmap;

        public Thread ThreadSimulate
        {
            get;set;
        }

        private bool isOn;
        public bool IsOn()
        {
            return this.isOn;
        }

        public void UpdateImage(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo, int cnt)
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    double type = buffer[pos];
                    if (type < 0)
                    {
                        bitmap.SetPixel(col, row, Color.Transparent); // null value
                        continue;
                    }
                    for (int i = 0; i < landUseInfo.AllTypes.Count; i++)
                    {
                        if (landUseInfo.AllTypes[i].LandUseTypeValue == type)
                        {
                            bitmap.SetPixel(col, row, Color.FromArgb(landUseInfo.AllTypes[i].LandUseTypeColor));
                            break;
                        }
                    }

                }
            }
            this.pictureBox1.Image = bitmap;
            this.labelTip.Text = "当前的模拟次数是: ";
            this.labelNum.Text = (cnt+1) + "";
        }




        public void UpdateImage(double[] buffer, int width, int height)
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    int type = (int)buffer[pos];                 
                    if (type == 1)
                    {
                        bitmap.SetPixel(col, row, Color.Black);
                    }                  

                }
            }
            this.pictureBox1.Image = bitmap;
            
        }

        public ImageForm(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo)
        {
            InitializeComponent();

            // 初始化图表
            InitialChart(landUseInfo);



            this.buttonStop.Enabled = false;
            this.buttonResume.Enabled = false;
            isOn = true;

            bitmap = new Bitmap(width, height);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    double type = buffer[pos];
                    if (type < 0)
                    {
                            bitmap.SetPixel(col, row, Color.Transparent); // null value
                            continue;
                    }
                    for (int i = 0; i < landUseInfo.AllTypes.Count; i++)
                    {
                        if (landUseInfo.AllTypes[i].LandUseTypeValue == type)
                        {
                            bitmap.SetPixel(col, row, Color.FromArgb(landUseInfo.AllTypes[i].LandUseTypeColor));
                            break;
                        }
                    }
                    //if (type < 0)
                    //{
                    //    bitmap.SetPixel(col, row, Color.Transparent);
                    //}
                    //if (type == 1)
                    //{
                    //    bitmap.SetPixel(col, row, Color.Black);
                    //}
                    //if (type == 2)
                    //{
                    //    bitmap.SetPixel(col, row, Color.Blue);
                    //}
                    //if (type == 0)
                    //{
                    //    bitmap.SetPixel(col, row, Color.White);
                    //}

                }
            }

            this.pictureBox1.Image = bitmap;
            this.labelTip.Text = "训练准备";
        }

        private void InitialChart(LandUseClassificationInfo landUseClassificationInfo)
        {
            this.chartTypeCount.Series.Clear();
            int size = landUseClassificationInfo.NumOfLandUseTypes;
            for (int j = 0; j < landUseClassificationInfo.AllTypes.Count; j++)
            {
                var landType = landUseClassificationInfo.AllTypes[j];
                var series = new Series()
                {
                    Name = landType.LandUseTypeName,
                    Color = Color.FromArgb(landType.LandUseTypeColor),
                    ChartType = SeriesChartType.Line,
                    IsVisibleInLegend = true
                };
                this.chartTypeCount.Series.Add(series);
            }           
        }

        public void UpdateChart(int[] cellCount1, int time1, LandUseClassificationInfo landUseInfo)
        {
            for(int i = 0; i < cellCount1.Length; i++)
            {
                this.chartTypeCount.Series[i].Points.AddXY(time1, cellCount1[i]);
            }
        }

        public ImageForm(double[] buffer, int width, int height)
        {
            InitializeComponent();
            
            this.buttonStop.Enabled = false;
            this.buttonResume.Enabled = false;
            isOn = true;

            bitmap = new Bitmap(width, height);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    int type = (int)buffer[pos];
                    if (type < 0)
                    {
                        bitmap.SetPixel(col, row, Color.Transparent);
                    }
                    if (type == 1)
                    {
                        bitmap.SetPixel(col, row, Color.Black);
                    }
                    if (type == 2)
                    {
                        bitmap.SetPixel(col, row, Color.Blue);
                    }
                    if (type == 0)
                    {
                        bitmap.SetPixel(col, row, Color.White);
                    }
                    
                }
            }

            this.pictureBox1.Image = bitmap;
            this.labelTip.Text = "训练准备";
        }

        public ImageForm()
        {
            InitializeComponent();



        }
        public void ActiveButton()
        {
            this.buttonStop.Enabled = true;
            this.buttonResume.Enabled = true;
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (isOn && this.ThreadSimulate!= null &&this.ThreadSimulate.IsAlive)
            {
                isOn = false;
                this.ThreadSimulate.Suspend();
            }
        }

        private void buttonResume_Click(object sender, EventArgs e)
        {
            if (!isOn && this.ThreadSimulate != null && this.ThreadSimulate.IsAlive)
            {
                isOn = true;
                this.ThreadSimulate.Resume();
            }
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {

        }

        //private void buttonReStart_Click(object sender, EventArgs e)
        //{
        //    if(this.ThreadSimulate.IsAlive)
        //    {
        //        try
        //        {
        //            this.ThreadSimulate.Abort();
        //        }
        //        catch(Exception ex)
        //        {
        //            // 终止线程
        //        }

        //    }
        //    this.ThreadSimulate.Start();
        //}
    }
}
