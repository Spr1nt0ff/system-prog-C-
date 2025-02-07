using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework
{
    public partial class Form1 : Form
    {
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

        private async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            const int bufferSize = 4096;
            string destinationFile = Path.Combine(destinationPath, Path.GetFileName(sourcePath));

            using (FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
            using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
            {
                byte[] buffer = new byte[bufferSize];
                int totalBytes = (int)sourceStream.Length;
                int bytesCopied = 0;
                int bytesRead;

                while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await destinationStream.WriteAsync(buffer, 0, bytesRead);
                    bytesCopied += bytesRead;
                    int progress = (bytesCopied * 100) / totalBytes;
                    progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = progress));
                }
            }
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            string sourcePath = textBox1.Text;
            string destinationPath = textBox2.Text;

            if (File.Exists(sourcePath) && !string.IsNullOrEmpty(destinationPath))
            {
                progressBar1.Value = 0;
                button3.Enabled = false;

                await CopyFileAsync(sourcePath, destinationPath);

                button3.Enabled = true;
                MessageBox.Show("Копирование завершено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Убедитесь, что исходный файл существует и путь назначения корректен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}