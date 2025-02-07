using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CalcRunner
{
    public partial class CalcRunner : Form
    {
        public CalcRunner()
        {
            InitializeComponent();
            myProcess.StartInfo.FileName = "calc.exe";
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (myProcess.StartInfo.FileName != "")
            {
                myProcess.Start();
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/IM CalculatorApp.exe /F",
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
    }
}
