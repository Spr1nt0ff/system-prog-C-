using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace homework
{
    public partial class Form1 : Form
    {
        private delegate void CopyFileDelegate(string sourcePath, string destinationPath);
        private delegate void UpdateProgressDelegate(int progress);

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

        private void CopyFile(string sourcePath, string destinationPath)
        {
            const int bufferSize = 4096;
            FileStream sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
            FileStream destinationStream = new FileStream(Path.Combine(destinationPath, Path.GetFileName(sourcePath)), FileMode.Create, FileAccess.Write);

            byte[] buffer = new byte[bufferSize];
            int totalBytes = (int)sourceStream.Length;
            int bytesCopied = 0;
            int bytesRead;

            while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                destinationStream.Write(buffer, 0, bytesRead);
                bytesCopied += bytesRead;
                int progress = (bytesCopied * 100) / totalBytes;
                Invoke(new UpdateProgressDelegate(UpdateProgress), progress);
            }

            sourceStream.Close();
            destinationStream.Close();
        }

        private void UpdateProgress(int progress)
        {
            progressBar1.Value = progress;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string sourcePath = textBox1.Text;
            string destinationPath = textBox2.Text;

            if (File.Exists(sourcePath) && !string.IsNullOrEmpty(destinationPath))
            {
                progressBar1.Value = 0;
                button3.Enabled = false;

                Thread copyThread = new Thread(() => CopyFile(sourcePath, destinationPath));
                copyThread.Start();

                
            }
            else
            {
                MessageBox.Show("Убедитесь, что исходный файл существует и путь назначения корректен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }
    }
}
