using System;
using System.Collections.Generic;

using System.Windows.Forms;



using System.Drawing;
using Ca.CommonDialog;
using Ca.CaClass;
using Ca.CaDialog;
using Ca.CommonClass;
using System.Threading;

namespace Ca.CaCommandClass

{
    class DecisionTreeCommand: CaCommandBase
    {

        public void Run()
        {
            
            // 读取设置参数
            var form = new DTCaSetUpForm();
            form.ShowDialog();

            if (form.DialogResult != DialogResult.OK)
            {
                // 错误终止
                return;
            }
            else
            {

                
                LandUseClassificationInfo landUseInfo = form.LandUse;
                // 默认的土地利用信息
                if (landUseInfo == null)
                {
                    landUseInfo = new LandUseClassificationInfo();
                    landUseInfo.AllTypes = new List<StructLanduseInfo>() 
                    {
                        new StructLanduseInfo(){LandUseTypeName="城市", LandUseTypeValue = 1.0, LandUseTypeColor = Color.Black.ToArgb()},
                        new StructLanduseInfo(){LandUseTypeName="非城市", LandUseTypeValue = 0.0, LandUseTypeColor = Color.White.ToArgb()},
                        new StructLanduseInfo(){LandUseTypeName="水体", LandUseTypeValue = 2.0, LandUseTypeColor = Color.Blue.ToArgb()},
                    };
                    landUseInfo.ConvertableInfos.Add(new StructLanduseInfo() { LandUseTypeName = "非城市", LandUseTypeValue = 0.0, LandUseTypeColor = Color.White.ToArgb() });
                    landUseInfo.NullInfo = new StructLanduseInfo() { LandUseTypeName = "空值", LandUseTypeValue = -9999.0, LandUseTypeColor = Color.Transparent.ToArgb() };
                    landUseInfo.UnConvertableInfos.Add(new StructLanduseInfo() { LandUseTypeName = "水体", LandUseTypeValue = 2.0, LandUseTypeColor = Color.Blue.ToArgb() });
                    landUseInfo.UrbanInfos.Add(new StructLanduseInfo() { LandUseTypeName = "城市", LandUseTypeValue = 1.0, LandUseTypeColor = Color.Black.ToArgb() });
                }           

                // 初始化控制台
                
                // 初始化树                
                var tree = new DecisionTreeCa(form.driveLayerNames, form.beginLayerName, form.endLayerName);
                
                // 注册事件
                tree.updateConsoleEvent += UpdateConsole;
                tree.updateImageEvent += UpdateImage;
                tree.simulateEndEvent += SimulateEnd;
                tree.updateChartEvent += UpdateChart;             
                // 设置参数                
                tree.LandInfo = landUseInfo;
                tree.SampleRate = form.RateOfSample;
                tree.EndCityCnt = form.EndCityCnt;             
                // 显示模拟图像
                this.imageForm = new ImageForm(tree.BeginBuffer, tree.Width, tree.Height, landUseInfo);
                this.imageForm.Width = tree.Width;
                this.imageForm.Height = tree.Height;                
                imageForm.Show();

                //// 线程池
                //Action<object> simulate = tree.Run;
                //simulate.BeginInvoke(form.NumOfSimulate, null, null);

                Thread threadSimulate = new Thread(new ParameterizedThreadStart(tree.Run));
                threadSimulate.IsBackground = true;
                threadSimulate.Start(form.NumOfSimulate);

                this.imageForm.ThreadSimulate = threadSimulate;
                this.imageForm.ActiveButton();
            }
        }

        
    }
}
