using Ca.CaClass;
using Ca.CommonClass;
using Ca.CommonDialog;
using Ca.CaDialog;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ca.CaCommandClass
{
    class LgCaCommand: CaCommandBase
    {
        public void Run()
        {
            var form = new LgCaSetUpForm();
            form.ShowDialog();

            if (form.DialogResult != DialogResult.OK)
            {
                return;
            }
            else
            {
                

                string beginLayerName = form.BeginLayerName;//"city2001_012"
                string endLayerName = form.EndLayerName;//"city2006_012";
                List<string> driveLayerNames = form.DriveLayerNames;
                LandUseClassificationInfo landUseInfo = form.LandUseInfo;
                // 默认土地利用信息
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

                // 从设置界面获得参数
                int sizeOfNeighbour = form.SizeOfNeighbour;//3;
                double alpha = form.Alpha;//1;
                int timeOfSimulate = form.TimeOfSimulate;//200;
                double threshold = form.Threshold;
                int numOfSample = form.NumOfSample;
                var ca = new LgCa(beginLayerName, endLayerName, driveLayerNames);
                ca.LandInfo = landUseInfo;
                ca.Threshold = threshold;
                ca.TimeOfSimulate = timeOfSimulate;
                ca.SizeOfNeighbour = sizeOfNeighbour;
                ca.NumOfSample = numOfSample;
                ca.Alpha = alpha;
                ca.EndCityCnt = form.EndCityCnt;
                ca.updateConsoleEvent += this.UpdateConsole;
                ca.updateImageEvent += this.UpdateImage;
                ca.simulateEndEvent += this.SimulateEnd;
                ca.updateChartEvent += this.UpdateChart;
                // 显示图像
                this.imageForm = new ImageForm(ca.BeginBuffer, ca.Width, ca.Height, landUseInfo);
                this.imageForm.Show();
                //Action simulate = ca.Simulate;
                //simulate.BeginInvoke(null, null);
                Thread threadSimulate = new Thread(new ThreadStart(ca.Simulate));
                threadSimulate.IsBackground = true;
                threadSimulate.Start();

                this.imageForm.ThreadSimulate = threadSimulate;
                this.imageForm.ActiveButton();

            }
        }
    }
}
