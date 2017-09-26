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
    /// <summary>
    /// 通过异步方法实现进度条控件
    /// </summary>
    public partial class Form2 : Form
    {
        private CancellationTokenSource _source;
        private CancellationToken _token;

        public Form2()
        {
            InitializeComponent();
            InitialControl();

           

        }
        private void InitialControl()
        {
            this.progressBar1.Value = 0;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            _source = new CancellationTokenSource();
            _token = _source.Token;

            const int time = 10;
            const int timePercent = 100 / time;
            int completedPercent = 0; //完成百分比

            for (int i = 0; i < time; i++)
            {
                if (_token.IsCancellationRequested)
                    break;
                try
                {
                    await Task.Delay(500, _token);

                    completedPercent = (i + 1) * timePercent;
                }
                catch (Exception ex)
                {
                    completedPercent = i * timePercent;
                }
                finally
                {
                    this.progressBar1.Value = completedPercent;
                }
            }
            String message = _token.IsCancellationRequested ? $"进度为{completedPercent}% 已经被取消" : "进度完成";
            MessageBox.Show(message);

            InitialControl();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == true)
                return;

            button2.Enabled = false;

            _source.Cancel();
        }
    }
}
