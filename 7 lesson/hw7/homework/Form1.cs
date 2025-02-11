using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        private int bytesCopied = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderDialog.SelectedPath;
            }
        }

        private async Task CopyFileAsync(string sourcePath, string destinationPath, CancellationToken cancellationToken)
        {
            const int bufferSize = 4096;
            string destinationFile = Path.Combine(destinationPath, Path.GetFileName(sourcePath));

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
            using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
            {
                byte[] buffer = new byte[bufferSize];
                int totalBytes = (int)sourceStream.Length;

                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    int bytesRead = await Task.Factory.StartNew(() => sourceStream.Read(buffer, 0, buffer.Length), cancellationToken);
                    if (bytesRead == 0) break;

                    await Task.Factory.StartNew(() => destinationStream.Write(buffer, 0, bytesRead), cancellationToken);
                    bytesCopied += bytesRead;

                    await Task.Delay(800);

                    int progress = (bytesCopied * 100) / totalBytes;
                    progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = progress));
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string sourcePath = textBox1.Text;
            string destinationPath = textBox2.Text;

            if (File.Exists(sourcePath) && !string.IsNullOrEmpty(destinationPath))
            {
                progressBar1.Value = 0;
                button3.Enabled = false;
                cancellationTokenSource = new CancellationTokenSource();
                bytesCopied = 0;

                Task.Factory.StartNew(async () =>
                {
                    await CopyFileAsync(sourcePath, destinationPath, cancellationTokenSource.Token);
                    button3.Invoke((MethodInvoker)(() => button3.Enabled = true));
                }, cancellationTokenSource.Token);
            }
            else
            {
                MessageBox.Show("Убедитесь, что исходный файл существует и путь назначения корректен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                MessageBox.Show($"Операция остановлена! Прочитано байт: {bytesCopied}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
