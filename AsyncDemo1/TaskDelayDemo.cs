using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo1
{
    public class TaskDelayDemo
    {
        public void Run()
        {
            Console.WriteLine($"{nameof(Run)} - start.");
            try
            {
                DoAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                DoAsync2();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public async void DoAsync()
        {
            Console.WriteLine($"    {nameof(DoAsync)} - start.");
            await Task.Delay(1000);
            Console.WriteLine($"    {nameof(DoAsync)} - end.");
            //throw new Exception("throw exception in async void");
            

        }
        public async Task DoAsync2()
        {
            Console.WriteLine($"    {nameof(DoAsync)} - start.");
            await Task.Delay(1000);
            Console.WriteLine($"    {nameof(DoAsync)} - end.");
            throw new Exception("throw exception in async Task");
            
        }
    }
}
