using System.Diagnostics;

namespace lab1
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private ProgressBar[] progressBars;
        private bool isRunning = false;

        private List<TimeSpan> finishTimes = new List<TimeSpan>();

        public Form1()
        {
            InitializeComponent();
            progressBars = new ProgressBar[] { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5 };
            listView1.Columns.Add("Horse", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Finish Time", 100, HorizontalAlignment.Center);
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (isRunning)
                return;

            isRunning = true;
            button1.Enabled = false;

            listView1.Items.Clear();
            finishTimes.Clear();

            var tasks = progressBars.Select((progressBar, index) =>
                Task.Run(() => FillProgressBar(progressBar, index + 1))).ToArray();

            await Task.WhenAll(tasks);

            button1.Enabled = true;
            isRunning = false;
        }

        private async Task FillProgressBar(ProgressBar progressBar, int horseNumber)
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int delay = random.Next(10, 90);

            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(delay);

                Invoke(new Action(() =>
                {
                    progressBar.Value = i;
                }));
            }

            stopwatch.Stop();
            finishTimes.Add(stopwatch.Elapsed);

            Invoke(new Action(() =>
            {
                ListViewItem item = new ListViewItem($"Horse {horseNumber}");
                item.SubItems.Add(stopwatch.Elapsed.ToString());
                listView1.Items.Add(item);
            }));
        }
    }
}
