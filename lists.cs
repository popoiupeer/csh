using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    private static List<int> list = new List<int>() { 4, 21, 6, 3, 1, 5, 1 };
    private static bool listChanged = false;
    

    static void Main()
    {
        Thread sortThread = new Thread(Sort);
        Thread observeThread = new Thread(Observe);

        sortThread.Start();
        observeThread.Start();

        Thread.Sleep(5000); 
        observeThread.Join(); 
    }

    static void Observe()
    {
        while (true)
        {
            if (listChanged)
            {
                Console.WriteLine("Измененный список: " + string.Join(", ", list));
                listChanged = false;
            }
            Thread.Sleep(50); 
        }
    }

    static void Sort()
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - 1 - i; j++)
            {
                if (list[j] > list[j + 1])
                {
                    int temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                    listChanged = true;
                }
                Thread.Sleep(100);
            }
        }
    }
}
