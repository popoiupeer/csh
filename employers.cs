// Задание 1
using System;
using System.Collections.Generic;

class Program
{
    delegate List<int> Filter(List<int> list);

    static void Main()
    {
        List<int> GetEvenNumbers(List<int> list)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] % 2 == 0)
                {
                    result.Add(list[i]);
                }
            }
            return result;
        }

        List<int> GetPositiveNumbers(List<int> list)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > 0)
                {
                    result.Add(list[i]);
                }
            }
            return result;
        }

        Filter filter = GetEvenNumbers;
        
        List<int> numbers = new List<int> { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5 };
        
        Console.WriteLine("Четные числа:");
        var evenNumbers = filter(numbers);
        foreach (var num in evenNumbers)
        {
            Console.WriteLine(num);
        }

        filter = GetPositiveNumbers;
        
        Console.WriteLine("\nПоложительные числа:");
        var positiveNumbers = filter(numbers);
        foreach (var num in positiveNumbers)
        {
            Console.WriteLine(num);
        }
    }
}
//Задание 2
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Func<List<int>, int, List<int>> addNumberToList = (list, number) =>
        {
            return list.Select(x => x + number).ToList();
        };

        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        int numberToAdd = 10;
        
        var result = addNumberToList(numbers, numberToAdd);
        
        Console.WriteLine("Оригинал: " + string.Join(", ", numbers));
        Console.WriteLine("Получившийся список " + numberToAdd + ": " + string.Join(", ", result));
    }
}
//Задание 3
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var employees = new[]
        {
            new { Name = "Ivan", Department = "IT", Salary = 100000 },
            new { Name = "Petr", Department = "HR", Salary = 80000 },
            new { Name = "Alexey", Department = "IT", Salary = 120000 },
            new { Name = "Mary", Department = "HR", Salary = 75000 },
            new { Name = "Darya", Department = "IT", Salary = 95000 },
            new { Name = "Sergey", Department = "Finance", Salary = 110000 }
        };
        
        Console.WriteLine(" ТОП-3 самых высокооплачиваемых сотрудников:");
        var topEmployees = employees
            .OrderByDescending(e => e.Salary)
            .Take(3);
        
        foreach (var emp in topEmployees)
        {
            Console.WriteLine($"{emp.Name} - {emp.Salary} rub.");
        }
        Console.WriteLine();
        
        var AvgSalary = employees
            .GroupBy(e => e.Department)
            .Select(g => new {
                Department = g.Key,
                AverageSalary = g.Average(e => e.Salary)
            })
            .OrderByDescending(x => x.AverageSalary)
            .First();

        Console.WriteLine($"Отдел с самой высокой средней зарплатой: {AvgSalary.Department}");
        Console.WriteLine($"Средняя заработная плата: {AvgSalary.AverageSalary} rub.");
        
        
        double avgSalary = employees.Average(e => e.Salary);

        var result = employees
            .Where(e => e.Salary > avgSalary)
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                Employees = g.OrderByDescending(e => e.Name).Select(e => $"{e.Name} ({e.Salary} rub.)")
            });

        foreach (var group in result)
        {
            Console.WriteLine($"Department: {group.Department}");
            foreach (var emp in group.Employees)
            {
                Console.WriteLine($"- {emp}");
            }
            Console.WriteLine();
        }
    }
}
