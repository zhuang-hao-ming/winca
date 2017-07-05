//using Accord.Math.Optimization.Losses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;
using Ca.CommonClass;
//using GIS.AddIns.Ca.Algorithms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Ca.Algorithms;

namespace Ca.CaClass
{
    // todo 标准化事件处理代码， 和.net内部的事件处理代码统一
    //public class UpdateConsoleEventArgs: EventArgs
    //{
    //    public string line { get; set; }
    //}




    public class RandomForestCa: BaseCa
    {

        #region fields





        Random rnd = null;

        #region simulation parameters
        int numOfSample = 3000;
        
        int numOfTree = 10;
        double coverageRatio = 1;
        double sampleRatio = 0.6;
        int sizeOfNeighbour = 5;
        #endregion
        //RandomForest forest = null;
        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginLayernName">训练模拟起始图层名字</param>
        /// <param name="endLayerName">训练模拟终止图层名字</param>
        /// <param name="driveLayerNames">驱动因子图层名字</param>
        public RandomForestCa(string beginLayernName, string endLayerName, List<string> driveLayerNames, LandUseClassificationInfo landInfo)
        {

            this.beginLayernName = beginLayernName;
            this.endLayerName = endLayerName;
            this.driveLayerNames = driveLayerNames;
            this.landInfo = landInfo;
            this.rnd = new Random();
            //准备数据
            LoadData();


        }
        #endregion

        #region private methods
     








        /// <summary>
        /// 采样,按照土地类型等比例采样
        /// </summary>
        /// <param name="count">样本数目</param>
        /// <returns></returns>
        private List<Cell> GetSamples(int count)
        {
            List<Cell> samples = new List<Cell>();
            long totalNumOfCells = 0;
            long totalNumOfCells1 = 0;

            // 聚集每种土地利用类型的元胞
            List<Cell>[] landUseChangeType = new List<Cell>[this.landInfo.NumOfLandUseTypes];
            List<Cell>[] endLandUseChangeType = new List<Cell>[this.landInfo.NumOfLandUseTypes];
            for (int i = 0; i < landUseChangeType.Length; i++)
            {
                landUseChangeType[i] = new List<Cell>();
                endLandUseChangeType[i] = new List<Cell>();
            }
            int beginCityCnt = 0;
            int endCityCnt = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    if (IsValid(pos)) // 所有数据都非空值才是可用的有效数据
                    {
                        // 计数起始城市元胞的数目
                        if (this.landInfo.IsExistInUrbanInfos(this.beginBuffer[pos]))
                        {
                            beginCityCnt++;
                        }
                        // 计数终止城市元胞的数目
                        if (this.landInfo.IsExistInUrbanInfos(this.endBuffer[pos]))
                        {
                            endCityCnt++;
                        }

                        for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
                        {
                            if (this.landInfo.AllTypes[i].LandUseTypeValue == beginBuffer[pos])
                            {
                                landUseChangeType[i].Add(new Cell() { row = row, col = col, idx = i});                                                                
                                totalNumOfCells++;
                            }
                            if (this.landInfo.AllTypes[i].LandUseTypeValue == endBuffer[pos])
                            {
                                endLandUseChangeType[i].Add(new Cell() { row = row, col = col, idx = i });
                                totalNumOfCells1++;
                            }

                        }
                    }

                }
            }
            // 计数起始城市元胞的数目
            this.beginCityCount = beginCityCnt;
            this.nowCityCount = beginCityCnt;
            // 计数终止城市元胞的数目
            this.endCityCount = endCityCnt;

            this.updateConsoleEvent("----------------------");
            this.updateConsoleEvent("起始的城市数目:" + this.beginCityCount);
            this.updateConsoleEvent("终止的城市数目:" + this.endCityCount);
            this.updateConsoleEvent("----------------------");

            // 对每种土地利用类型分别采样
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                int num = (int)((float)(landUseChangeType[i].Count) / (float)totalNumOfCells * count);
                for (int j = 0; j < num; j++)
                {

                    if (landUseChangeType[i].Count != 0)
                    {
                        int idx = this.rnd.Next(landUseChangeType[i].Count);
                        Cell cell = landUseChangeType[i][idx];
                        samples.Add(cell);
                        landUseChangeType[i].RemoveAt(idx);
                    }

                }
            }

            // 采样数目不够，继续采样
            while (samples.Count != count)
            {
                int row = rnd.Next(height);
                int col = rnd.Next(width);
                int pos = row * width + col;
                if (!IsValid(pos)) // 跳过无效数据
                {
                    continue;
                }
                for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
                {
                    if (this.landInfo.AllTypes[i].LandUseTypeValue == beginBuffer[pos])
                    {
                        samples.Add(new Cell() { row = row, col = col, idx=i });
                    }
                }
                 
            }



            return samples;
        }

        /// <summary>
        /// 判断一个点的数据是否有效
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsValid(int pos)
        {
            if(Math.Abs(this.beginBuffer[pos] - this.landInfo.NullInfo.LandUseTypeValue)  < Double.Epsilon)
            {
                return false;
            }
            for (int i = 0; i < driveBufferList.Length; i++)
            {
                if (driveBufferList[i][pos] < 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获得一个位置的驱动数据
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private double[] getOneInput(Cell cell)
        {
            List<double> input = new List<double>();
            var pos = cell.row * width + cell.col;            
            double[] drive = (from buffer in driveBufferList
                              select buffer[pos]).ToArray<double>();           
            return drive;
        }

        /// <summary>
        /// 获得type指定类型的邻域比例
        /// </summary>
        /// <param name="cellRow"></param>
        /// <param name="cellCol"></param>
        /// <param name="type"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private double GetNeighbourAffect(int cellRow, int cellCol, double type, int step)
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

                    if (beginBuffer[pos] == type)
                    {
                        cnt++;
                    }
                }
            }
            return (double)cnt / all;
        }

        #endregion

        #region properties
        /// <summary>
        /// 目标城市栅格数目
        /// </summary>
        public int targetCityCnt { get; set; }
        /// <summary>
        /// 类型转换控制矩阵
        /// </summary>
        public int[,] transformControlMatrix { get; set; }
        /// <summary>
        /// 起始年份的城市数目
        /// </summary>
        public int beginCityCount { get; set; }
        /// <summary>
        /// 终止年份的城市数目
        /// </summary>
        public int endCityCount { get; set; }
        /// <summary>
        /// 当前模拟周期的城市数目
        /// </summary>
        public int nowCityCount { get; set; }

        /// <summary>
        /// 城市发展概率调整，适当调大城市发展概率
        /// </summary>
        public double cityPropAdjust { get; set; }

        /// <summary>
        /// 是否要计算变量重要性
        /// </summary>
        public bool isNeedSignificant
        {
            get;set;
        }

        public double alpha
        {
            get;set;
        }

        /// <summary>
        /// 森林模型
        /// </summary>
        public alglib.decisionforest Forest
        {
            get;set;
        }

        /// <summary>
        /// 采样数目
        /// </summary>
        public int NumOfSample
        {
            get
            {
                return this.numOfSample;
            }
            set
            {
                if(value < 0)
                {
                    return;
                }
                this.numOfSample = value;
            }
        }

      


        /// <summary>
        /// 树的个数
        /// </summary>
        public int NumOfTree
        {
            get
            {
                return this.numOfTree;
            }
            set
            {
                this.numOfTree = value;
            }
        }
        /// <summary>
        /// 训练一棵树时所使用的数据比例
        /// </summary>
        public double SampleRatio
        {
            get
            {
                return this.sampleRatio;
            }
            set
            {
                this.sampleRatio = value;
            }
        }
        /// <summary>
        /// 训练一棵树时所使用的属性比例
        /// </summary>
        public double CoverageRatio
        {
            get
            {
                return this.coverageRatio;
            }
            set
            {
                this.coverageRatio = value;
            }
        }

        public int SizeOfNeighbour
        {
            get
            {
                return this.sizeOfNeighbour;
            }
            set
            {
                this.sizeOfNeighbour = value;
            }
        }

        




        #endregion

        #region public methods

        public void Train()
        {
            var samples = GetSamples(this.NumOfSample);

            double[][] inputs = (from cell in samples
                                 select getOneInput(cell)).ToArray<double[]>();

            int[] classes = (from cell in samples
                             select cell.idx).ToArray<int>();


            /**/
            int dim1 = inputs.Length;
            int dim2 = inputs[0].Length + 1;
            double[,] xy = new double[dim1, dim2];
            for (int i = 0; i < dim1; i++)
            {
                for (int j = 0; j < dim2 - 1; j++)
                {
                    xy[i, j] = inputs[i][j];
                }
                xy[i, dim2 - 1] = classes[i];
            }

            int info;
            alglib.decisionforest df;
            alglib.dfreport rep;

            alglib.dfbuildrandomdecisionforest(xy, dim1, dim2 - 1, this.landInfo.AllTypes.Count, this.NumOfTree, this.SampleRatio, out info, out df, out rep);
            updateConsoleEvent("平均误差"+rep.avgerror * 100 + "%");

            if (isNeedSignificant)
            {
                // 计算变量重要性
                computeSignificant(xy, rep);
            }
            

            this.Forest = df;
        }

        /// <summary>
        /// 计算变量重要性
        /// </summary>
        /// <param name="xy"></param>
        /// <param name="oobavgerror"></param>
        private void computeSignificant(double[,] xy, alglib.dfreport rep)
        {


            
            int dim1 = xy.GetLength(0); // 测试集大小
            int dim2 = xy.GetLength(1); // 自变量个数+1
            double[] results = new double[dim2 - 1];

            int info;
            alglib.decisionforest df;
            alglib.dfreport rep1;

            for (int i = 0; i < results.Length; i++)
            {
                double[,] xy1 = (double[,])xy.Clone();

                // 清空列
                for(int j = 0; j < dim1; j++)
                {
                    xy1[j, i] = 0;    
                }
                alglib.dfbuildrandomdecisionforest(xy1, dim1, dim2 - 1, this.landInfo.NumOfLandUseTypes, this.NumOfTree, this.SampleRatio, out info, out df, out rep1);
                results[i] = rep.oobavgce - rep.oobavgce;

                //this.updateConsoleEvent("变量" + (i + 1) + "的重要性分数oobavgce是" +　(rep.oobavgce - rep1.oobavgce));
                //this.updateConsoleEvent("变量" + (i + 1) + "的重要性分数oobavgerror是" + (rep.oobavgerror - rep1.oobavgerror));
                //this.updateConsoleEvent("变量" + (i + 1) + "的重要性分数oobavgrelerror是" + (rep.oobavgrelerror - rep1.oobavgrelerror));
                //this.updateConsoleEvent("变量" + (i + 1) + "的重要性分数oobrelclserror是" + (rep.oobrelclserror - rep1.oobrelclserror));
                
                
                this.updateConsoleEvent("变量" + (i + 1) + this.driveLayerNames[i] + "的重要性分数oobrmserror是" + (rep1.oobrmserror - rep.oobrmserror));
            }
            
            
            

        }

        /// <summary>
        /// 模拟
        /// </summary>
        public void Simulate(int times)
        {
            int nowCityCnt = this.nowCityCount;
            int cnt = 0;
            int numOfChangesOneTime = 0;
            double[] middleBuffer = new double[width * height];
            while (cnt < times)
            {
                if(this.nowCityCount > this.targetCityCnt)
                {
                    break;
                }            

                // 清空图表存储数组
                this.ClearChartCellCountArr();

        

                #region parallel for
                Parallel.For(0, height * width, pos =>
                {
                    
                    int row = pos / width;
                    int col = pos % width;

                    middleBuffer[pos] = beginBuffer[pos];

                    if (!IsValid(pos)) // 无效值 跳过
                    {
                        return;
                    }

                    if (middleBuffer[pos] == this.landInfo.WaterInfo.LandUseTypeValue) // 水体跳过
                    {

                        AddChartCellCountArr(middleBuffer[pos]);
                        return;
                    }
                    if (this.landInfo.IsExistInUrbanInfos(middleBuffer[pos])) // 城市跳过
                    {

                        AddChartCellCountArr(middleBuffer[pos]);
                        return;
                    }

                    var input = getOneInput(new Cell() { row = row, col = col });

                    double[] propabilities = new double[this.landInfo.AllTypes.Count];
                    alglib.dfprocess(this.Forest, input, ref propabilities);
                    for (int i = 0; i < propabilities.Length; i++)
                    {
                        double ra = (double)(1 + Math.Pow(-Math.Log(ThreadLocalRandom.NextDouble(), Math.E), this.alpha));
                        double neighbour = GetNeighbourAffect(row, col, this.landInfo.AllTypes[i].LandUseTypeValue, this.sizeOfNeighbour);
                        propabilities[i] = propabilities[i] * ra * neighbour;

                    }

                    int idx = getIdxFromPropArr(propabilities);
                    
                    if (idx == this.landInfo.UrbanIndex)
                    {
                        // Interlocked.Add(ref nowCityCnt, 1);
                        // this.nowCityCount++;
                        nowCityCnt++;
                    }

                    if (this.landInfo.AllTypes[idx].LandUseTypeValue == this.landInfo.WaterInfo.LandUseTypeValue) // target 不能为水体
                    {
                        AddChartCellCountArr(middleBuffer[pos]);
                        return;
                    }


 

                    // 转换控制
                    int srcIdx = 0;
                    for(int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
                    {
                        if(middleBuffer[pos] == this.landInfo.AllTypes[i].LandUseTypeValue)
                        {
                            srcIdx = i;
                        }
                    }
                    if (this.transformControlMatrix[srcIdx, idx] == 0)
                    {
                        // 不转换
                        AddChartCellCountArr(middleBuffer[pos]);
                        return;
                    }

                    middleBuffer[pos] = this.landInfo.AllTypes[idx].LandUseTypeValue;
                    if (middleBuffer[pos] != this.beginBuffer[pos])
                    {
                        numOfChangesOneTime++;
                    }
                    // 修改图表计数数组
                    AddChartCellCountArr(middleBuffer[pos]);

                });
                #endregion



                this.nowCityCount = nowCityCnt;
                this.updateConsoleEvent("---当前城市数目: " + this.nowCityCount);
                this.beginBuffer = middleBuffer;
                if(updateImageEvent != null)
                {
                    this.updateImageEvent(this.beginBuffer, this.width, this.height, this.landInfo,cnt);
                    
                }
                this.updateChartEvent(this.ChartCellCountArr, cnt + 1, landInfo);
                
                middleBuffer = new double[width * height];
                if(updateConsoleEvent != null)
                {
                    this.updateConsoleEvent("转化了:" + numOfChangesOneTime);
                }
                
                numOfChangesOneTime = 0;
                cnt++;
            }
            if (updateConsoleEvent != null)
            {
                this.updateConsoleEvent("模拟结束");
            }
            var kappa = new KappaTest(this.endBuffer, this.beginBuffer, width, height, landInfo);
            var kappaVal = kappa.GetVal();
            this.updateConsoleEvent("kappa值 : " + kappaVal);


        }
        /// <summary>
        /// 从概率转换数组中，得到概率值最大的索引
        /// </summary>
        /// <param name="propabilities"></param>
        /// <returns></returns>
        private int getIdxFromPropArr(double[] propabilities)
        {
            //归一化, 归一化后比较容易调参数
            double pSum = propabilities.Sum();
            for (int n = 0; n < propabilities.Length; n++)
            {
                propabilities[n] /= pSum;
            }
            
            double upAdjust = this.cityPropAdjust;

            double downadjust = upAdjust / (this.landInfo.NumOfLandUseTypes - 1);
            propabilities[this.landInfo.UrbanIndex] += upAdjust;
            for (int m = 0; m < this.landInfo.NumOfLandUseTypes; m++)
            {
                if (m == this.landInfo.UrbanIndex)
                {
                    continue;
                }
                propabilities[m] -= downadjust;
            }
            

            int idx;
            double max = propabilities.Max(out idx);
            return idx;
        }

        public void Run(Object obj)
        {
            int times = (int)obj;
            this.Train();

            this.ChartCountOfTypes = this.landInfo.NumOfLandUseTypes;
            this.ChartCellCountArr = new int[this.ChartCountOfTypes];
            this.InitialChart();

            this.Simulate(times);
            this.simulateEndEvent(this);
        }

        #endregion
    }
}
