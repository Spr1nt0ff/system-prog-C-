using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace CSharpApplication.Threads
{
    class ProgressBarDance : Form
    {
        // ���������� �������
        const int ThreadsCount = 3;

        // ����������
        ProgressBar[] pr = new ProgressBar[ThreadsCount];
        // ������ ��� �������/��������� �������
        CheckBox[] bRun = new CheckBox[ThreadsCount];
        // ������ ��� ������������/������������� �������
        CheckBox[] bSuspend = new CheckBox[ThreadsCount];
        // ������ ��� �������� ��������� �������
        bool[] ThreadsRun = new bool[ThreadsCount];
        // ������ ��������� (��������� �� ������� - ����� ����� � �����)
        ThreadStart[] threadsstart;
        // ������ �������
        Thread[] threads;

        static void Main()
        {
            // ������ ����������
            Application.Run(new ProgressBarDance());
        }

        ProgressBarDance()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.ClientSize = new Size(350, 310);

            //Icon ico =  Icon.ExtractAssociatedIcon(@"C:\WINDOWS\system32\notepad.exe");
            //this.Icon = ico;

            this.Icon = new Icon(GetType(), "Ico.ico");
            this.Text = "������ � ��������";

            // ������������� ��������� ����������
            for (int i = 0; i < ThreadsCount; i++)
            {
                pr[i] = new ProgressBar();
                pr[i].Parent = this;
                pr[i].Location = new Point(10, 10 + i * 100);
                pr[i].ClientSize = new Size(this.ClientRectangle.Width - 20, 50);
                // ����������� �������� ����������
                pr[i].Minimum = 0;
                // ������������ �������� ����������
                pr[i].Maximum = 100;

                bRun[i] = new CheckBox();
                bRun[i].Parent = this;
                bRun[i].Location = new Point(10, 10 + i * 100 + 65);
                bRun[i].ClientSize = new Size(150, 20);
                bRun[i].Text = string.Format("��������� {0}-� �����", i + 1);

                bSuspend[i] = new CheckBox();
                bSuspend[i].Parent = this;
                bSuspend[i].Location = new Point(170, 10 + i * 100 + 65);
                bSuspend[i].ClientSize = new Size(200, 20);
                bSuspend[i].Text = string.Format("������������� {0}-� �����", i + 1);
                bSuspend[i].Enabled = false;

                // ���������� ������� �� CheckBox'��
                bRun[i].Click += new EventHandler(OnCheckBoxClick);
                bSuspend[i].Click += new EventHandler(OnCheckBoxClick);
            }
        }

        // public delegate void ThreadStart(); - ��� ��������� �������.
        // ��������� �������
        private void WorkThread()
        {
            Random r = new Random();


            // public static Thread CurrentThread {get;} - ��������, ���������� ������ �������� ������. 
            // �������� ������ ������, ������� ������� � ��� �����
            int index = Convert.ToInt32(Thread.CurrentThread.Name);

            // ���� ���� ��������� ������ ���������
            while (ThreadsRun[index])
            {
                // �������� ��������� �������� ��� ������� ����������
                pr[index].Value = r.Next(100);
                // �������� ����� �� ��������� �������� �������
                Thread.Sleep(r.Next(50, 500));
            }
        }

        // ���������� ������ �� CheckBox'��
        private void OnCheckBoxClick(object sender, EventArgs e)
        {
            // ���� ������� �������
            if (threadsstart == null)
            {
                // public delegate void ThreadStart(); - ��� ��������� �������.
                // �������������� ������ ���������
                threadsstart = new ThreadStart[]
                    {
                        new ThreadStart(WorkThread),
                        new ThreadStart(WorkThread),
                        new ThreadStart(WorkThread)
                    };

                threads = new Thread[ThreadsCount];

                // �������������� ������
                for (int i = 0; i < ThreadsCount; i++)
                {
                    // public Thread(ThreadStart start); - ����������� ������, 
                    // �������� � �������� ��������� �������, ������� ��������� �� ������� - ����� ����� � ����� 

                    // ������� �����
                    threads[i] = new Thread(threadsstart[i]);
                    // ���� ��� ��� (������ �������)
                    threads[i].Name = string.Format("{0}", i);
                    // ���������, ��� ����� �� �������
                    ThreadsRun[i] = false;
                }
            }

            // ���������� �������� ������� �� ����� (CheckBox)
            CheckBox check = (CheckBox)this.ActiveControl; // CheckBox check = (CheckBox)sender;

            // ���� ��� ������ � ������� ������� ������� �������
            int index = Array.IndexOf(bRun, check);

            // ����� CheckBox �� ����� �������
            if (index != -1)
            {
                // ���� ������� �����������
                if (check.Checked)
                {
                    // ���������, ��� ����� �������
                    ThreadsRun[index] = true;
                    bRun[index].Text = string.Format("���������� {0}-� �����", index + 1);
                    bSuspend[index].Enabled = true;
                    // ������ ������
                    threads[index].Start();
                }
                else // ���� ������� �����
                {
                    // ���������, ��� ����� ����������� ���� ������
                    ThreadsRun[index] = false;

                    // ���� ����� ��������� � ���������������� ���������
                    if (bSuspend[index].Checked)
                    {
                        // public void Resume(); - ������������ ���������������� ����� 
                        threads[index].Resume();

                        //public void Abort(); - �������� ���������� ���� ThreadAbortException � ������������� �����. 
                        // ������ ���������� ����� ����������� � ������ �, � ������� ������� public static void ResetAbort(); �������� ���������� ������. 
                        threads[index].Abort();
                        bSuspend[index].Checked = false;
                    }
                    else
                    {
                        // ����� �������� ����� �������������, �� ��� ���� ���
                        // ������������� ������ ����� �������
                        // threads[index].Abort();

                        // public void Join();
                        // public bool Join(int);
                        // public bool Join(TimeSpan);
                        // ��������� ���������� ����� (�� ������������ ��� �������������� ���������� �������), 
                        // ���� �����, ��� �������� ������� ����������, �� ����������.

                        // ������� ���������� ������
                        threads[index].Join();
                    }

                    // ����� �������������� ������ ����� 
                    threads[index] = new Thread(threadsstart[index]);
                    threads[index].Name = string.Format("{0}", index);
                    pr[index].Value = 0;
                    bRun[index].Text = string.Format("��������� {0}-� �����", index + 1);
                    bSuspend[index].Text = string.Format("������������� {0}-� �����", index + 1);
                    bSuspend[index].Enabled = false;
                }
            }
            else
            {
                // ���� ������ � ������� ������� ������������ �������
                int sindex = Array.IndexOf(bSuspend, check);

                // ���� ������� �����������
                if (check.Checked)
                {
                    // public void Suspend(); - ���������������� ����� 
                    threads[sindex].Suspend();
                    bSuspend[sindex].Text = string.Format("����������� {0}-� �����", sindex + 1);
                }
                else
                {
                    // public void Resume(); - ������������ ���������������� ����� 
                    threads[sindex].Resume();
                    bSuspend[sindex].Text = string.Format("������������� {0}-� �����", sindex + 1);
                }
            }
        }


        /*
        ����� �������� ���� �������, ���� �������� (�� ���������).
        ��� ���������� �� �������� ��������. 
        ���� �������� ������� ����� � ���������� �������� ����������, ����� ����� ����������� �� ����������� ������. 
        �������� �� ����� ��� �������� ���������� ��������� � ����������� ������.
        ���� ��� �������� ������, ������������� ��������, �����������, ������������ ����� ���������� ��������� �������, 
        ������� ����� Abort ��� ���� ������� �������, ������� ��� ���������.
        */

        // ���������� �������� �����
        protected override void OnClosed(EventArgs e)
        {
            // ���������� ������ �������
            for (int i = 0; i < ThreadsCount; i++)
            {
                /*******************************************************/
                /* ������ ������ ��������� ���������� �������
                /*******************************************************/
                // ���� ����� �������
                if (ThreadsRun[i])
                {
                    // ���������� ���� ��������� ������ (��������� ������)
                    ThreadsRun[i] = false;
                    // ���� ����� ��� �������������
                    if (bSuspend[i].Checked)
                        // ������������ ������ ������
                        threads[i].Resume();
                }

                /******************************************************/
                /* ������ ������ ��������� ���������� �������
                /******************************************************/
                /*
                if(bSuspend[i].Checked == true)
                     ������������ ������ ������
                    threads[i].Resume();
                 "������" ��������� ������
                threads[i].Abort();
                */

                /******************************************************/
                /* ������ ������ ��������� ���������� �������
                /******************************************************/

                // ������� ������� � ������� �����
                // threads[i].IsBackground = true;
            }
            base.OnClosed(e);
        }
    }
}