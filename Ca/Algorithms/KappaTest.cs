using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ca.CommonClass;
namespace Ca.Algorithms
{
    /// <summary>
    /// 计算kappa值的类
    /// </summary>
    class KappaTest
    {
        #region fields

        private double[] realBuffer = null;
        private double[] simulateBuffer = null;
        private int width = 0;
        private int height = 0;
        private LandUseClassificationInfo landUseInfo = null;

        #endregion

        #region constructor
        public KappaTest(double[] realBuffer, double[] simulateBuffer, int width, int height,LandUseClassificationInfo landUseInfo)
        {
            this.realBuffer = realBuffer;
            this.simulateBuffer = simulateBuffer;
            this.landUseInfo = landUseInfo;
            this.height = height;
            this.width = width;
        }
        #endregion

        #region public methods
        public double GetVal()
        {
            var allTypes = this.landUseInfo.AllTypes;
            int[,] confusionMatrix = new int[allTypes.Count, allTypes.Count];

            int cellCount = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int pos = row * width + col;
                    var realVal = this.realBuffer[pos];
                    var simulateVal = this.simulateBuffer[pos];
                    // 空值跳过
                    // 为了避免浮点类型数值判等的bug
                    // 这里的判断有点绕
                    if (Math.Abs(realVal - this.landUseInfo.NullInfo.LandUseTypeValue) < Double.Epsilon || Math.Abs(simulateVal - this.landUseInfo.NullInfo.LandUseTypeValue) < Double.Epsilon || realVal < 0 || simulateVal < 0)
                    {
                        continue;
                    }
                    cellCount++;
                    /**
                    *  
                    */
                    int idxR = GetIdx(realVal);// 1维(行)代表real
                    int idxS = GetIdx(simulateVal);// 2维(列)代表simulate
                    confusionMatrix[idxR, idxS] += 1;
                }
            }

            return this.ComputeKappa(confusionMatrix);
        }
        #endregion

        #region private methods
        private int GetIdx(double cellVal)
        {
            for (int i = 0; i < this.landUseInfo.AllTypes.Count; i++)
            {
                if (this.landUseInfo.AllTypes[i].LandUseTypeValue == cellVal)
                {
                    return i;
                }
            }
            throw new Exception("索引异常");
        }


        /// <summary>
        /// 计算一个给定矩阵的kappa值
        /// </summary>
        /// <param name="confusionMatrix"></param>
        /// <returns></returns>
        private double ComputeKappa(int[,] confusionMatrix)
        {
            int len = confusionMatrix.GetLength(0);
            int cellCount = 0;
            int[] rowSum = new int[len];
            int[] colSum = new int[len];
            double diagSum = 0;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    var val = confusionMatrix[i, j];
                    cellCount += val;
                    rowSum[i] += val;
                    colSum[j] += val;
                    if (i == j)
                    {
                        diagSum += val; // 对角线和
                    }
                }
            }
            double tmp = 0;
            for (int k = 0; k < len; k++)
            {
                tmp += rowSum[k] * colSum[k];
            }
            double p0 = diagSum / cellCount;
            double pc = tmp / cellCount / cellCount;
            return (p0 - pc) / (1 - pc);

        }
        #endregion
    }
}
