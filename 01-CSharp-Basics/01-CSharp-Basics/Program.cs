class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        bool isRunning = true;
        int[] scores = new int[0];

        while (isRunning)
        {
            Console.WriteLine("\n=== Статистика результатів тестування ===");
            Console.WriteLine("0. Вихід");
            Console.WriteLine("1. Додати результат");
            Console.WriteLine("2. Показати всі результати");
            Console.WriteLine("3. Показати середній бал");
            Console.WriteLine("4. Показати найвищий і найнижчий бал");
            Console.WriteLine("5. Показати розподіл за категоріями");

            int choice = ReadInt("\nВаш вибір: ", 0, 5);

            switch (choice)
            {
                case 0:
                    isRunning = false;
                    break;

                case 1:
                    int score = ReadInt(
                        "Введіть результат від 0 до 100: ",
                        0,
                        100);

                    Array.Resize(ref scores, scores.Length + 1);
                    scores[scores.Length - 1] = score;

                    Console.WriteLine("Результат успішно додано.");
                    break;

                case 2:
                    if (scores.Length == 0)
                    {
                        Console.WriteLine("Результатів поки немає.");
                    }
                    else
                    {
                        Console.WriteLine("Усі результати:");

                        for (int i = 0; i < scores.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {scores[i]}");
                        }
                    }
                    break;

                case 3:
                    if (scores.Length == 0)
                    {
                        Console.WriteLine("Результатів поки немає.");
                    }
                    else
                    {
                        double average = CalculateAverage(scores);

                        Console.WriteLine(
                            $"Середній бал: {average:F2}");
                    }
                    break;

                case 4:
                    if (scores.Length == 0)
                    {
                        Console.WriteLine("Результатів поки немає.");
                    }
                    else
                    {
                        FindMinMax(
                            scores,
                            out int min,
                            out int minIndex,
                            out int max,
                            out int maxIndex);

                        Console.WriteLine(
                            $"Найнижчий бал: {min}, позиція: {minIndex + 1}");

                        Console.WriteLine(
                            $"Найвищий бал: {max}, позиція: {maxIndex + 1}");
                    }
                    break;

                case 5:
                    if (scores.Length == 0)
                    {
                        Console.WriteLine("Результатів поки немає.");
                    }
                    else
                    {
                        ShowCategories(scores);
                    }
                    break;
            }
        }

        Console.WriteLine("Роботу завершено.");
    }

    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) &&
                value >= min &&
                value <= max)
            {
                return value;
            }

            Console.WriteLine(
                $"Некоректне значення. Введіть ціле число від {min} до {max}.");
        }
    }

    static double CalculateAverage(int[] scores)
    {
        int sum = 0;

        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }

        return (double)sum / scores.Length;
    }

    static void FindMinMax(
        int[] scores,
        out int min,
        out int minIndex,
        out int max,
        out int maxIndex)
    {
        min = scores[0];
        max = scores[0];
        minIndex = 0;
        maxIndex = 0;

        for (int i = 1; i < scores.Length; i++)
        {
            if (scores[i] < min)
            {
                min = scores[i];
                minIndex = i;
            }

            if (scores[i] > max)
            {
                max = scores[i];
                maxIndex = i;
            }
        }
    }

    static void ShowCategories(int[] scores)
    {
        int excellent = 0;
        int good = 0;
        int satisfactory = 0;
        int unsatisfactory = 0;

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] >= 90)
            {
                excellent++;
            }
            else if (scores[i] >= 75)
            {
                good++;
            }
            else if (scores[i] >= 60)
            {
                satisfactory++;
            }
            else
            {
                unsatisfactory++;
            }
        }

        Console.WriteLine("Розподіл за категоріями:");
        Console.WriteLine($"Відмінно (90-100): {excellent}");
        Console.WriteLine($"Добре (75-89): {good}");
        Console.WriteLine($"Задовільно (60-74): {satisfactory}");
        Console.WriteLine($"Незадовільно (0-59): {unsatisfactory}");
    }
}