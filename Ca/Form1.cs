using Ca.CaCommandClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ca
{
    public partial class Form1 : Form
    {
        private StringBuilder consoleInfo = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();
        }

        private void randomForestCa_Click(object sender, EventArgs e)
        {
            var ca = new RandomForestCaCommand();
            ca.Run();
        }

        private void annCa_Click(object sender, EventArgs e)
        {
            var ca = new AnnCaCommand();
            ca.Run();
        }

        private void logisticCa_Click(object sender, EventArgs e)
        {
            var ca = new LgCaCommand();
            ca.Run();
        }

        private void dtCa_Click(object sender, EventArgs e)
        {
            var ca = new DecisionTreeCommand();
            ca.Run();
        }

        public void AddLineToConsole(string line)
        {
            this.consoleInfo.AppendLine(line);
            this.textBoxConsole.Text = this.consoleInfo.ToString();
            this.textBoxConsole.SelectionStart = this.textBoxConsole.Text.Length;
            this.textBoxConsole.ScrollToCaret();
        }


    }
}
