#!/usr/bin/env dotnet


class Class1
{
    static object locker = new object();

    static async Task<int> printC(ConsoleColor color, int start, int end)
    {
        int numOfIterations = 0;

        for (int i = start; i < end; i++)
        {
            lock (locker)
            {
                Console.ForegroundColor = color;
                Console.Write(i + " ");
                Console.ResetColor();
            }

            numOfIterations++;

            await Task.Delay(10);
        }

        return numOfIterations;
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("Printing numbers in different colors...");

        Task<int> task1 = printC(ConsoleColor.Red, 1, 101);
        Task<int> task2 = printC(ConsoleColor.Green, 1, 101);

        int[] results = await Task.WhenAll(task1, task2);

        Console.WriteLine("\nAll tasks completed.");

        Console.WriteLine($"Task 1 iterations: {results[0]}");
        Console.WriteLine($"Task 2 iterations: {results[1]}");
    }
}


class Program2
{
    static object locker = new object();

    static void PrintCo(ConsoleColor color, int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            lock (locker)
            {
                Console.ForegroundColor = color;
                Console.Write(i + " ");
                Console.ResetColor();
            }

            Thread.Sleep(50);
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Printing numbers in different colors...");

        Thread t1 = new Thread(() => PrintCo(ConsoleColor.Red, 1, 100));
        Thread t2 = new Thread(() => PrintCo(ConsoleColor.Green, 1, 100));

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("All threads completed.");
    }
}



