using System;
using System.IO;
using System.Threading;

class Program
{
    static void SearchWordInFile(string filePath, string word, List<string> results, object lockObj)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (line.Contains(word))
                {
                    lock (lockObj)
                    {
                        results.Add($"Файл: {filePath}, слово: {word}");
                    }
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            lock (lockObj)
            {
                results.Add($"Ошибка при чтении файла {filePath}: {ex.Message}");
            }
        }
    }

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Использование: Program.exe <файл1> <файл2> ... <слово1> <слово2>");
            return;
        }

        var filePaths = new List<string>();
        var searchWords = new List<string>();

        foreach (var arg in args)
        {
            if (File.Exists(arg))
            {
                filePaths.Add(arg);
            }
            else
            {
                searchWords.Add(arg);
            }
        }

        var threads = new List<Thread>();
        var results = new List<string>();
        object lockObj = new object();

        foreach (var file in filePaths)
        {
            foreach (var word in searchWords)
            {
                string currentFile = file;
                string currentWord = word;
                Thread t = new Thread(() => SearchWordInFile(currentFile, currentWord, results, lockObj));
                threads.Add(t);
                t.Start();
            }
        }

        foreach (var t in threads)
        {
            t.Join();
        }

        Console.WriteLine("Найденные слова:");
        foreach (var res in results)
        {
            Console.WriteLine(res);
        }
    }
}
