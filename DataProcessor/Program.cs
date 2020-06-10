using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console; //方便一点
using System.IO;


namespace DataProcessor
{
    class Program
    {
        //public static int WatchError { get; private set; }

        static void Main(string[] args)
        {
            WriteLine("--file+路径 或 --dir");
            var directoryToWatch = args[0];
            if (!Directory.Exists(directoryToWatch))
            {
                WriteLine($"错误：{directoryToWatch} 不存在");
            }
            else
            {
                WriteLine($"监控文件夹{directoryToWatch}的变动");
                using (var inputFileWatcher =new FileSystemWatcher(directoryToWatch))
                {
                    inputFileWatcher.IncludeSubdirectories = false;
                    inputFileWatcher.InternalBufferSize = 32768;//32kb
                    inputFileWatcher.Filter = "*.*";
                    inputFileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite ;

                    inputFileWatcher.Created += FileCreated;
                    inputFileWatcher.Changed += FileChanged;
                    inputFileWatcher.Deleted += FileDeleted;
                    inputFileWatcher.Renamed += FileRenamed;
                    inputFileWatcher.Error += WatchError;
                    inputFileWatcher.EnableRaisingEvents = true;
                    WriteLine("按Enter退出");
                    ReadLine();

                }
            }
            //##################第一个项目#############################
            //var command = args[0];
            //if (command == "--file")
            //{
            //    var filePath = args[1];
            //    WriteLine($"你选择了单个文件： {filePath} ");
            //    ProcessSingleFile(filePath);
            //}
            //else if (command == "--dir")
            //{
            //    var directoryPath = args[1];
            //    var fileType = args[2];
            //    WriteLine($"Directory {directoryPath} selected for {fileType} files");
            //    ProcessDirectory(directoryPath, fileType);
            //}
            //else
            //{
            //    WriteLine("无效的命令");
            //}
            //WriteLine("输入Enter退出");
            //ReadLine();
            //#########################################

        }

        private static void WatchError(object sender, ErrorEventArgs e)
        {
            //throw new NotImplementedException();
            WriteLine($"Error: file system watching may on longer be active:{e.GetException()}");
        }

        private static void FileDeleted(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
            WriteLine($"文件{e.Name}被删除，类型为：{e.ChangeType}");
        }

        private static void FileRenamed(object sender, RenamedEventArgs e)
        {
            WriteLine($"文件{e.OldName}重命名为{e.Name}，类型为：{e.ChangeType}");
            //throw new NotImplementedException();
        }

        private static void FileChanged(object sender, FileSystemEventArgs e)
        {
            //throw new NotImplementedException();
            WriteLine($"文件{e.Name}改变，类型为：{e.ChangeType}");
        }

        private static void FileCreated(object sender, FileSystemEventArgs e)
        {
            WriteLine($"文件{e.Name}创建，类型为：{e.ChangeType}");
            //throw new NotImplementedException();
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {
            //throw new NotImplementedException();
            //var allFiles = Directory.GetFiles(directoryPath); // to get all files
            switch (fileType)
            {
                case "TEXT":
                    string[]  textFiles = Directory.GetFiles(directoryPath,"*.txt");
                    foreach (var textFilePath in textFiles)
                    {
                        var fileProcessor = new FileProcessor(textFilePath);
                        fileProcessor.Process();
                    }
                    break;
                default:
                    WriteLine($"错误 {fileType} is not supported");
                    return;
            }

        }

        private static void ProcessSingleFile(string filePath)
        {
            //throw new NotImplementedException(); 这个自动生成的，注释掉
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }
    }
}
