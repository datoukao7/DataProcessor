﻿using static System.Console;
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
            WriteLine($"Begin Process of {InputFilePath}");
        }
    }
}