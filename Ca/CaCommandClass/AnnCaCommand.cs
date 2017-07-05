using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;


using System.Threading;
using Ca.CaDialog;
using Ca.CaClass;
using Ca.CommonClass;
using Ca.CommonDialog;
using System.Windows.Forms;



namespace Ca.CaCommandClass
{
    class AnnCaCommand: CaCommandBase
    {
        #region fields 

        AnnCa network = null;

        #endregion
        public void Run()
        {

            // 显示设置参数窗口
            var setUpForm = new AnnCaSetUpForm();
            setUpForm.ShowDialog();

            if (setUpForm.DialogResult != DialogResult.OK)
            {


            }
            else
            {

          

                // 获得参数
                string beginLayerName = setUpForm.beginLayerName;
                string endLayerName = setUpForm.endLayerName;
                List<string> driveLayerNames = setUpForm.driveLayerNames;

                int numOfSample = setUpForm.NumOfSample;
                int numOfTrain = setUpForm.NumOfTrain;
                int numOfSimulate = setUpForm.NumOfSimulate;
                int sizeOfNeighbour = setUpForm.SizeOfNeighbour;
                double threshold = setUpForm.Threshold;
                double alpha = setUpForm.Alpha;

                LandUseClassificationInfo landInfo = setUpForm.LandUse;

                

                //string beginLayerName = "2001";
                //string endLayerName = "2006";
                //List<string> driveLayerNames = new List<string>() { "dem", "distocity", "distohighway", "distorailway", "distoroad", "distotown", "slope" };

                if (landInfo == null)
                {
                        landInfo = new LandUseClassificationInfo();
                        landInfo.NullInfo = new StructLanduseInfo() { LandUseTypeColor = Color.White.ToArgb(), LandUseTypeName = "NULL", LandUseTypeValue = -3.40282306074e+038 };
                        landInfo.AllTypes = new List<StructLanduseInfo>()
                        {
                            new StructLanduseInfo() {LandUseTypeName = "城市", LandUseTypeValue = 1, LandUseTypeColor = Color.Black.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "水", LandUseTypeValue = 2, LandUseTypeColor = Color.Blue.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "田", LandUseTypeValue = 3, LandUseTypeColor = Color.Yellow.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "森林", LandUseTypeValue = 4, LandUseTypeColor = Color.Green.ToArgb()},
                            new StructLanduseInfo() {LandUseTypeName = "果园", LandUseTypeValue = 5, LandUseTypeColor = Color.LightPink.ToArgb()}
                        };
                }


            
                // 设置网络
                this.network = new AnnCa(beginLayerName, endLayerName, driveLayerNames, landInfo);
                network.NumOfSamples = numOfSample;
                network.TimesOfTrain = numOfTrain;
                network.Threshold = threshold;
                network.Alpha = alpha;
                network.SizeOfNeighbour = sizeOfNeighbour;
                network.EndCityCnt = setUpForm.EndCityCnt;
                network.TransformControlMatrix = setUpForm.TransformControlMatrix;
                network.updateConsoleEvent += this.UpdateConsole;
                network.updateImageEvent += this.UpdateImage;
                network.updateChartEvent += this.UpdateChart;
                network.simulateEndEvent += this.SimulateEnd;
                
                // 设置图像显示
                this.imageForm = new ImageForm(network.BeginBuffer, network.Width, network.Height, landInfo);
                
                this.imageForm.Show();
                //// 开始模拟

                Thread threadSimulate = new Thread(new ParameterizedThreadStart(network.Run));
                
                threadSimulate.IsBackground = true;
                threadSimulate.Start(numOfSimulate);

                this.imageForm.ThreadSimulate = threadSimulate;
                this.imageForm.ActiveButton();


                //Action<object> simulate = network.Run;
                //simulate.BeginInvoke(numOfSimulate, null, null);


            }
        }
    }
}
