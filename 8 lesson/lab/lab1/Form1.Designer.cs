namespace lab1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            progressBar5 = new ProgressBar();
            progressBar4 = new ProgressBar();
            progressBar3 = new ProgressBar();
            progressBar2 = new ProgressBar();
            progressBar1 = new ProgressBar();
            listView1 = new ListView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(114, 457);
            button1.Name = "button1";
            button1.Size = new Size(114, 41);
            button1.TabIndex = 11;
            button1.Text = "Click";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // progressBar5
            // 
            progressBar5.Location = new Point(24, 391);
            progressBar5.Name = "progressBar5";
            progressBar5.Size = new Size(328, 41);
            progressBar5.TabIndex = 10;
            // 
            // progressBar4
            // 
            progressBar4.Location = new Point(24, 310);
            progressBar4.Name = "progressBar4";
            progressBar4.Size = new Size(328, 41);
            progressBar4.TabIndex = 9;
            // 
            // progressBar3
            // 
            progressBar3.Location = new Point(24, 223);
            progressBar3.Name = "progressBar3";
            progressBar3.Size = new Size(328, 41);
            progressBar3.TabIndex = 8;
            // 
            // progressBar2
            // 
            progressBar2.Location = new Point(24, 137);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(328, 41);
            progressBar2.TabIndex = 7;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(24, 55);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(328, 41);
            progressBar1.TabIndex = 6;
            // 
            // listView1
            // 
            listView1.Location = new Point(407, 36);
            listView1.Name = "listView1";
            listView1.Size = new Size(381, 396);
            listView1.TabIndex = 12;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 26);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 13;
            label1.Text = "Лошадь 1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 110);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 14;
            label2.Text = "Лошадь 2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 195);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 15;
            label3.Text = "Лошадь 3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 283);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 16;
            label4.Text = "Лошадь 4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 364);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 17;
            label5.Text = "Лошадь 5";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 534);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(button1);
            Controls.Add(progressBar5);
            Controls.Add(progressBar4);
            Controls.Add(progressBar3);
            Controls.Add(progressBar2);
            Controls.Add(progressBar1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ProgressBar progressBar5;
        private ProgressBar progressBar4;
        private ProgressBar progressBar3;
        private ProgressBar progressBar2;
        private ProgressBar progressBar1;
        private ListView listView1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
