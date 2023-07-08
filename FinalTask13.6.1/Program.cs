//Наша задача — сравнить производительность вставки в List<T> и LinkedList<T>.Для этого используйте уже знакомый вам StopWatch.
//На примере этого текста, выясните, какие будут различия между этими коллекциями.

using System.Diagnostics;

class Program
{
    public static void Main()
    {
        Console.Write("Введите путь к файлу: ");
        string filePath = string.Empty;
        filePath = Console.ReadLine();

        if (File.Exists(filePath))
        {
            //объявляем разделители
            char[] delimiters = new char[] { ' ', '\r', '\n' };

            //запускаем таймер для List
            var stopWatchList = Stopwatch.StartNew();
            ListInsert(delimiters, filePath);
            Console.WriteLine($"Время обработки с List (мс) = {stopWatchList.Elapsed.TotalMilliseconds}");

            //запускаем таймер для LinkedList
            var stopWatchLinkedList = Stopwatch.StartNew();
            LinkedListInsert(delimiters, filePath);
            Console.WriteLine($"Время обработки с LinkedList (мс) = {stopWatchLinkedList.Elapsed.TotalMilliseconds}");            
        }
        else Console.WriteLine("Файл не найден...");

        Console.ReadKey();
    }

    public static List<string> ListInsert(char[] delimiters, string filePath)
    {
        var words = new List<string>();

        using (StreamReader sr = File.OpenText(filePath))
        {
            string stroke = string.Empty;
            while ((stroke = sr.ReadLine()) != null) // Пока не кончатся строки - считываем из файла по одной и выводим в консоль
            {
                // Разбиваем считываемую строку на слова на основе разделителей
                var strokeWords = stroke.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // Вставляем все слова в список
                foreach (var strokeWord in strokeWords)
                {
                    // проверка необязательна - можно было words.Insert(words.Count(), strokeWord),
                    // но для равнозначного теста с LinkedList делаем:
                    if (words.Count() != 0) words.Insert(1, strokeWord); // вставляем после первого
                    else words.Insert(0, strokeWord);
                }
            }
        }

        return words;
    }

    public static LinkedList<string> LinkedListInsert(char[] delimiters, string filePath)
    {
        var words = new LinkedList<string>();

        using (StreamReader sr = File.OpenText(filePath))
        {
            string stroke = string.Empty;
            while ((stroke = sr.ReadLine()) != null) // Пока не кончатся строки - считываем из файла по одной и выводим в консоль
            {
                // Разбиваем считываемую строку на слова на основе разделителей
                var strokeWords = stroke.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // Вставляем все слова в список
                foreach (var strokeWord in strokeWords)
                {
                    if (words.Count() != 0) words.AddAfter(words.First, strokeWord); // вставляем после первого
                    else words.AddFirst(strokeWord);
                }
            }
        }

        return words;
    }
}