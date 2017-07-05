using Ca.CommonClass;
using Ca.CommonDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ca.CaClass;
using Ca.CaDialog;
using System.Windows.Forms;
using System.IO;

namespace Ca.CaCommandClass
{
    /// <summary>
    /// ca command类的基类
    /// </summary>
    abstract class CaCommandBase
    {
        #region field
        private int uuid = 0;
        private DateTime dateNow = DateTime.Now;
        ///// <summary>
        ///// 控制台
        ///// </summary>
        //protected GIS.Common.Dialogs.Console.Console consolePad = null;
        /// <summary>
        /// 绘图窗口
        /// </summary>
        protected ImageForm imageForm = null;
        #endregion
        #region delegate

        /// <summary>
        /// 更新图表代理
        /// </summary>
        /// <param name="cellCount"></param>
        /// <param name="time"></param>
        protected delegate void AsyncUpdateChart(int[] cellCount, int time, LandUseClassificationInfo landUseInfo);
        /// <summary>
        /// 绘图代理
        /// </summary>
        /// <param name="imgBuffer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="landUseInfo"></param>
        protected delegate void AsyncUpdateImage(double[] imgBuffer, int width, int height, LandUseClassificationInfo landUseInfo, int cnt);
        /// <summary>
        /// 更新控制台代理
        /// </summary>
        /// <param name="line"></param>
        protected delegate void AsyncUpdateConsole(string line);

        #endregion
        #region event handler
        ///// <summary>
        ///// 更新控制台事件处理函数
        ///// </summary>
        ///// <param name="line"></param>
        protected void UpdateConsole(string line)
        {
            if (Ca.Program.form1.InvokeRequired)
            {
                Ca.Program.form1.Invoke(new AsyncUpdateConsole((line1) =>
                {

                    Ca.Program.form1.AddLineToConsole(line1);

                }), line);
            }
            else
            {
                Ca.Program.form1.AddLineToConsole(line);
            }

        }
        /// <summary>
        /// 绘制图表处理函数
        /// </summary>
        /// <param name="cellCount"></param>
        /// <param name="time"></param>
        protected void UpdateChart(int[] cellCount, int time, LandUseClassificationInfo landUseInfo)
        {
            if (this.imageForm.InvokeRequired)
            {
                this.imageForm.Invoke(new AsyncUpdateChart((cellCount1, time1, landUseInfo1) =>
                {

                    this.imageForm.UpdateChart(cellCount1, time1, landUseInfo1);

                }), cellCount, time, landUseInfo);
            }
            else
            {

                this.imageForm.UpdateChart(cellCount, time, landUseInfo);
            }
        }




        /// <summary>
        /// 绘图事件处理函数
        /// </summary>
        /// <param name="imgBuffer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="landUseInfo"></param>
        protected void UpdateImage(double[] imgBuffer, int width, int height, LandUseClassificationInfo landUseInfo, int cnt)
        {
            if (this.imageForm.InvokeRequired)
            {
                this.imageForm.Invoke(new AsyncUpdateImage((imgBuffer1, width1, height1, landUseInfo1, cnt1) =>
                {

                    this.imageForm.UpdateImage(imgBuffer1, width1, height1, landUseInfo1, cnt1);
                    
                }), imgBuffer, width, height, landUseInfo, cnt);
            }
            else
            {

                this.imageForm.UpdateImage(imgBuffer, width, height, landUseInfo, cnt);
            }

        }



        /// <summary>
        /// 模拟结束事件监听辅助函数
        /// </summary>
        protected void SimulateEndHelper(BaseCa ca)
        {
            
            // 将模拟结果添加到系统中
            double[] buffer = ca.BeginBuffer;

            int width = ca.Width;
            int height = ca.Height;
            //var map = GIS.FrameWork.Application.App.Map;

　    
            // 保存输出结果
            var path = Application.StartupPath + @"\log";
            if (!Directory.Exists(path))//如果不存在就创建file文件夹　　             　　                
                Directory.CreateDirectory(path);//创建该文件夹　
            var outputPath = path + @"\" + (dateNow.ToString("MMddyyhhmmss")) + (this.uuid++) + ".tif";
            OSGeo.GDAL.Driver driverSave = OSGeo.GDAL.Gdal.GetDriverByName("GTiff");
            OSGeo.GDAL.Dataset datasetSave = driverSave.Create(outputPath, width, height, 1, OSGeo.GDAL.DataType.GDT_Byte, null);
            datasetSave.WriteRaster(0, 0, width, height, buffer, width, height, 1, new int[1] { 1 }, 0, 0, 0);
            datasetSave.SetProjection(ca.projStr);
            datasetSave.SetGeoTransform(ca.geoTransform);
            datasetSave.GetRasterBand(1).SetNoDataValue(ca.noDataVal);
            datasetSave.FlushCache();
            datasetSave.Dispose();
            driverSave.Dispose();

            //// 翻转图像 ？
            //buffer = InvertImage(buffer, width, height);

            MessageBox.Show("模拟结束, 输出文件：" + outputPath);
            

            //// 新建 GDAL dataset
            //OSGeo.GDAL.Driver driver = OSGeo.GDAL.Gdal.GetDriverByName("MEM");
            //OSGeo.GDAL.Dataset dataset = driver.Create("", width, height, 1, OSGeo.GDAL.DataType.GDT_Float64, null);
            //dataset.WriteRaster(0, 0, width, height, buffer, width, height, 1, new int[1] { 1 }, 0, 0, 0);


            //// 将GDAL dataset转化为IRaster数据集
            //DotSpatial.Data.IRaster raster = GIS.GDAL.RasterConverter.Gdal2DSRaster(dataset, 1);
            //raster.Name = "Result";

            //map.Layers.Add(raster);
        }
        /// <summary>
        /// 模拟结束事件监听函数
        /// </summary>
        protected void SimulateEnd(BaseCa ca)
        {

            //var lengend = GIS.FrameWork.Application.App.Legend as Legend;
            //if (lengend.InvokeRequired)
            //{
            //    lengend.Invoke(new Action<BaseCa>((ca1) =>
            //    {

            //        SimulateEndHelper(ca1);

            //    }), ca);
            //}
            //else
            //{
            SimulateEndHelper(ca);
            //}

        }


        #endregion

        #region private methods
        /// <summary>
        /// 翻转图像
        /// </summary>
        /// <param name="rawBuffer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        double[] InvertImage(double[] rawBuffer, int width, int height)
        {
            double[] newBuffer = new double[width * height];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    newBuffer[row * width + col] = rawBuffer[(height - row - 1) * width + col];
                }
            }
            return newBuffer;
        }

        #endregion


    }
}
