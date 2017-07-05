

using Ca.CaClass;
using Ca.CommonClass;
using Ca.CommonDialog;
using Ca.CaDialog;


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace Ca.CaCommandClass
{
    class RandomForestCaCommand : CaCommandBase
    {




        public void Run()
        {

            var form = new RandomForestSetUpForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                //// 初始化控制台
                //this.consolePad = WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).PadContent.Control as GIS.Common.Dialogs.Console.Console;
                //// 显示控制台
                //WorkbenchSingleton.Workbench.GetPad(typeof(ConsolePad)).BringPadToFront();

                string beginLayerName = form.BeginLayerName;
                string endLayerName = form.EndLayerName;
                List<string> driveLayerNames = form.DriveLayerNames;

                LandUseClassificationInfo landInfo = form.LandUse;
                if (landInfo == null)
                {
                    landInfo = new LandUseClassificationInfo();
                    landInfo.NullInfo = new StructLanduseInfo() { LandUseTypeColor = Color.White.ToArgb(), LandUseTypeName = "NULL", LandUseTypeValue = -3.40282306074e+038 };
                    landInfo.AllTypes = new List<StructLanduseInfo>()
                    {
                        new StructLanduseInfo() {LandUseTypeName = "城市", LandUseTypeValue = 1, LandUseTypeColor = Color.Black.ToArgb()},
                        new StructLanduseInfo() {LandUseTypeName = "水体", LandUseTypeValue = 2, LandUseTypeColor = Color.Blue.ToArgb()},
                        new StructLanduseInfo() {LandUseTypeName = "田", LandUseTypeValue = 3, LandUseTypeColor = Color.Yellow.ToArgb()},
                        new StructLanduseInfo() {LandUseTypeName = "森林", LandUseTypeValue = 4, LandUseTypeColor = Color.Green.ToArgb()},
                        new StructLanduseInfo() {LandUseTypeName = "果园", LandUseTypeValue = 5, LandUseTypeColor = Color.LightPink.ToArgb()}
                    };
                    landInfo.UrbanInfos.Add(new StructLanduseInfo() { LandUseTypeName = "城市", LandUseTypeValue = 1, LandUseTypeColor = Color.Black.ToArgb() });
                    
                }

                var randomForestCa = new RandomForestCa(beginLayerName, endLayerName, driveLayerNames, landInfo);
                randomForestCa.updateConsoleEvent += this.UpdateConsole;
                randomForestCa.updateImageEvent += this.UpdateImage;
                randomForestCa.simulateEndEvent += this.SimulateEnd;
                randomForestCa.updateChartEvent += this.UpdateChart;
                randomForestCa.NumOfSample = form.NumOfSample;
                randomForestCa.NumOfTree = form.NumOfTree;
                randomForestCa.SampleRatio = form.SampleRatio;
                randomForestCa.CoverageRatio = form.CoverageRatio;
                randomForestCa.SizeOfNeighbour = form.SizeOfNeighbour;
                randomForestCa.isNeedSignificant = form.IsNeedSignificant;
                randomForestCa.alpha = form.RandomFactor;
                randomForestCa.cityPropAdjust = form.cityPropAdjust;
                randomForestCa.transformControlMatrix = form.TransformControlMatrix;
                randomForestCa.targetCityCnt = form.TargetCityCnt;
                this.imageForm = new ImageForm(randomForestCa.BeginBuffer, randomForestCa.Width, randomForestCa.Height, randomForestCa.LandInfo);
                this.imageForm.Show();


                //Action<object> simulate = randomForestCa.Run;
                //simulate.BeginInvoke(form.NumOfSimulate, null, null);


                Thread threadSimulate = new Thread(new ParameterizedThreadStart(randomForestCa.Run));
                threadSimulate.IsBackground = true;
                threadSimulate.Start(form.NumOfSimulate);
                this.imageForm.ThreadSimulate = threadSimulate;
                this.imageForm.ActiveButton();
            }
            else
            {
                MessageBox.Show("参数设置错误！");
            }








        }
    }
}
