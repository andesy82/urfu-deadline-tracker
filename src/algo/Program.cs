using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UrFUDeadlineTracker
{
    public class LabTask
    {
        public string Title { get; set; } = "";
        public int MaxPoints { get; set; }
        public int HoursLeft { get; set; }
        public int Difficulty { get; set; }

        public double UrgencyIndex => Math.Round((double)(MaxPoints * Difficulty) / Math.Max(HoursLeft, 1), 2);
    }

    class Program
    {
        static void Main()
        {
            var tasks = new List<LabTask>
            {
                new LabTask { Title = "C# (ulearn): Практика «Циклы и массивы»", MaxPoints = 100, HoursLeft = 18, Difficulty = 4 },
                new LabTask { Title = "Алгоритмы: Рекурсивный перебор с отсечением", MaxPoints = 100, HoursLeft = 36, Difficulty = 5 },
                new LabTask { Title = "OpenEdu: Тест по истории России", MaxPoints = 30, HoursLeft = 12, Difficulty = 2 },
                new LabTask { Title = "Матанализ: Типовой расчет по пределам", MaxPoints = 50, HoursLeft = 72, Difficulty = 3 }
            };

            var sortedTasks = tasks.OrderByDescending(t => t.UrgencyIndex).ToList();

            Console.WriteLine("=== ТОП ПРИОРИТЕТНЫХ ДЕДЛАЙНОВ ИРИТ-РТФ ===\n");
            var csvLines = new List<string> { "Title;MaxPoints;HoursLeft;Difficulty;UrgencyIndex" };

            foreach (var task in sortedTasks)
            {
                Console.ForegroundColor = task.UrgencyIndex >= 15 ? ConsoleColor.Red :
                                          task.UrgencyIndex >= 8  ? ConsoleColor.Yellow : ConsoleColor.Green;

                Console.WriteLine($"[Индекс: {task.UrgencyIndex:F2}] {task.Title} | Баллы: {task.MaxPoints} | Осталось: {task.HoursLeft} ч.");
                csvLines.Add($"{task.Title};{task.MaxPoints};{task.HoursLeft};{task.Difficulty};{task.UrgencyIndex}");
            }

            Console.ResetColor();
            File.WriteAllLines("deadlines_report.csv", csvLines);
            Console.WriteLine("\n✅ Отчет успешно экспортирован в deadlines_report.csv");
        }
    }
}
