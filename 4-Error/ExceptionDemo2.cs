using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Error
{
    public class ExceptionDemo2
    {
        public async Task MainTask()
        {
            Task<string> task = ReadFileAysnc("2.txt");
            try
            {
                string text = await task;
                Console.WriteLine("File contents:{0}",text);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Caught IOException:{0}",ex.Message);
            }
        }
        /// <summary>
        /// 异步方法在调用时不会直接抛出异常
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<String> ReadFileAysnc(string filename)
        {
            using (var reader = File.OpenText(filename))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
