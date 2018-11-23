using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace qiyubrother.MemoryMappedFileImplement
{
    public class MemoryMappedFileImplement
    {
        MemoryMappedFile memoryMappedFile;
        MemoryMappedViewStream stream;

        private string _fileName = "fn";
        private long _fileSize = 1024 * 5;
        public MemoryMappedFileImplement(string fileName, long fileSize)
        {
            _fileName = fileName;
            _fileSize = fileSize;
            // 创建内存映射文件
            memoryMappedFile = MemoryMappedFile.CreateOrOpen(_fileName, _fileSize); // 10MB
            // 获取其读写流
            stream = memoryMappedFile.CreateViewStream();
        }

        public string FileName { get => _fileName; }
        public long FileSize { get => _fileSize; }
        /// <summary>
        /// 释放相关资源
        /// </summary>
        private void DisposeMemoryMappedFile()
        {
            if (stream != null)
            {
                stream.Close();
            }
            if (memoryMappedFile != null)
            {
                memoryMappedFile.Dispose();
            }
        }

        public MemoryMappedFileData LoadData()
        {
            IFormatter formatter = new BinaryFormatter();

            stream.Seek(0, SeekOrigin.Begin);
            var flg = stream.ReadByte();
            // 没有存入数据，无需理会
            if (flg == 0)
            {
                return null;
            }
            else
            {
                MemoryMappedFileData md = formatter.Deserialize(stream) as MemoryMappedFileData;
                Console.WriteLine(md.Data);
                stream.Seek(0, SeekOrigin.Begin);
                stream.WriteByte(0);
                return md;
            }
        }

        public void WriteData(MemoryMappedFileData memoryMappedFileData)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(1, SeekOrigin.Begin);
            formatter.Serialize(stream, memoryMappedFileData);
            stream.Seek(0, SeekOrigin.Begin);
            stream.WriteByte(2);
            stream.Flush();
        }
    }

    [Serializable]
    public class MemoryMappedFileData
    {
        public object Data;
    }
}
