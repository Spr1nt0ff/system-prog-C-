using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework
{
    public partial class Form1 : Form
    {
        private bool _stopSearch = false;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            string directory = @"D:\";
            string searchPattern = textBox1.Text;

            button1.Enabled = false;
            button2.Enabled = true;
            listView1.Items.Clear();

            await SearchFilesAndDirectoriesAsync(directory, searchPattern);

            button1.Enabled = true;
            button2.Enabled = false;
        }

        private async Task SearchFilesAndDirectoriesAsync(string directory, string searchPattern)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (_stopSearch) return;

                    string[] files;

                    files = Directory.GetFiles(directory, searchPattern);


                    foreach (var file in files)
                    {
                        if (_stopSearch) return;

                        FileInfo fileInfo = new FileInfo(file);
                        Invoke((Action)(() =>
                        {
                            AddItemViewList(fileInfo.Name, fileInfo.DirectoryName, fileInfo.Length.ToString(), fileInfo.LastWriteTime.ToString());
                        }));
                    }

                }
                catch (Exception ex)
                {
                    Invoke((Action)(() =>
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            });
        }

        private void AddItemViewList(string name, string folder, string size, string lastWriteTime)
        {
            ListViewItem item = new ListViewItem(name);
            item.SubItems.Add(folder);
            item.SubItems.Add(size);
            item.SubItems.Add(lastWriteTime);

            listView1.Items.Add(item);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            _stopSearch = true;
        }
    }
}
