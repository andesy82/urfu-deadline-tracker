
using System.Globalization;
using System.Text;

namespace UrfuDeadlineTracker;

public class LabTask
{
    public string Title { get; set; }
    public string Subject { get; set; }
    public double MaxPoints { get; set; }
    public int Difficulty { get; set; }
    public double DaysLeft { get; set; }

    public double PriorityScore =>
        (MaxPoints * Difficulty) / Math.Max(DaysLeft, 0.5);

    public LabTask(
        string title,
        string subject,
        double maxPoints,
        int difficulty,
        double daysLeft)
    {
        Title = title;
        Subject = subject;
        MaxPoints = maxPoints;
        Difficulty = difficulty;
        DaysLeft = daysLeft;
    }
}

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var tasks = new List<LabTask>
        {
            new(
                "Коллекции и LINQ",
                "C#",
                100,
                4,
                2
            ),
            new(
                "Сортировки и графы",
                "Алгоритмы",
                90,
                5,
                1
            ),
            new(
                "Интегралы и ряды",
                "Математический анализ",
                80,
                4,
                5
            ),
            new(
                "Объектно-ориентированное программирование",
                "C#",
                100,
                3,
                7
            ),
            new(
                "Дифференциальные уравнения",
                "Высшая математика",
                70,
                5,
                3
            )
        };

        var sortedTasks = tasks
            .OrderByDescending(task => task.PriorityScore)
            .ToList();

        PrintHeader();
        PrintTasks(sortedTasks);
        SaveReport(sortedTasks);

        Console.WriteLine();
        Console.WriteLine("Отчёт успешно сохранён в report.csv");
        Console.WriteLine($"Всего лабораторных работ: {tasks.Count}");
        Console.WriteLine(
            $"Самая приоритетная работа: {sortedTasks[0].Title}"
        );
    }

    private static void PrintHeader()
    {
        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("          UrFU DEADLINE TRACKER");
        Console.WriteLine("       Система контроля дедлайнов");
        Console.WriteLine("==============================================");
        Console.WriteLine();
    }

    private static void PrintTasks(List<LabTask> tasks)
    {
        Console.WriteLine(
            $"{"Название",-38} {"Предмет",-24} {"Баллы",7} {"Сложн.",7} {"Дней",7} {"Приоритет",12}"
        );

        Console.WriteLine(new string('-', 102));

        foreach (var task in tasks)
        {
            Console.WriteLine(
                $"{Truncate(task.Title, 37),-38} " +
                $"{Truncate(task.Subject, 23),-24} " +
                $"{task.MaxPoints,7:F0} " +
                $"{task.Difficulty,7} " +
                $"{task.DaysLeft,7:F1} " +
                $"{task.PriorityScore,12:F2}"
            );
        }

        Console.WriteLine(new string('-', 102));
    }

    private static string Truncate(string value, int maxLength)
    {
        return value.Length <= maxLength
            ? value
            : value[..(maxLength - 3)] + "...";
    }

    private static void SaveReport(List<LabTask> tasks)
    {
        var csvPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "report.csv"
        );

        var csv = new StringBuilder();

        csv.AppendLine(
            "Title,Subject,MaxPoints,Difficulty,DaysLeft,PriorityScore"
        );

        foreach (var task in tasks)
        {
            csv.AppendLine(
                $"{EscapeCsv(task.Title)}," +
                $"{EscapeCsv(task.Subject)}," +
                $"{task.MaxPoints.ToString(CultureInfo.InvariantCulture)}," +
                $"{task.Difficulty}," +
                $"{task.DaysLeft.ToString(CultureInfo.InvariantCulture)}," +
                $"{task.PriorityScore.ToString(CultureInfo.InvariantCulture)}"
            );
        }

        File.WriteAllText(
            csvPath,
            csv.ToString(),
            new UTF8Encoding(true)
        );
    }

    private static string EscapeCsv(string value)
    {
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
