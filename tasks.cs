//1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {

        List<int> numbers = Enumerable.Range(1, 100).ToList();

        try
        {
            long totalSum = CalculateSumOfSquares(numbers);
            Console.WriteLine($"Общая сумма квадратов: {totalSum}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static long CalculateSumOfSquares(List<int> numbers)
    {
        int chunkSize = numbers.Count / 4;
        Task<long>[] tasks = new Task<long>[4];

        for (int i = 0; i < 4; i++)
        {
            int start = i * chunkSize;
            int end = (i == 3) ? numbers.Count : start + chunkSize;

            tasks[i] = Task.Run(() => ProcessChunk(numbers, start, end));
        }

        Task.WaitAll(tasks);

        return tasks.Sum(t => t.Result);
    }

    static long ProcessChunk(List<int> numbers, int start, int end)
    {
        long sum = 0;

        for (int i = start; i < end; i++)
        {
            if (numbers[i] < 0)
            {
                Console.WriteLine($"Обнаружено отрицательное число: {numbers[i]}. Игнорируем.");
                continue;
            }

            sum += numbers[i] * numbers[i];
        }

        return sum;
    }
}
//2
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Начало загрузки...");
        var start = DateTime.Now;

        var task1 = DownloadAsync("Сервер1", 1000);
        var task2 = DownloadAsync("Сервер2", 2000);
        var task3 = DownloadAsync("Сервер3", 3000);

        await Task.Delay(2500);

        var results = new List<string>();

        if (task1.IsCompleted)
            results.Add(task1.Result);
        else
            Console.WriteLine("Сервер1: не успел");

        if (task2.IsCompleted)
            results.Add(task2.Result);
        else
            Console.WriteLine("Сервер2: не успел");

        if (task3.IsCompleted)
            results.Add(task3.Result);
        else
            Console.WriteLine("Сервер3: не успел");

        var time = (DateTime.Now - start).TotalSeconds;

        Console.WriteLine("\nУспешно загружено:");
        foreach (var r in results)
        {
            Console.WriteLine(r);
        }
        Console.WriteLine($"Время: {time:F2}с");
    }

    static async Task<string> DownloadAsync(string name, int delay)
    {
        await Task.Delay(delay);
        return $"{name}: данные загружены";
    }
}

//3
using System.Threading;
internal class Program
{
    static int count = 0;
    static object locker = new object();

    static void Main()
    {
        Console.WriteLine("Запуск 5 потоков, каждый делает 1000 инкрементов...");
        Thread[] threads = new Thread[5];

        for (int i = 0; i < 5; i++)
        {
            threads[i] = new Thread(Add);
            threads[i].Start();
        }


        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine($"Финальное значение: {count}");
        Console.WriteLine($"Ожидаемое значение: 5000");
        Console.WriteLine($"Результат верен: {count == 5000}");
    }

    static void Add()
    {
        for (int i = 0; i < 1000; i++)
        {
            lock (locker)
            {
                count++;
            }
        }
        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} завершил работу");
    }
}
//4
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        var Results = new List<double>();
        var numbers = new[] { 1, -4, 9 };
        var tasks = new List<Task>();

        foreach (var number in numbers)
        {
            var task = Task.Run(() => Sqrt(number))
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        Console.WriteLine($"Ошибка: {t.Exception.InnerException.Message}");
                    }
                    else if (t.IsCompletedSuccessfully)
                    {
                        Results.Add(t.Result);
                        Console.WriteLine($"Результат: {t.Result}");
                    }
                });

            tasks.Add(task);
        }
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine($"\nУспешные результаты: {string.Join(", ", Results)}");
    }

    static double Sqrt(double x)
    {
        if (x < 0) throw new ArgumentException($"Число {x} отрицательное");
        return Math.Sqrt(x);
    }
}

//5
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== ПАРАЛЛЕЛЬНЫЕ ЗАДАЧИ С ОТМЕНОЙ ===\n");
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

        var task1 = LongOperationAsync("Задача 1", cts.Token);
        var task2 = LongOperationAsync("Задача 2", cts.Token);

        try
        {
            await Task.WhenAll(task1, task2);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nВсе задачи были отменены через 3 секунды");
        }

        var result1 = await task1;
        var result2 = await task2;

        Console.WriteLine($"\n=== РЕЗУЛЬТАТЫ ===");
        Console.WriteLine($"Задача 1: завершено {result1} итераций");
        Console.WriteLine($"Задача 2: завершено {result2} итераций");
    }

    static async Task<int> LongOperationAsync(string taskName, CancellationToken cancellationToken)
    {
        int completedIterations = 0;

        Console.WriteLine($"{taskName}: запущена");

        try
        {
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(500, cancellationToken);

                completedIterations++;
                Console.WriteLine($"{taskName}: итерация {i} завершена");
            }

            Console.WriteLine($"{taskName}: все итерации завершены");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"{taskName}: отменена на итерации {completedIterations + 1}");
        }

        return completedIterations;
    }
}

