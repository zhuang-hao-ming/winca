using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Statistics.Models.Regression.Fitting;
using Accord.Statistics;
using System.Windows.Forms;

using Accord.Math;
using Ca.CommonClass;
namespace Ca.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    class LogisticRegression
    {
        #region delegate
        public delegate void UpdateConsoleDelegate(string line); // 向console中打印消息的委托
        public UpdateConsoleDelegate updateConsoleEvent;
        #endregion

        #region fields
        double[] beginBuffer = null;
        double[] endBuffer = null;
        double[][] driveBuffers = null;
        int width = 0;
        int height = 0;
        double[] result = null;
        #endregion

        #region properties
        /// <summary>
        /// 样本数目
        /// </summary>
        public int NumberOfSample
        {
            get;
            set;
        }
        public string ResultLayerName
        {
            get;
            set;
        }
        public LandUseClassificationInfo landUse
        {
            get;
            set;
        }
        /// <summary>
        /// 返回计算出来的logistic图像
        /// </summary>
        public double[] Result
        {
            get
            {
                return this.result;
            }
        }            

        #endregion

        #region inner class
        private class Cell
        {
            public int row { get; set; }
            public int col { get; set; }
            public bool type { get; set; } // true for change, false for not change
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

        /// <summary>
        /// 载入图层名对应的数据
        /// </summary>
        /// <param name="inputColumnNames"></param>
        /// <param name="outputColumnName"></param>
        private void LoadData(string beginLayerName, string endLayerName, List<string> driveLayerNames)
        {
            // ----------------todo 处理null的问题 

            // --- 开发时gdal读数据 --

            //string prefix = @"E:\test\10_21_ann\ca_ann_test\img\";
            //string extname = ".tif";
            string prefix = "";
            string extname = "";
            this.beginBuffer = GdalGetData(prefix + beginLayerName + extname, ref width, ref height);
            this.endBuffer = GdalGetData(prefix + endLayerName + extname, ref width, ref height);
            this.driveBuffers = (from layerName in driveLayerNames
                                select GdalGetData(prefix + layerName + extname, ref width, ref height)).ToArray<double[]>();


            // ---

            // --- 生产时

            // this.driveBufferList = (from fileName in inputColumnNames
            //                         select GetData(fileName, ref width, ref height)).ToArray<double[]>();
            // this.outputBuffer = GetData(outputColumnName, ref width, ref height);

            // ---



        }

        #endregion


        public LogisticRegression(string beginLayerName, string endLayerName, List<string> driveLayerNames)
        {
            // 加载数据
            LoadData(beginLayerName, endLayerName, driveLayerNames);
        }

        public void Run()
        {
            // 采样
            List<Cell> sameplePoints = getSample(this.NumberOfSample);

            // 回归
            regression(sameplePoints);
        }

        public double[] GetResult()
        {
            // 采样
            List<Cell> samplePoints = getSample(this.NumberOfSample);

            // 样本数目
            int COUNT = samplePoints.Count;

            // 构造输入和输出数据集
            double[][] inputs = new double[COUNT][];
            bool[] outputs = new bool[COUNT];
            for (int i = 0; i < COUNT; i++)
            {
                Cell cell = samplePoints[i];
                int pos = cell.row * width + cell.col;
                inputs[i] = (from buffer in driveBuffers
                             select buffer[pos]).ToArray<double>();
                outputs[i] = cell.type;
                
            }



            var learner = new IterativeReweightedLeastSquares<Accord.Statistics.Models.Regression.LogisticRegression>()
            {
                Tolerance = 1e-8,  // 收敛参数
                Iterations = 20,  // 最大循环数目
                Regularization = 0,
                ComputeStandardErrors = true
            };


            Accord.Statistics.Models.Regression.LogisticRegression regression = learner.Learn(inputs, outputs);


            //// 输出 odds
            //StringBuilder strb = new StringBuilder();
            //for (int i = 0; i <= inputs[0].Length; i++)
            //{
            //    strb.AppendLine(" " + i + " : " + regression.GetOddsRatio(i));
            //}
            //updateConsoleEvent(strb.ToString());

            //// 输出 weights
            //StringBuilder strw = new StringBuilder();
            //strw.AppendLine("权重系数：");
            //strw.AppendLine("截距: " + regression.Intercept.ToString());
            //var weights = regression.Weights;
            //for (int i = 0; i < weights.Length; i++)
            //{
            //    strw.AppendLine("权重" + (i + 1) + ":" + weights[i]);
            //}
            //updateConsoleEvent(strw.ToString());

            double[] result = new double[width * height];
            double minProp = double.MaxValue;
            double[] minInput = null;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    if (beginBuffer[pos] < 0 || !IsValid(pos))
                    {
                        result[pos] = this.landUse.NullInfo.LandUseTypeValue;
                        continue;
                    }
                    double[] input = (from buffer in driveBuffers
                                      select buffer[pos]).ToArray<double>();
                    double prop = regression.Probability(input);
                    if (prop < minProp)
                    {
                        minProp = prop;
                        minInput = input;
                    }
                    result[pos] = prop;
                }
            }
            return result;
        }



        private void regression(List<Cell> samplePoints)
        {
            // 构造输入输出数据集

            // 样本数目
            int COUNT = samplePoints.Count;

            // 构造输入和输出数据集
            double[][] inputs = new double[COUNT][];
            bool[] outputs = new bool[COUNT];
            for (int i = 0; i < COUNT; i++)
            {
                Cell cell = samplePoints[i];
                int pos = cell.row * width + cell.col;
                inputs[i] = (from buffer in driveBuffers
                             select buffer[pos]).ToArray<double>();
                outputs[i] = cell.type;
            }



            var learner = new IterativeReweightedLeastSquares<Accord.Statistics.Models.Regression.LogisticRegression>()
            {
                Tolerance = 1e-8,  // 收敛参数
                Iterations = 20,  // 最大循环数目
                Regularization = 0,
                ComputeStandardErrors = true
            };

            
            Accord.Statistics.Models.Regression.LogisticRegression regression = learner.Learn(inputs, outputs);


            // 输出 odds
            StringBuilder strb = new StringBuilder();
            for (int i = 0; i <= inputs[0].Length; i++)
            {
                strb.AppendLine(" " + i + " : " + regression.GetOddsRatio(i));
            }
            updateConsoleEvent(strb.ToString());
            
            // 输出 weights
            StringBuilder strw = new StringBuilder();
            strw.AppendLine("权重系数：");
            strw.AppendLine("截距: " + regression.Intercept.ToString());
            var weights = regression.Weights;
            for(int i = 0; i < weights.Length; i++)
            {
                strw.AppendLine("权重" + (i + 1) + ":" + weights[i]);
            }
            updateConsoleEvent(strw.ToString());

            double[] result = new double[width * height];
            double minProp = double.MaxValue;
            double[] minInput = null;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    if (beginBuffer[pos] < 0 || !IsValid(pos))
                    {
                        result[pos] = this.landUse.NullInfo.LandUseTypeValue;
                        continue;
                    }
                    double[] input = (from buffer in driveBuffers
                                      select buffer[pos]).ToArray<double>();
                    double prop = regression.Probability(input);
                    if (prop < minProp)
                    {
                        minProp = prop;
                        minInput = input;
                    }
                    result[pos] = prop;
                }
            }

            

            // 新建 GDAL dataset
            OSGeo.GDAL.Driver driver = OSGeo.GDAL.Gdal.GetDriverByName("GTIFF");
            OSGeo.GDAL.Dataset dataset = driver.Create(this.ResultLayerName, width, height, 1, OSGeo.GDAL.DataType.GDT_Float64, null);

            dataset.WriteRaster(0, 0, width, height, result, width, height, 1, new int[1] { 1 }, 0, 0, 0);
            dataset.FlushCache();




            
        }



        /// <summary>
        /// 获取样本点
        /// </summary>
        /// <param name="sampleCount">基础采样数目，实际采样数目为：新增：不变 = 1 ：4</param>
        /// <returns></returns>
        private List<Cell> getSample(int sampleCount)
        {
            
            // 首先聚集不变和新增点。
            // 可以实现分类采样，并且提高采样效率
            List<Cell> samePoints = new List<Cell>();
            List<Cell> changePoints = new List<Cell>();
            for (int row = 0; row < this.height; row++)
            {
                for (int col = 0; col < this.width; col++)
                {
                    int pos = row * width + col;
                    double beginType = this.beginBuffer[pos];
                    double endType = this.endBuffer[pos];
                    if (!IsValid(pos))
                    {
                        continue;
                    }
                    if (this.landUse.IsExistInConvertableInfos(beginType))
                    {

                        if (this.landUse.IsExistInConvertableInfos(endType))
                        {
                            samePoints.Add(new Cell { row = row, col = col, type = false});
                        }
                        else if (this.landUse.IsExistInUrbanInfos(endType))
                        {
                            changePoints.Add(new Cell { row = row, col = col, type = true});
                        }
                        
                    }                                        
                }
            }


            List<Cell> samplePoints = new List<Cell>();
            Random rnd = new Random();
            for (int i = 0; i < sampleCount; i++)
            {
                

                if (i % 4 == 0) 
                {
                    int idx = rnd.Next(changePoints.Count);
                    samplePoints.Add(changePoints[idx]);
                    changePoints.RemoveAt(idx);
                }
                else
                {
                    int idx = rnd.Next(samePoints.Count);
                    samplePoints.Add(samePoints[idx]);
                    samePoints.RemoveAt(idx);
                }
            }

            return samplePoints;
        }

        /// <summary>
        /// 判断一个点的驱动数据是否有效
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsValid(int pos)
        {
            for (int i = 0; i < driveBuffers.Length; i++)
            {
                if (driveBuffers[i][pos] < 0 || Math.Abs(this.driveBuffers[i][pos] - this.landUse.NullInfo.LandUseTypeValue) < Double.Epsilon)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
