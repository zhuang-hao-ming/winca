using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OSGeo.GDAL;
using Accord.Math;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.MachineLearning.DecisionTrees.Pruning;
using System.Windows.Forms;
using Accord.MachineLearning.DecisionTrees.Rules;
using Ca.CommonClass;
using Ca.Algorithms;
using System.Threading.Tasks;

namespace Ca.CaClass
{
    /// <summary>
    /// 决策树类
    /// </summary>
    class DecisionTreeCa : BaseCa
    {

        

        




        #region fields
        
        
        
        double sampleRate = 0.01;
        
        Func<double[], int> func = null;
        


        Random rnd = null;
        #endregion


        #region property
        /// <summary>
        /// 起始城市栅格
        /// </summary>
        public int BeginCityCnt { get; set; }
        /// <summary>
        /// 目标城市栅格
        /// </summary>
        public int EndCityCnt { get; set; }

        /// <summary>
        /// 采样率
        /// </summary>
        public double SampleRate
        {
            get
            {
                return this.sampleRate;
            }
            set
            {
                this.sampleRate = value;
            }
        }



        #endregion

        #region private methods

  

        /// <summary>
        /// 获得邻域影响值
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="cellRow"></param>
        /// <param name="cellCol"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private double GetNeighbourAffect(double[] buffer, int width, int height, int cellRow, int cellCol, int step)
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
                    double type = buffer[pos];
                    if (this.landInfo.IsExistInUrbanInfos(type))
                    {
                        cnt++;
                    }
                }
            }
            return (double)cnt / all;
        }


        /// <summary>
        /// 构建决策树
        /// </summary>
        public void BuildTree()
        {

            // 采样
            List<Cell> samplePoints = getSample();
            updateConsoleEvent("-----------");
            updateConsoleEvent("起始城市栅格数目：" + this.BeginCityCnt);
            updateConsoleEvent("目标城市栅格数目：" + this.EndCityCnt);
            updateConsoleEvent("-----------");
            updateConsoleEvent("-----------开始训练----------");

            // 样本数目
            int COUNT = samplePoints.Count;

            // 构造输入和输出数据集
            double[][] inputs = new double[COUNT][];
            int[] outputs = new int[COUNT];
            for (int i = 0; i < COUNT; i++)
            {
                Cell cell = samplePoints[i];
                int pos = cell.row * width + cell.col;
                List<double> input = (from buffer in driveBufferList
                                      select buffer[pos]).ToList<double>();
                input.Add(GetNeighbourAffect(beginBuffer, width, height, cell.row, cell.col, 3));
                inputs[i] = input.ToArray<double>();
                if(this.landInfo.UrbanInfos[0].LandUseTypeValue == (int)beginBuffer[pos])
                {
                    outputs[i] = 1;
                } else
                {
                    outputs[i] = 0;
                }
                
            }


            // 训练数据集
            var trainingInputs = inputs.Submatrix(0, COUNT / 2 - 1);
            var trainingOutput = outputs.Submatrix(0, COUNT / 2 - 1);

            // 检验数据集
            var pruningInputs = inputs.Submatrix(COUNT / 2, COUNT - 1);
            var pruningOutput = outputs.Submatrix(COUNT / 2, COUNT - 1);

            // 设置驱动因子的名字
            List<DecisionVariable> featuresList = (from column in this.driveLayerNames
                                                   select new DecisionVariable(column, DecisionVariableKind.Continuous)).ToList<DecisionVariable>();
            featuresList.Add(new DecisionVariable("affectofneighbour", DecisionVariableKind.Continuous));

            // 训练树
            var tree = new DecisionTree(inputs: featuresList, classes: 2);
            var teacher = new C45Learning(tree);
            teacher.Learn(trainingInputs, trainingOutput);

            // 剪枝
            ErrorBasedPruning prune = new ErrorBasedPruning(tree, pruningInputs, pruningOutput);
            prune.Threshold = 0.1;// Gain threshold ？
            double lastError;
            double error = Double.PositiveInfinity;
            do
            {
                lastError = error;
                error = prune.Run();

            } while (error < lastError);

            updateConsoleEvent("错误率：" + error);

            this.func = tree.ToExpression().Compile();
            //UpdateUi("错误率" + error);
            
            DecisionSet rules = tree.ToRules();
            string ruleText = rules.ToString();
            //consolePad.addLineToInfo(ruleText);
            updateConsoleEvent("规则:");
            updateConsoleEvent(ruleText);
            updateConsoleEvent("-----------训练结束----------");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Cell> getSample()
        {
            int beginCityCnt = 0;
            int count = (int)(this.width * this.height * sampleRate) * 2; // 采样数目为采样率的两倍，多出来的一倍用于剪枝
            // 首先聚集城市和非城市点。
            // 可以实现分类采样，并且提高采样效率
            List<Cell> cityPoints = new List<Cell>();
            List<Cell> noCityPoints = new List<Cell>();
            for (int row = 0; row < this.height; row++)
            {
                for (int col = 0; col < this.width; col++)
                {
                    int pos = row * width + col;
                    double type = this.beginBuffer[pos];
                    
                    // 从城市栅格中采样
                    if (this.landInfo.IsExistInUrbanInfos(type))
                    {
                        beginCityCnt++;
                        cityPoints.Add(new Cell { row = row, col = col });
                    }
                    // 从非城市但可以转化为城市的栅格中采样
                    if (this.landInfo.IsExistInConvertableInfos(type))
                    {
                        noCityPoints.Add(new Cell { row = row, col = col });
                    }
                }
            }

            this.BeginCityCnt = beginCityCnt;

            List<Cell> samplePoints = new List<Cell>();
            
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    int idx = rnd.Next(cityPoints.Count);
                    samplePoints.Add(cityPoints[idx]);
                    cityPoints.RemoveAt(idx);
                }
                else
                {
                    int idx = rnd.Next(noCityPoints.Count);
                    samplePoints.Add(noCityPoints[idx]);
                    noCityPoints.RemoveAt(idx);
                }
            }

            return samplePoints;
        }

        #endregion


        #region constructor

        /// <summary>
        /// 这里的名字是图层的名字
        /// </summary>
        /// <param name="inputColumnNames">驱动因子列名字列表</param>
        /// <param name="outputColumnName">结果列名字</param>
        public DecisionTreeCa(List<string> driveLayerNames, string beginLayernName, string endLayerName)
        {
            this.driveLayerNames = driveLayerNames;
            this.beginLayernName = beginLayernName;
            this.endLayerName = endLayerName;
            this.rnd = new Random();
            //准备数据
            LoadData();

            
        }

        #endregion


        #region public methods

        public void Simulate(object obj)
        {
            int nowCityCnt = this.BeginCityCnt;
            int times = (int)obj;
            
            // 开始模拟            
            int cnt = 0;
            
            double[] middleBuffer = new double[width * height];
            while (cnt < times)
            {
                int addedCity = 0;
                if (nowCityCnt > this.EndCityCnt)
                {
                    break;
                }
                
                // 清空图表计数数组
                this.ClearChartCellCountArr();
                #region parallel for
                Parallel.For(0, height * width, pos =>
                {

                    int row = pos / width;
                    int col = pos % width;

                    middleBuffer[pos] = beginBuffer[pos];
                    double type = beginBuffer[pos];
                    // 只考虑可以转化为城市的栅格
                    if (this.landInfo.IsExistInConvertableInfos(type))
                    {
                        List<double> input = (from buffer in driveBufferList
                                              select buffer[pos]).ToList<double>();
                        input.Add(GetNeighbourAffect(beginBuffer, width, height, row, col, 3));
                        double[] inputArr = input.ToArray<double>();
                        int newType = func(inputArr);
                        // 如果转化为城市
                        if (newType == 1 && ThreadLocalRandom.NextDouble() < 0.1)
                        {
                            middleBuffer[pos] = this.landInfo.UrbanInfos[0].LandUseTypeValue;
                            addedCity++;
                            nowCityCnt++;
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
                //        double type = beginBuffer[pos];
                //        // 只考虑可以转化为城市的栅格
                //        if (this.landInfo.IsExistInConvertableInfos(type))
                //        {
                //            List<double> input = (from buffer in driveBufferList
                //                                  select buffer[pos]).ToList<double>();
                //            input.Add(GetNeighbourAffect(beginBuffer, width, height, row, col, 3));
                //            double[] inputArr = input.ToArray<double>();
                //            int newType = func(inputArr);
                //            // 如果转化为城市
                //            if (newType == 1 && rnd.NextDouble() < 0.1)
                //            {
                //                middleBuffer[pos] = this.landInfo.UrbanInfos[0].LandUseTypeValue;
                //                addedCity++;
                //                nowCityCnt++;
                //            }                            
                //        }                                                                       
                //        // 修改图表计数数组
                //        AddChartCellCountArr(middleBuffer[pos]);
                //    }
                //}
                #endregion

                // 将结果复制会outputBuffer
                for (int pos = 0; pos < width * height; pos++)
                {
                    beginBuffer[pos] = middleBuffer[pos];
                }
                updateConsoleEvent("-------");
                updateConsoleEvent("当前城市数目:" + nowCityCnt);
                updateChartEvent(ChartCellCountArr, cnt + 1, landInfo);
                updateImageEvent(beginBuffer, width, height, this.landInfo, cnt); // 通知主线程绘图                
                updateConsoleEvent("新增： " + addedCity); // 通知主线程输出控制台消息
                cnt++;
            }
            updateConsoleEvent("模拟结束");
            var kappa = new KappaTest(this.endBuffer, this.beginBuffer, width, height, landInfo);
            var kappaVal = kappa.GetVal();
            this.updateConsoleEvent("kappa值 : " + kappaVal);
        }

        public void Run(object obj)
        {
            this.ChartCountOfTypes = this.landInfo.NumOfLandUseTypes;
            this.ChartCellCountArr = new int[this.ChartCountOfTypes];
            this.InitialChart();
            BuildTree();
            Simulate(obj);
            this.simulateEndEvent(this);
        }

        #endregion



    }


}
