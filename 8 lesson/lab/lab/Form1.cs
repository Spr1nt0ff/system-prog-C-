using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private ProgressBar[] progressBars;
        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();
            progressBars = new ProgressBar[] { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5 };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
 
            if (isRunning)
                return;

            isRunning = true;
            button1.Enabled = false;

            var tasks = progressBars.Select(progressBar => Task.Run(() => FillProgressBar(progressBar))).ToArray();
            await Task.WhenAll(tasks);

            button1.Enabled = true;
            isRunning = false;
        }

        private async Task FillProgressBar(ProgressBar progressBar)
        {

            int delay = random.Next(10, 90);

            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(delay);

                Invoke(new Action(() =>
                {
                    progressBar.Value = i;
                }));
            }
        }
    }
}
