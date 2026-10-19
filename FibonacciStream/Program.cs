using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Числа Фибоначчи с увеличивающейся задержкой:");

        try
        {
            await foreach (int number in GenerateFibonacciAsync(10))
            {
                Console.WriteLine(number);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при генерации чисел: " + ex.Message);
        }
    }

    static async IAsyncEnumerable<int> GenerateFibonacciAsync(int count)
    {
        int first = 0;
        int second = 1;

        for (int i = 0; i < count; i++)
        {
            // Каждое следующее число появляется позже предыдущего.
            await Task.Delay(100 * (i + 1));

            yield return first;

            int next = first + second;
            first = second;
            second = next;
        }
    }
}
