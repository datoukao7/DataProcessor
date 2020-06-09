using System;
using System.IO;
using static System.Console;
namespace DataProcessor
{
    internal class FileProcessor
    {
        private static readonly string BackupDirectoryName = "备份";
        private static readonly string InProgressDirectoryName = "处理";
        private static readonly string CompletedDirectoryName = "完成";
        //private string filePath;
        public string InputFilePath { get; }
        public FileProcessor(string filePath)
        {
            InputFilePath = filePath;
        }
        public void Process()
        {
            WriteLine($"开始处理 {InputFilePath}");
            //check if file exists
            if (!File.Exists(InputFilePath))
            {
                WriteLine($"错误：文件 {InputFilePath}不存在");
                return;
            }
            ///////////////////////////
            string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
            WriteLine($"文件根目录为: {rootDirectoryPath}");
            /////////////////////////////
            

            // Check if backup dir exists
            string inputFileDirectoryPath = Path.GetDirectoryName(InputFilePath);
            string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);//建立备份的路径
            if (!Directory.Exists(backupDirectoryPath)) //如果不存在就创建一个备份文件夹
            {
                WriteLine($"创建 {backupDirectoryPath}");
                Directory.CreateDirectory(backupDirectoryPath);
                
            }
            //Copy file to backup dir复制文件到备份文件夹
            string inputFileName = Path.GetFileName(InputFilePath);
            string backupFilePath = Path.Combine(backupDirectoryPath, inputFileName);
            WriteLine($"复制 {InputFilePath} 到 {backupFilePath}");
            File.Copy(InputFilePath, backupFilePath,true); //true 强制复制

            /////////////////////////////////////////
            //move file to process dir移动文件到处理文件夹
            Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName)); //创建处理文件夹
            string inProgressFilePath =
                Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);
            if (File.Exists(inProgressFilePath))
            {
                WriteLine($"错误：已经存在名为{inProgressFilePath}的文件");
                return;
            }
            File.Move(InputFilePath, inProgressFilePath);

            ////////////////////////////////////
            //Determine type of file判断文件类型
            string extension = Path.GetExtension(InputFilePath);
            switch (extension)
            {
                case ".txt":
                    ProcessTextFile(inProgressFilePath);
                    break;
                default:
                    WriteLine($"后缀名 {extension} 不支持");
                    break;
            }
            string completedDirectoryPath = Path.Combine(rootDirectoryPath, CompletedDirectoryName);
            Directory.CreateDirectory(completedDirectoryPath);
            WriteLine($"移动 {inProgressFilePath} 到 {completedDirectoryPath}");
            var completedFileName =
                $"{Path.GetFileNameWithoutExtension(InputFilePath)}-{Guid.NewGuid()}{extension}";
            //completedFileName = Path.ChangeExtension(completedFileName, ".complete");
            var completedFilePath = Path.Combine(completedDirectoryPath, completedFileName);

            File.Move(inProgressFilePath, completedFilePath);

            ////
            string inProcessDirectoryPath = Path.GetDirectoryName(inProgressFilePath);
            Directory.Delete(inProcessDirectoryPath, true);
        }

        private void ProcessTextFile(string inProgressFilePath)
        {
            WriteLine($"处理文本文件 {inProgressFilePath}");
            //throw new NotImplementedException();
        }



    }
}