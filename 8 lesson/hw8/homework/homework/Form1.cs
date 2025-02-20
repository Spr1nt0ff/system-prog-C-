using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace homework
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cts;
        public Form1()
        {
            InitializeComponent();


            textBox1.Text = "Hello world! This my first project in c# on WF";
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();
            string path = string.Empty;
            string nameFile = string.Empty;
            bool allChecked = checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked;


            if (text.Length == 0)
            {
                MessageBox.Show("Not found text!");
                return;
            }

            if (!allChecked)
            {
                MessageBox.Show("You don't checked!");
                return;
            }

            if (isSaveResults.Checked)
            {
                path = showPath.Text.Trim();
                nameFile = inputNameFile.Text.Trim();

                if (path.Length == 0 || nameFile.Length == 0)
                {
                    MessageBox.Show("Your file saved!", "Report save",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            progressBar1.Value = 0;

            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            int totalSteps = 0;
            int completedSteps = 0;

            CheckBox[] checkBoxes = [checkBox1, checkBox2, checkBox3, checkBox4, checkBox5];
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (checkBox.Checked) totalSteps++;
            }

            var results = await Task.Run(() => {
                var resultList = new List<string>();

                if (checkBox1.Checked && !token.IsCancellationRequested)
                {
                    int count = CountSentences(text);
                    resultList.Add($"Sentences: {count}");
                    UpdateProgress(ref completedSteps, totalSteps);
                }

                if (checkBox2.Checked && !token.IsCancellationRequested)
                {
                    int count = CountCharacters(text);
                    resultList.Add($"Symbols: {count}");
                    UpdateProgress(ref completedSteps, totalSteps);
                }

                if (checkBox3.Checked && !token.IsCancellationRequested)
                {
                    int count = CountWords(text);
                    resultList.Add($"Words: {count}");
                    UpdateProgress(ref completedSteps, totalSteps);
                }

                if (checkBox4.Checked && !token.IsCancellationRequested)
                {
                    int count = CountQuestionSentences(text);
                    resultList.Add($"Question: {count}");
                    UpdateProgress(ref completedSteps, totalSteps);
                }

                if (checkBox5.Checked && !token.IsCancellationRequested)
                {
                    int count = CountExclamationSentences(text);
                    resultList.Add($"Exclamation: {count}");
                    UpdateProgress(ref completedSteps, totalSteps);
                }

                return resultList;
            }, token);
            string msg = string.Join("\n", results);

            MessageBox.Show(msg, "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (isSaveResults.Checked)
            {
                var selectedResults = new List<string>();
                string filteredResults = msg;

                if (radioButton1.Checked)
                {
                    if (checkBox1.Checked) selectedResults.Add(results.FirstOrDefault(r => r.StartsWith("Sentences")));
                    if (checkBox2.Checked) selectedResults.Add(results.FirstOrDefault(r => r.StartsWith("Symbols")));
                    if (checkBox3.Checked) selectedResults.Add(results.FirstOrDefault(r => r.StartsWith("Words")));
                    if (checkBox4.Checked) selectedResults.Add(results.FirstOrDefault(r => r.StartsWith("Question")));
                    if (checkBox5.Checked) selectedResults.Add(results.FirstOrDefault(r => r.StartsWith("Exclamation")));
                    filteredResults = string.Join("\n", selectedResults.Where(r => r != null));
                }

                SaveToFile(filteredResults, path, nameFile);
            }

            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }


        private void UpdateProgress(ref int completedSteps, int totalSteps)
        {
            completedSteps++;
            int progress = (completedSteps * 100) / totalSteps;
            Invoke(new Action(() => progressBar1.Value = progress));
        }

        private void SaveToFile(string text, string path, string nameFile)
        {
            string fullPath = Path.Combine(path, nameFile + ".txt");

            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine(text);
            }

            MessageBox.Show("File saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static int CountSentences(string text)
        {
            Thread.Sleep(1000);
            return Regex.Matches(text, @"[.!?](\s|$)").Count;
        }
        public static int CountCharacters(string text)
        {
            Thread.Sleep(1000);
            return text.Length;
        }
        public static int CountWords(string text)
        {
            Thread.Sleep(1000);
            return Regex.Matches(text, @"\b\w+\b").Count;
        }
        public static int CountQuestionSentences(string text)
        {
            Thread.Sleep(1000);
            return Regex.Matches(text, @"\?(\s|$)").Count;
        }
        public static int CountExclamationSentences(string text)
        {
            Thread.Sleep(1000);
            return Regex.Matches(text, @"!(\s|$)").Count;
        }

        private void isSaveResults_CheckedChanged(object sender, EventArgs e)
        {
            if (isSaveResults.Checked)
            {
                buttonPath.Enabled = true;
                inputNameFile.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else
            {
                buttonPath.Enabled = false;
                inputNameFile.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Saving file";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    showPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void CheckBoxes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                return;
            }

            bool allChecked = checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && checkBox4.Checked && checkBox5.Checked;

            checkBox6.CheckedChanged -= checkBox6_CheckedChanged;
            checkBox6.Checked = allChecked;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = checkBox5.Checked = true;
            }
            else
            {
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = checkBox5.Checked = false;
            }
        }

    }
}
