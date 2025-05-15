using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string file1Path = "C:\\Users\\erdan\\Desktop\\task 1\\file1.txt";
        string file2Path = "C:\\Users\\erdan\\Desktop\\task 1\\file2.txt";
        string mergedFilePath = "C:\\Users\\erdan\\Desktop\\task 1\\merged.txt";

        if (File.Exists(file1Path))
        {
            string[] linesFile1 = File.ReadAllLines(file1Path);
            File.WriteAllLines(mergedFilePath, linesFile1);
        }
        else
        {
            Console.WriteLine($"Файл {file1Path} не найден.");
        }

        if (File.Exists(file2Path))
        {
            string[] linesFile2 = File.ReadAllLines(file2Path);
            File.AppendAllLines(mergedFilePath, linesFile2);
        }
        else
        {
            Console.WriteLine($"Файл {file2Path} не найден.");
        }

        Console.WriteLine("Файлы успешно объединены в " + mergedFilePath);
    }
}
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFilePath = "C:\\Users\\erdan\\Desktop\\task 2\\input.txt";
        string outputFilePath = "C:\\Users\\erdan\\Desktop\\task 2\\result.txt";

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Ошибка: файл {inputFilePath} не найден.");
            return;
        }

        
        string content = File.ReadAllText(inputFilePath);
        string[] words = content.Split(' ', '\n', '\r');

        int wordCount = 0;
        foreach (var word in words)
        {
            if (word.Length > 0) 
            {
                wordCount++;
            }
        }

        File.WriteAllText(outputFilePath, $"Количество слов: {wordCount}");

        Console.WriteLine($"Количество слов: {wordCount}");
        Console.WriteLine($"Результат сохранен в {outputFilePath}");
    }
}
