using System;
using System.Collections.Generic;
using System.Linq;

using Accord.Neuro;
using Accord.Neuro.Learning;

using Accord.Math;
using Ca.CommonClass;
using Ca.Algorithms;
using System.Threading.Tasks;

namespace Ca.CaClass
{
    class AnnCa: BaseCa
    {

        #region fields

        double alpha = 1;
        double threshold = 0.7;
        int numOfSample = 3000;
        int timesOfTrain = 1000;
        int sizeOfNeighbour = 7;
        ActivationNetwork network = null;




        #endregion


        #region properties
        /// <summary>
        ///  起始城市栅格数目
        /// </summary>
        public int BeginCityCnt { get; set; }
        /// <summary>
        /// 目标城市栅格数目
        /// </summary>
        public int EndCityCnt { get; set; }
        /// <summary>
        /// 邻域半径
        /// </summary>
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

        public int TimesOfTrain
        {
            get
            {
                return this.timesOfTrain;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("训练次数要求是正整数");
                }
                else
                {
                    this.timesOfTrain = value;
                }
                
            }
        }

        /// <summary>
        /// 采样数目
        /// </summary>
        public int NumOfSamples
        {

            get
            {
                return this.numOfSample;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("样本数要是正整数");
                }
                else
                {
                    this.numOfSample = value;
                }

            }
        }

        /// <summary>
        /// 转换的阈值
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
        /// 随机因子中的系数
        /// </summary>
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
        /// 土地类型转换控制矩阵
        /// </summary>
        public int[,] TransformControlMatrix { get; set; }



        #endregion


        #region private methods

        /// <summary>
        /// sigmoid函数
        /// </summary>
        /// <param name="net"></param>
        /// <returns></returns>
        private double Sigmoid(double net)
        {
            return (double)(1.0 / (1.0 + Math.Exp(-net)));
        }

        /// <summary>
        /// 采样,按照土地类型等比例采样，土地类型改变和不变各采样一半
        /// </summary>
        /// <param name="count">样本数目</param>
        /// <returns></returns>
        private List<Cell> getSamples(int count)
        {
            List<Cell> samples = new List<Cell>();
            long totalNumOfCells = 0;


            // dimension 1 for different landuse dimension 2 for change or no-change
            List<Cell>[][] landUseChnageType = new List<Cell>[this.landInfo.NumOfLandUseTypes][];
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                landUseChnageType[i] = new List<Cell>[2];
                landUseChnageType[i][0] = new List<Cell>(); // same
                landUseChnageType[i][1] = new List<Cell>();  // change
            }
            int beginCityCnt = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    // 非空值,对于带有小数点的数，判等要在一定的精度条件下判断
                    if (beginBuffer[pos] > 0 && endBuffer[pos] > 0 && Math.Abs(beginBuffer[pos] - this.landInfo.NullInfo.LandUseTypeValue) > Double.Epsilon && Math.Abs(endBuffer[pos] - this.landInfo.NullInfo.LandUseTypeValue) > Double.Epsilon)
                    {

                        for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
                        {
                            if (this.landInfo.AllTypes[i].LandUseTypeValue == beginBuffer[pos])
                            {
                                if (i == this.landInfo.UrbanIndex)
                                {
                                    beginCityCnt++;
                                }

                                if (beginBuffer[pos] == endBuffer[pos])
                                {
                                    landUseChnageType[i][0].Add(new Cell() { row = row, col = col }); // 不变
                                }
                                else
                                {
                                    landUseChnageType[i][1].Add(new Cell() { row = row, col = col }); // 变

                                }
                                totalNumOfCells++;
                            }
                        }
                    }

                }
            }
            this.BeginCityCnt = beginCityCnt;


            Random rnd = new Random();
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                int num = (int)((float)(landUseChnageType[i][0].Count + landUseChnageType[i][1].Count) / (float)totalNumOfCells * count);
                for (int j = 0; j < num; j++)
                {
                    if (j % 2 == 0)
                    {
                        if (landUseChnageType[i][0].Count != 0)
                        {
                            int idx = rnd.Next(landUseChnageType[i][0].Count);
                            Cell cell = landUseChnageType[i][0][idx];
                            samples.Add(cell);
                            landUseChnageType[i][0].RemoveAt(idx);

                        }
                    }
                    else
                    {
                        if (landUseChnageType[i][1].Count != 0)
                        {
                            int idx = rnd.Next(landUseChnageType[i][1].Count);
                            Cell cell = landUseChnageType[i][1][idx];
                            samples.Add(cell);
                            landUseChnageType[i][1].RemoveAt(idx);
                        }
                    }
                }

            }
            while (samples.Count != count)
            {
                int row = rnd.Next(height);
                int col = rnd.Next(width);
                int pos = row * width + col;
                // 跳过空值
                if (beginBuffer[pos] < 0 || endBuffer[pos] < 0 || Math.Abs(beginBuffer[pos] - this.landInfo.NullInfo.LandUseTypeValue) < Double.Epsilon || Math.Abs(endBuffer[pos] - this.landInfo.NullInfo.LandUseTypeValue) < Double.Epsilon)                
                {
                    continue;
                }
                samples.Add(new Cell() { row = row, col = col });

            }



            return samples;
        }


        /// <summary>
        /// 得到元胞的土地类型序号， 这个序号是神经网络的输出目标值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private int getOneClass(Cell cell)
        {
            
            int pos = cell.row * width + cell.col;
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                if (this.landInfo.AllTypes[i].LandUseTypeValue == this.endBuffer[pos])
                {
                    return i;
                }                
            }
            throw new Exception("-1" + this.beginBuffer[pos]);
            
        }


        private double GetError(double[][] inputs, double[][] outpus, int[] classes)
        {
            int numOfRight = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                double[] answer = this.network.Compute(inputs[i]);

                int expected = classes[i];
                int actual; 
                answer.Max(out actual);
                if (expected == actual)
                {
                    numOfRight++;
                }                
            }
            return (double)numOfRight / inputs.Length;
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
            step = step / 2; // 
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
            return cnt / all;
        }
        /// <summary>
        /// 得到一个元胞的神经网络输入
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private double[] getOneInput(Cell cell)
        {
            List<double> input = new List<double>();
            
            var pos = cell.row * width + cell.col;
            
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                // 考虑到土地利用类型值是整数，判断不会出现问题，所以此处直接判等
                if (this.landInfo.AllTypes[i].LandUseTypeValue == this.beginBuffer[pos])
                {
                    input.Add(1);
                }
                else
                {
                    input.Add(0);
                }
            }
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                input.Add(GetNeighbourAffect(cell.row, cell.col, this.landInfo.AllTypes[i].LandUseTypeValue, this.sizeOfNeighbour));
            }
            double[] drive = (from buffer in driveBufferList 
                       select buffer[pos]).ToArray<double>();
            input.AddRange(drive);

            return input.ToArray<double>();
        }

        /// <summary>
        /// 得到一个元胞的神经网络输出。
        /// 因为使用accord的函数可以实现，这个函数暂时废弃
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private double[] getOneOutput(Cell cell)
        {
            var output = new double[this.landInfo.NumOfLandUseTypes];
            int pos = cell.row * width + cell.col;
            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
            {
                if (this.landInfo.AllTypes[i].LandUseTypeValue == this.endBuffer[pos])
                {
                    output[i] = 1.0;
                }
                else
                {
                    output[i] = 0.0;
                }
            }
            return output;
        }

        #endregion

        #region public methods
        /// <summary>
        /// 训练神经网络
        /// </summary>
        public void Train()
        {
            updateConsoleEvent("---------神经网络训练开始------");
            var samples = getSamples(this.numOfSample);

            double[][] inputs = (from cell in samples
                                 select getOneInput(cell)).ToArray<double[]>();


            int[] classes = (from cell in samples
                             select getOneClass(cell)).ToArray<int>();

            double[][] outputs = Accord.Statistics.Tools.Expand(classes, 0, +1);

            // Create an activation function for the net
            //var function = new BipolarSigmoidFunction();
            var function = new SigmoidFunction();
            

            // Create an activation network with the function and
            //  4 inputs, 5 hidden neurons and 3 possible outputs:
            int numOfInput = inputs[0].Length;
            int numOfHidden = numOfInput * 2 / 3;
            int numOfOut = outputs[0].Length;
            this.network = new ActivationNetwork(function, numOfInput, numOfHidden, numOfOut);

            // Randomly initialize the network
            new NguyenWidrow(this.network).Randomize();

            // Teach the network using parallel Rprop:
            var teacher = new ParallelResilientBackpropagationLearning(this.network);

            double correctRate = 0.0;
            int times = this.timesOfTrain;
            int cnt = 0;
            while (cnt < times)
            {
                teacher.RunEpoch(inputs, outputs);
                if (cnt % 10 == 0)
                {
                    correctRate = GetError(inputs, outputs, classes);
                    updateConsoleEvent("正确率：" + correctRate * 100 + "%");
                }
                cnt++;

            }
            updateConsoleEvent("训练结束。最终正确率：" + correctRate * 100 + "%");
            updateConsoleEvent("---------神经网络训练结束------");
        }

        



        /// <summary>
        /// 模拟
        /// </summary>
        public void Simulate(int times)
        {
            updateConsoleEvent("--------- 模拟开始");
            updateConsoleEvent("初始城市数目:" + this.BeginCityCnt);
            updateConsoleEvent("目标城市数目：" + this.EndCityCnt);
            updateConsoleEvent("---------");

            Random rnd = new Random();
            int cnt = 0;
            int numOfChangesOneTime = 0;
            double[] middleBuffer = new double[width * height];

            int nowCityCnt = this.BeginCityCnt;
            
            while (cnt < times)
            {
                if(nowCityCnt > this.EndCityCnt)
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

                    // null value 跳过
                    if (beginBuffer[pos] < 0 || Math.Abs(beginBuffer[pos] - this.landInfo.NullInfo.LandUseTypeValue) < Double.Epsilon)
                    {
                        return;                       
                    }
                    var input = getOneInput(new Cell() { row = row, col = col });
                    double[] answer = this.network.Compute(input);
                    int actual;
                    double max = answer.Max(out actual);

                    //double ra = (double)(1 + Math.Pow(-Math.Log(ThreadLocalRandom.NextDouble(), Math.E), this.alpha)); // -------todo 这个随机数太大了？？
                    double ra = ThreadLocalRandom.NextDouble();
                    if (max * ra > this.threshold)
                    {
                        // 转换控制
                        int srcIdx = 0;
                        for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
                        {
                            if (middleBuffer[pos] == this.landInfo.AllTypes[i].LandUseTypeValue)
                            {
                                srcIdx = i;
                            }
                        }
                        if (this.TransformControlMatrix[srcIdx, actual] == 0)
                        {
                            // 不转换
                            AddChartCellCountArr(middleBuffer[pos]);
                            return;
                        }

                        middleBuffer[pos] = landInfo.AllTypes[actual].LandUseTypeValue;

                        if (landInfo.AllTypes[actual].LandUseTypeValue != beginBuffer[pos])
                        {
                            if (actual == this.landInfo.UrbanIndex)
                            {
                                nowCityCnt++;
                            }
                            numOfChangesOneTime++;
                        }

                    }
                    // 修改图表计数数组
                    AddChartCellCountArr(middleBuffer[pos]);


                });
                #endregion
                #region sequential for
                //for (int row = 0; row < height; row++)
                //{

                //    for (int col = 0; col < width; col++)
                //    {
                //        int pos = row * width + col;
                //        middleBuffer[pos] = beginBuffer[pos];
                //        // null value 跳过
                //        if (beginBuffer[pos] < 0 || Math.Abs(beginBuffer[pos] - this.landInfo.NullInfo.LandUseTypeValue) <Double.Epsilon)                        
                //        {

                //            continue;
                //        }
                //        var input = getOneInput(new Cell() { row = row, col = col });
                //        double[] answer = this.network.Compute(input);
                //        int actual;
                //        double max = answer.Max(out actual);

                //        double ra = (double)(1 + Math.Pow(-Math.Log(rnd.NextDouble(), Math.E), this.alpha)); -------todo 这个随机数太大了？？
                //        //double ra = rnd.NextDouble();
                //        if (max * ra > this.threshold)
                //        {
                //            // 转换控制
                //            int srcIdx = 0;
                //            for (int i = 0; i < this.landInfo.NumOfLandUseTypes; i++)
                //            {
                //                if (middleBuffer[pos] == this.landInfo.AllTypes[i].LandUseTypeValue)
                //                {
                //                    srcIdx = i;
                //                }
                //            }
                //            if (this.TransformControlMatrix[srcIdx, actual] == 0)
                //            {
                //                // 不转换
                //                AddChartCellCountArr(middleBuffer[pos]);
                //                continue;
                //            }

                //            middleBuffer[pos] = landInfo.AllTypes[actual].LandUseTypeValue;

                //            if (landInfo.AllTypes[actual].LandUseTypeValue != beginBuffer[pos])
                //            {
                //                if (actual == this.landInfo.UrbanIndex)
                //                {
                //                    nowCityCnt++;
                //                }
                //                numOfChangesOneTime++;
                //            }

                //        }                       
                //        // 修改图表计数数组
                //        AddChartCellCountArr(middleBuffer[pos]);
                //    }
                //}
                #endregion

                this.beginBuffer = middleBuffer;
                updateConsoleEvent("------");
                updateConsoleEvent("当前城市数目:" + nowCityCnt);
                updateImageEvent(this.beginBuffer, width, height, this.landInfo, cnt);  // draw
                updateChartEvent(this.ChartCellCountArr, cnt+1, landInfo);
                middleBuffer = new double[width * height];
                updateConsoleEvent("转化了:" + numOfChangesOneTime);
                updateConsoleEvent("------");
                numOfChangesOneTime = 0;
                cnt++;
            }
            this.updateConsoleEvent("---------------模拟结束------------");
            var kappa = new KappaTest(this.endBuffer, this.beginBuffer, width, height, landInfo);
            var kappaVal = kappa.GetVal();
            this.updateConsoleEvent("kappa值 : " + kappaVal);


        }



        public void Run(object obj)
        {
            int times = (int)obj;
            this.Train();

            this.ChartCountOfTypes = this.landInfo.NumOfLandUseTypes;
            this.ChartCellCountArr = new int[this.ChartCountOfTypes];
            this.InitialChart();

            this.Simulate(times);
            this.simulateEndEvent(this); // 激发模拟结束事件
        }

        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginLayernName">训练模拟起始图层名字</param>
        /// <param name="endLayerName">训练模拟终止图层名字</param>
        /// <param name="driveLayerNames">驱动因子图层名字</param>
        public AnnCa(string beginLayernName, string endLayerName, List<string> driveLayerNames, LandUseClassificationInfo landInfo)
        {
            
            this.beginLayernName = beginLayernName;
            this.endLayerName = endLayerName;
            this.driveLayerNames = driveLayerNames;
            this.landInfo = landInfo;



            //准备数据
            LoadData();


        }
        #endregion










    }
}
