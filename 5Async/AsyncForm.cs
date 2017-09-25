using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5Async
{
    public partial class AsyncForm : Form
    {
        Label label;
        Button button;
        Button button2;
        private CancellationTokenSource cts;
        public AsyncForm()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
            label = new Label { Location = new Point(10,20), Text="Length" };
            button = new Button { Location = new Point(10,50),Text="Click" };
            button2 = new Button { Location = new Point(10, 80), Text = "Cancle" };

            button.Click += DisplayWebSiteLength;
            button2.Click += Button2_Click;
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            AutoSize = true;

            Controls.Add(label);
            Controls.Add(button);
            Controls.Add(button2);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (cts != null)
                cts.Cancel();
        }       
        /// <summary>
        /// 直接使用async await
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void DisplayWebSiteLength2(object sender, EventArgs args)
        {
            this.label.Text = "Fetch......";
            using (HttpClient client = new HttpClient())
            {
                string text = await client.GetStringAsync("http://www.sina.com");
                label.Text = text.Length.ToString();
            }
        }


        /// <summary>
        /// 使用Task并传送取消token
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void DisplayWebSiteLength(object sender, EventArgs args)
        {
            try
            {
                label.Text = "Fetch......";
                using (HttpClient client = new HttpClient())
                {
                    Task<string> task = Task<string>.Run(() =>
                    {

                        return client.GetStringAsync("https://social.msdn.microsoft.com/Forums/vstudio/en-US/d3dcf07b-b2ed-4c07-9072-3d6fc018c25a/netnamedpipebinding-the-pipe-could-not-close-gracefully?forum=wcf").Result;
                    }, cts.Token);

                    //禁止切换到同步上下文，如果不操作UI的话，禁止切换上下文可以提高执行速度
                    //await task.ConfigureAwait(false);
                    string text = await task;
                    //cts.Token.ThrowIfCancellationRequested();
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    label.Text = text.Length.ToString();
                }
            }
            catch (OperationCanceledException) { }
        }
    }
}
