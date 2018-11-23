using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using qiyubrother.MemoryMappedFileImplement;

namespace MemoryMappedFileDemoReader
{
    class DemoReaderProgram
    {
        static void Main(string[] args)
        {
            var mmfi = new MemoryMappedFileImplement("fn", 1024*5);
            Console.WriteLine("正在监听内存映射文件，按任意键退出...");
            var task = new Task(() =>
            {
                while (true)
                {
                    var d = mmfi.LoadData();
                    if (d != null)
                    {
                        Console.WriteLine(d.Data);
                    }
                    Thread.Sleep(500);
                }
            });
            task.Start();
            Console.ReadKey();
        }
    }
}
