using System.IO;
using static System.Console;
namespace DataProcessor
{
    internal class FileProcessor
    {
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
            string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
            WriteLine($"文件根目录为: {rootDirectoryPath}");
        }
    }
}