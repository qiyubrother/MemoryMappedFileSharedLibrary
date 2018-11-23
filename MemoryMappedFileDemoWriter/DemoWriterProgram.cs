using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using qiyubrother.MemoryMappedFileImplement;

namespace MemoryMappedFileDemoWriter
{
    class DemoWriterProgram
    {
        static void Main(string[] args)
        {
            var mmfi = new MemoryMappedFileImplement("fn", 1024 * 5);
            Console.WriteLine("正在写入内存映射文件，按任意键退出...");
            var task = new Task(() =>
            {
                while (true)
                {
                    var data = new MemoryMappedFileData();
                    data.Data = $"::{DateTime.Now.ToLongTimeString()}";
                    mmfi.WriteData(data);
                    Console.WriteLine(data.Data);
                    Thread.Sleep(800);
                }
            });
            task.Start();
            Console.ReadKey();
        }
    }
}
