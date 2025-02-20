using System.Threading;

namespace homework
{
    public partial class Form1 : Form
    {
        private const string GUID = "9FD4D217-9CAC-4BEC-ACA2-7BAF4D4C5E5F";
        private int threadCounter = 1;
        private Semaphore semaphore;
        private Dictionary<int, CancellationTokenSource> activeThreads = new();
        private Queue<int> waitingThreads = new();
        private SynchronizationContext uiContext;

        public Form1()
        {
            InitializeComponent();

            semaphore = new Semaphore(1, 1, GUID);
            uiContext = SynchronizationContext.Current;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int threadId = threadCounter++;
            listBox3.Items.Add(threadId);
        }

        private void listBox3_DoubleClick_1(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem is int threadId)
            {
                listBox3.Items.Remove(threadId);
                listBox2.Items.Add(threadId);
                waitingThreads.Enqueue(threadId);
                AttemptToStartPendingThreads();
            }
        }

        private async void AttemptToStartPendingThreads()
        {
            while (waitingThreads.Count > 0)
            {
                if (!semaphore.WaitOne(0))
                    return;

                int threadId = waitingThreads.Dequeue();
                listBox2.Items.Remove(threadId);
                listBox1.Items.Add(threadId);

                var cts = new CancellationTokenSource();
                activeThreads[threadId] = cts;

                await Task.Run(() => ExecuteThread(threadId, cts.Token));
            }
        }

        private void ExecuteThread(int threadId, CancellationToken token)
        {
            int counter = 0;
            while (!token.IsCancellationRequested)
            {
                counter++;
                uiContext.Post(state => UpdateThreadDisplay(threadId, counter), null);
                Thread.Sleep(1000);
            }
            semaphore.Release();
        }

        private void UpdateThreadDisplay(int threadId, int counter)
        {
            if (listBox1.Items.Contains(threadId))
            {
                int index = listBox1.Items.IndexOf(threadId);
                listBox1.Items[index] = "Thread " + threadId + ": " + counter + " seconds";
            }
        }

        private void listBox1_DoubleClick_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is string selectedItem && int.TryParse(selectedItem.Split(' ')[0], out int threadId))
            {
                if (activeThreads.TryGetValue(threadId, out var cts))
                {
                    cts.Cancel();
                    activeThreads.Remove(threadId);
                    listBox1.Items.Remove(selectedItem);
                    AttemptToStartPendingThreads();
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int newLimit = int.Parse(numericUpDown1.Value.ToString());
            semaphore = new Semaphore(newLimit, newLimit, GUID);
            AttemptToStartPendingThreads();
        }
    }
}