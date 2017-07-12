using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6Yeild
{
    class FileReadLineDemo
    {
        static IEnumerable<string> ReadLines(Func<TextReader> provider)
        {
            using (TextReader reader = provider())
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                         
                }
            }            
        }
        static IEnumerable<string> ReadLines(string fileName,Encoding encoding)
        {
            Func<TextReader> delegatefun = delegate { return File.OpenText(fileName); };
            return  ReadLines(delegatefun);
        }

       public static void Demo()
        {

            foreach (string str in ReadLines("ReadMe.txt", Encoding.UTF8))
                Console.WriteLine(str);
        }
    }
}
