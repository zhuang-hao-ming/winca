using Ca.CommonClass;
using OSGeo.GDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ca.CaClass
{
    /// <summary>
    /// 向console中打印消息的委托
    /// </summary>
    /// <param name="line"></param>
    public delegate void UpdateConsoleDelegate(string line);
    /// <summary>
    /// 绘制图表委托
    /// </summary>
    /// <param name="cellCount"></param>
    /// <param name="time"></param>
    public delegate void UpdateChartDelegat(int[] cellCount, int time, LandUseClassificationInfo landUseInfo);
    /// <summary>
    /// 绘制模拟图像委托
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="landUseInfo"></param>         
    public delegate void UpdateImageDelegate(double[] buffer, int width, int height, LandUseClassificationInfo landUseInfo, int cnt);
    /// <summary>
    /// 模拟结束委托
    /// </summary>
    public delegate void SimulateEndDelegate(BaseCa ca);
    /// <summary>
    /// c# ca类的基类
    /// </summary>
    public class BaseCa
    {
        #region event
        /// <summary>
        /// 向console中打印消息事件
        /// </summary>
        public  UpdateConsoleDelegate updateConsoleEvent;
        /// <summary>
        /// 绘制图像事件
        /// </summary>
        public  UpdateImageDelegate updateImageEvent;
        /// <summary>
        /// 绘制图表事件
        /// </summary>
        public UpdateChartDelegat updateChartEvent;
        /// <summary>
        /// 模拟结束事件
        /// </summary>
        public  SimulateEndDelegate simulateEndEvent;
        #endregion

        #region fields
        /// <summary>
        /// 起始图层名
        /// </summary>
        protected string beginLayernName = "";
        /// <summary>
        /// 终止图层名
        /// </summary>
        protected string endLayerName = "";
        /// <summary>
        /// 驱动图层名
        /// </summary>
        protected List<string> driveLayerNames = null;
        /// <summary>
        /// 驱动图层数据
        /// </summary>
        protected double[][] driveBufferList = null;
        /// <summary>
        /// 起始图层数据
        /// </summary>
        protected double[] beginBuffer = null;
        /// <summary>
        /// 终止图层数据
        /// </summary>
        protected double[] endBuffer = null;
        /// <summary>
        /// 图像宽度
        /// </summary>
        protected int width = 0;
        /// <summary>
        /// 图像高度
        /// </summary>
        protected int height = 0;
        /// <summary>
        /// 土地利用分类信息
        /// </summary>
        protected LandUseClassificationInfo landInfo = null;
        #endregion




        #region properties
        /// <summary>
        /// tiff type
        /// </summary>
        public Type tiffType { get; private set; }
        /// <summary>
        /// tiff no data
        /// </summary>
        public double noDataVal { get; private set; }
        /// <summary>
        /// tif geotransform
        /// </summary>
        public double[] geoTransform { get; set; }
        /// <summary>
        /// tif proj
        /// </summary>
        public string projStr { get; set; }
        /// <summary>
        /// 图表字段数目
        /// </summary>       
        protected int ChartCountOfTypes { get; set; }
        /// <summary>
        /// 图表数组
        /// </summary>
        protected int[] ChartCellCountArr { get; set; }






        /// <summary>
        /// 当前图像状态
        /// </summary>
        public double[] BeginBuffer
        {
            get
            {
                return this.beginBuffer;
            }
        }
        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }
        }
        /// <summary>
        /// 图像高度
        /// </summary>
        public int Height
        {
            get
            {
                return this.height;
            }
        }
        /// <summary>
        /// 返回土地利用信息
        /// </summary>
        public LandUseClassificationInfo LandInfo
        {
            get
            {

                return this.landInfo;
            }
            set
            {

                this.landInfo = value;
            }
        }

        



        #endregion

        #region protected method

        /// <summary>
        /// 修改图表计数数组
        /// </summary>
        /// <param name="val"></param>
        protected void AddChartCellCountArr(double val)
        {

            int countIdx = GetListIdx(val);
            if (countIdx != -1)
            {
                this.ChartCellCountArr[countIdx]++;
            }
        }


        /// <summary>
        /// 清空chart数组
        /// </summary>
        protected void ClearChartCellCountArr()
        {
            for (int m = 0; m < this.ChartCellCountArr.Length; m++)
            {
                this.ChartCellCountArr[m] = 0;
            }
        }
        /// <summary>
        /// 得到一个栅格值，在土地利用列表中的索引
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected int GetListIdx(double val)
        {
            for (int i = 0; i < landInfo.AllTypes.Count; i++)
            {
                if (landInfo.AllTypes[i].LandUseTypeValue == val)
                {

                    return i;
                }
            }
            return -1;
        }

  

        /// <summary>
        /// 初始化图表
        /// </summary>
        protected void InitialChart()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    int i = GetListIdx(this.beginBuffer[pos]);
                    if (i != -1)
                    {
                        this.ChartCellCountArr[i]++;
                    }

                }
            }
            updateChartEvent(this.ChartCellCountArr, 0, landInfo);  // draw
        }

        /// <summary>
        /// 判断在这个系统中两个double值是否相等
        /// </summary>
        /// <param name="lh"></param>
        /// <param name="rh"></param>
        /// <returns></returns>
        protected bool IsDoubleEqual(double lh, double rh)
        {
            return (int)lh == (int)rh;            
        }




        ///// <summary>
        ///// 根据栅格图层名字返回栅格图层数据数组
        ///// </summary>
        ///// <param name="layerName">图层名字</param>
        ///// <param name="width">ref类型,数据的宽度</param>
        ///// <param name="height">ref类型,数据的高度</param>
        ///// <returns>数据数组，按行优先</returns>
        //private double[] GetData(string layerName, ref int width, ref int height)
        //{
        //    var map = GIS.FrameWork.Application.App.Map;
        //    var layers = map.Layers;
        //    ILayer selectedLayer = null;
        //    for (int i = 0; i < layers.Count; i++)
        //    {
        //        var layer = layers[i];
        //        if (layer.LegendText == layerName)
        //        {
        //            selectedLayer = layer;
        //            break;
        //        }
        //    }

        //    if (selectedLayer == null)
        //    {
        //        return null; // 图层名无效
        //    }

        //    try
        //    {
        //        RasterLayer rasterLayer = selectedLayer as RasterLayer;
        //        IRaster rasterDataSet = rasterLayer.DataSet;
        //        Dataset dataset = GIS.GDAL.RasterConverter.Ds2GdalRaster(rasterDataSet, null, new int[] { 1 });
        //        width = dataset.RasterXSize;
        //        height = dataset.RasterYSize;
        //        double[] imageBuffer = new double[width * height];
        //        Band band = dataset.GetRasterBand(1);
        //        band.ReadRaster(0, 0, width, height, imageBuffer, width, height, 0, 0);
        //        return imageBuffer;
        //    }
        //    catch
        //    {
        //        return null; // 图层不是栅格图层或将图层转化为GDAL dataset失败或从GDAL dataset中读取数据失败
        //    }
        //}




        /// <summary>
        /// 和GetData功能相同，但是使用GDAL直接从文件中读取数据。
        /// 获取空间参照信息
        /// </summary>
        /// <param name="fileName">文件名字</param>
        /// <param name="width">ref用于返回数据的宽度</param>
        /// <param name="height">ref用于返回数据的高度</param>
        /// <returns>一维数据数组,按行优先</returns>
        protected double[] GdalGetData(string fileName, ref int width, ref int height)
        {
            OSGeo.GDAL.Dataset dataset = OSGeo.GDAL.Gdal.Open(fileName, OSGeo.GDAL.Access.GA_ReadOnly);
            width = dataset.RasterXSize;
            height = dataset.RasterYSize;
            this.geoTransform = new double[6];
            
            dataset.GetGeoTransform(geoTransform);            
            this.projStr = dataset.GetProjection();
            this.tiffType = dataset.GetType();
            
            double[] imageBuffer = new double[width * height];
            OSGeo.GDAL.Band b = dataset.GetRasterBand(1);
            b.ReadRaster(0, 0, width, height, imageBuffer, width, height, 0, 0);
            double noDataVal;
            int hasVal;
            b.GetNoDataValue(out noDataVal, out hasVal);
            this.noDataVal = noDataVal;
            return imageBuffer;
        }

        /// <summary>
        /// 载入图层名对应的数据
        /// </summary>        
        protected void LoadData()
        {
            // ----------------todo 处理null的问题 

            // --- 开发时gdal读数据 --

            //string prefix = @"E:\test\10_21_ann\ca_ann_test\img\";
            //string extname = ".tif";
            string prefix = @"";
            string extname = "";
            this.driveBufferList = (from fileName in this.driveLayerNames
                                    select GdalGetData(prefix + fileName + extname, ref width, ref height)).ToArray<double[]>();
            this.beginBuffer = GdalGetData(prefix + this.beginLayernName + extname, ref width, ref height);
            this.endBuffer = GdalGetData(prefix + this.endLayerName + extname, ref width, ref height);


            // ---

            // --- 生产时

            // this.driveBufferList = (from fileName in inputColumnNames
            //                         select GetData(fileName, ref width, ref height)).ToArray<double[]>();
            // this.outputBuffer = GetData(outputColumnName, ref width, ref height);

            // ---



        }
        #endregion

        #region inner class

        protected class Cell
        {
            public int row { get; set; }
            public int col { get; set; }
            public int idx { get; set; }
        }

        #endregion

        #region constructor
        public BaseCa()
        {

        }
        #endregion
    }
}
