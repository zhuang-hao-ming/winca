using Ca.Algorithms;
using Ca.CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ca.CaClass
{
    class LgCa: BaseCa
    {
        #region fields
        Random rnd = null;
        double[] lgBuffer = null;
        double[] middleBuffer = null;
        

        #region simulation parameter


        int numberOfSample = 0;
        int timeOfSimulate = 20;        
        double threshold = 0.7;
        int sizeOfNeighbour = 3;
        double alpha = 1;
        #endregion
        #endregion

        #region property
        /// <summary>
        /// 邻域大小
        /// </summary>
        public int SizeOfNeighbour
        {
            get
            {
                return sizeOfNeighbour;
            }
            set
            {
                this.sizeOfNeighbour = value;
            }
        }            
        /// <summary>
        /// 模拟次数
        /// </summary>
        public int TimeOfSimulate
        {
            get
            {
                return this.timeOfSimulate;
            }
            set
            {
                this.timeOfSimulate = value;
            }
        }
        /// <summary>
        /// 转换阈值
        /// </summary>
        public double Threshold
        {
            get
            {
                return this.threshold;
            }
            set
            {
                this.threshold = value;
            }
        }
        /// <summary>
        /// 采样数目
        /// </summary>
        public int NumOfSample
        {
            get
            {
                return this.numberOfSample;
            }
            set
            {
                this.numberOfSample = value;
            }
        }

        public double Alpha
        {
            get
            {
                return this.alpha;
            }
            set
            {
                this.alpha = value;
            }
        }
        /// <summary>
        /// 终止城市数目
        /// </summary>
        public int EndCityCnt { get; set; }
        /// <summary>
        /// 起始城市数目
        /// </summary>
        public int BeginCityCnt { get; set; }

        #endregion

        #region constructor
        public LgCa(string beginLayernName, string endLayerName, List<string> driveLayerNames)
        {
            this.beginLayernName = beginLayernName;
            this.endLayerName = endLayerName;
            this.driveLayerNames = driveLayerNames;

            this.rnd = new Random();
            // 准备数据
            LoadData();
            
            // 开辟中间内存
            this.middleBuffer = new double[width * height];


        }
        #endregion

        #region public method
        /// <summary>
        /// 模拟
        /// </summary>
        public void Simulate()
        {

            this.ChartCountOfTypes = this.landInfo.NumOfLandUseTypes;
            this.ChartCellCountArr = new int[this.ChartCountOfTypes];
            this.InitialChart();

            this.BeginCityCnt = this.GetCityCnt();
            int nowCityCnt = this.BeginCityCnt;

            // 计算lg值
            this.lgBuffer = GetLgBuffer();
            int times = 0;
            int numOfAll = 0;
            while (times < this.timeOfSimulate)
            {

                if(nowCityCnt > this.EndCityCnt)
                {
                    break;
                }

                // 清空图表存储数组
                this.ClearChartCellCountArr();
                times++;
                int numOfChange = 0;
                #region parallel for
                Parallel.For(0, height * width, pos =>
                {

                    int row = pos / width;
                    int col = pos % width;

                    middleBuffer[pos] = beginBuffer[pos];
                    if (this.landInfo.IsExistInConvertableInfos(this.beginBuffer[pos]))
                    {
                        double globalValue = this.lgBuffer[pos];
                        double localValue = GetNeighbourAffect(row, col, this.sizeOfNeighbour);
                        double randomValue = Math.Pow(-Math.Log(ThreadLocalRandom.NextDouble()), this.alpha);
                        if (globalValue * localValue * randomValue > threshold)
                        {
                            this.middleBuffer[pos] = this.landInfo.UrbanInfos[0].LandUseTypeValue;
                            numOfChange++;
                            numOfAll++;
                            nowCityCnt++;
                        }
                        // 修改图表计数数组

                    }
                    AddChartCellCountArr(middleBuffer[pos]);

                });
                #endregion
                #region sequential
                //    for (int row = 0; row < height; row++)
                //{
                //    for (int col = 0; col < width; col++)
                //    {
                //        int pos = row * width + col;
                //        this.middleBuffer[pos] = this.beginBuffer[pos];
                //        if (this.landInfo.IsExistInConvertableInfos(this.beginBuffer[pos]))
                //        {
                //            double globalValue = this.lgBuffer[pos];
                //            double localValue = GetNeighbourAffect(row, col, this.sizeOfNeighbour);
                //            double randomValue = Math.Pow(-Math.Log(rnd.NextDouble()), this.alpha);
                //            if (globalValue * localValue * randomValue > threshold)
                //            {
                //                this.middleBuffer[pos] = this.landInfo.UrbanInfos[0].LandUseTypeValue;
                //                numOfChange++;
                //                numOfAll++;
                //                nowCityCnt++;
                //            }
                //            // 修改图表计数数组

                //        }
                //        AddChartCellCountArr(middleBuffer[pos]);

                //    }
                //}
                #endregion
                updateConsoleEvent("-------");
                updateConsoleEvent("当前城市数目： " + nowCityCnt);
                updateConsoleEvent("这次增长了" + numOfChange + "个元胞");
                updateConsoleEvent("-------");
                this.updateImageEvent(this.middleBuffer, this.width, this.height, this.landInfo, times);
                this.updateChartEvent(this.ChartCellCountArr, times++, this.landInfo);
                // 将middle buffer的值复制会begin buffer
                for (int pos = 0; pos < width * height; pos++)
                {
                    this.beginBuffer[pos] = this.middleBuffer[pos];
                }                
                


            }
            this.updateConsoleEvent("模拟结束");
            this.updateConsoleEvent("总共增长了" + numOfAll + "个元胞");

            var kappa = new KappaTest(this.endBuffer, this.beginBuffer, width, height, landInfo);
            var kappaVal = kappa.GetVal();
            this.updateConsoleEvent("kappa值 : " + kappaVal);
            this.simulateEndEvent(this);
        }
        #endregion

        #region private method

        private int GetCityCnt()
        {
            int beginCityCnt = 0;
            for (int i = 0; i < width * height; i++)
            {
                if (this.landInfo.IsExistInUrbanInfos(this.beginBuffer[i]))
                {
                    beginCityCnt++;
                }
            }
            return beginCityCnt;
        }

        /// <summary>
        /// 获得邻域影响值
        /// </summary>        
        /// <param name="cellRow"></param>
        /// <param name="cellCol"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private double GetNeighbourAffect(int cellRow, int cellCol, int step)
        {
            double all = step * step - 1;
            step = (int)(step / 2); // 
            int rowBegin = cellRow - step;
            int rowEnd = cellRow + step + 1;
            int colBegin = cellCol - step;
            int colEnd = cellCol + step + 1;
            int cnt = 0;
            for (int row = rowBegin; row < rowEnd; row++)
            {
                for (int col = colBegin; col < colEnd; col++)
                {
                    if (row < 0 || col < 0 || row >= height || col >= width || (row == cellRow && col == cellCol))
                    {
                        continue;
                    }
                    int pos = row * width + col;
                    double type = this.beginBuffer[pos];
                    if (this.landInfo.IsExistInUrbanInfos(type))
                    {
                        cnt++;
                    }
                }
            }
            return (double)cnt / all;
        }

        private double[] GetLgBuffer()
        {
            var lg = new LogisticRegression(this.beginLayernName, this.endLayerName, this.driveLayerNames);
            lg.landUse = this.landInfo;
            lg.NumberOfSample = this.numberOfSample;
            return lg.GetResult();
        }
        #endregion



    }
}
