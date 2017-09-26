using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDemo2
{
    public partial class Form1 : Form
    {
        private Thread demoThread;
        public Form1()
        {
            InitializeComponent();
            //匿名异步函数
            button1.Click += async (sender, e) =>
             {
                 this.label1.Text = "Doing";
               
                 await Task.Yield();
                 await Task.Delay(4000);
                 label1.Text = "Complete";
             };

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            //this.label1.Text = "Doing";
            ////通知application处理当前消息队列中的消息
            ////Application.DoEvents();
            ////Thread.Sleep(3000);
            //await Task.Delay(3000);
            //label1.Text = "Complete";
        }

        /// <summary>
        /// 参照重油项目 在新线程中使用委托执行UI控件的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            demoThread = new Thread(new ThreadStart(ThreadProcSafe));
            demoThread.Start();
        }
        private void ThreadProcSafe()
        {
            SetText("Doing");
            Thread.Sleep(3000);
            SetText("Complete");
        }
        private void SetText(string text)
        {
            if (this.label1.InvokeRequired)
            {
                while (this.label1.IsHandleCreated)
                {
                    if (this.label1.IsDisposed || this.label1.Disposing)
                        return;
                }
                Action<string> action = SetText;
                this.label1.Invoke(action, new object[] { text });
            }
            else
            {
                this.label1.Text = text;
            }
        }
    }
}
