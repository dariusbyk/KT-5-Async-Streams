using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "numbers.txt");

        Console.WriteLine("Суммы чисел в строках файла:");

        try
        {
            await foreach (string result in ReadSumsAsync(filePath))
            {
                Console.WriteLine(result);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("Ошибка чтения файла: " + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Нет доступа к файлу: " + ex.Message);
        }
    }

    static async IAsyncEnumerable<string> ReadSumsAsync(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            int lineNumber = 0;

            while (true)
            {
                string? line = await reader.ReadLineAsync();

                if (line == null)
                {
                    break;
                }

                lineNumber++;
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                long sum = 0;
                bool correct = true;

                for (int i = 0; i < parts.Length; i++)
                {
                    int number;

                    if (int.TryParse(parts[i], out number))
                    {
                        sum = sum + number;
                    }
                    else
                    {
                        correct = false;
                        break;
                    }
                }

                if (correct)
                {
                    yield return "Строка " + lineNumber + ": сумма = " + sum;
                }
                else
                {
                    yield return "Строка " + lineNumber + ": ошибка разбора чисел";
                }
            }
        }
    }
}
