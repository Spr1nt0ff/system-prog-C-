using System;
using System.Diagnostics;
using System.Threading;


class ReactionTest
{
    static Stopwatch stopwatch = new Stopwatch();

    static void Main()
    {
        Console.WriteLine("Приготовьтесь...");
        Thread.Sleep(3000);

        Console.WriteLine("НАЖМИТЕ КНОПКУ! (любую)");
        stopwatch.Start();

        Console.ReadKey(true);
        stopwatch.Stop();

        Console.WriteLine($"Время реакции: {stopwatch.ElapsedMilliseconds} мс");
    }
}