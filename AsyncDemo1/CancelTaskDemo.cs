using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    public class CancelTaskDemo
    {
        public void Run()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Task t = ExcuteAsync(token);

            //挂起3秒，因为异步方法是采用 新线程运行的（Task.Run），所以不受影响
            Thread.Sleep(3000);
            source.Cancel();

            t.Wait(token);
            Console.WriteLine($"{nameof(token.IsCancellationRequested)}: {token.IsCancellationRequested}");

            Console.ReadLine();

        }
        public async Task ExcuteAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            await Task.Run(()=> CircleOutput(token),token);

        }
        public  void CircleOutput(CancellationToken token)
        {
            Console.WriteLine($"{nameof(CircleOutput)} 方法开始调用：");
            if (token.IsCancellationRequested)
                return;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i+1}/{5} 完成");
                Thread.Sleep(1000);
            }
        }
    }
}
