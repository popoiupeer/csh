using System;
using System.Collections.Generic;

class PredictionGenerator
{
    private string[] beginnings = new string[]
    {
            "Сегодня", "Завтра", "Через неделю", "В ближайшую полнолуние",
            "Когда взойдет красная луна", "Ровно в полночь", "На следующем перекрестке",
            "Когда ты меньше всего ожидаешь", "В день твоего рождения", "Когда встретишь черного кота",
            "Под знаком Водолея", "Когда ветер сменит направление", "В час великого решения",
            "Когда загадаешь желание", "В сумерках"
    };

    private string[] actions = new string[]
    {
            "ты найдешь", "ты потеряешь", "тебя ждет", "ты откроешь",
            "ты поймешь", "ты встретишь", "ты избежишь", "ты притянешь",
            "ты разгадаешь", "ты изменишь", "ты обретешь", "ты отпустишь",
            "ты преодолеешь", "ты создашь", "ты почувствуешь", "ты услышишь",
            "ты увидишь", "ты прикоснешься к", "ты раскроешь", "ты станешь частью"
    };

    private string[] endings = new string[]
    {
            "счастье", "тайну", "приключение", "неожиданный поворот",
            "древний артефакт", "забытое знание", "вторую половинку",
            "скрытую силу", "долгожданную победу", "мистический знак",
            "испытание", "дар судьбы", "ключ от всех дверей", "проклятие",
            "благословение луны", "следы прошлой жизни", "зеркальное отражение",
            "голос из ниоткуда", "путеводную звезду", "тень неизвестного",
            "магический кристалл", "книгу судеб", "загадочное послание",
            "судьбоносную встречу", "дверь в другой мир"
    };

    private Random random;

    public PredictionGenerator()
    {
        random = new Random();
    }

    public string GeneratePrediction()
    {
        int startIndex = random.Next(beginnings.Length);
        int actionIndex = random.Next(actions.Length);
        int endIndex = random.Next(endings.Length);

        return $"{beginnings[startIndex]}, {actions[actionIndex]} {endings[endIndex]}.";
    }
}
class Program
{
    static void Main(string[] args)
    {
        PredictionGenerator generator = new PredictionGenerator();
        string choice;
        do
        {
            Console.WriteLine("Хотите получить магическое предсказание? (да/нет)");
            choice = Console.ReadLine().ToLower();

            if (choice == "да")
            {
                string prediction = generator.GeneratePrediction();
                Console.WriteLine(prediction);
            }
            else if (choice != "нет")
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите 'да' или 'нет'.");
            }

        } while (choice != "нет");

        Console.WriteLine("До свидания!");
    }
}
