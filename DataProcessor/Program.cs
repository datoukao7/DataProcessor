using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console; //方便一点

namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("--file+路径 或 --dir");
            var command = args[0];
            if (command == "--file")
            {
                var filePath = args[1];
                WriteLine($"你选择了单个文件： {filePath} ");
                ProcessSingleFile(filePath);
            }
            else if (command == "--dir")
            {
                var directoryPath = args[1];
                var fileType = args[2];
                WriteLine($"Directory {directoryPath} selected for {fileType} files");
                ProcessDirectory(directoryPath, fileType);
            }
            else
            {
                WriteLine("无效的命令");
            }
            WriteLine("输入Enter退出");
            ReadLine();
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {
            //throw new NotImplementedException();

        }

        private static void ProcessSingleFile(string filePath)
        {
            //throw new NotImplementedException(); 这个自动生成的，注释掉
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }
    }
}
