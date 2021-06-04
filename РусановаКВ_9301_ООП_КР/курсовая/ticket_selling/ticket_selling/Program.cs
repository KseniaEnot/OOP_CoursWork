using System;

class Program
{
    static void Main(string[] args)
    {
        string[] NamePlay = { "Лебединое озеро", "Утиная охота", "Три сестры", "Хулиган", "Мастер и Маргарита" };  //массив названий пьесс
        float[] CostPlay = { 3000, 1200, 5000, 3500, 5000 };  //массив стоимостей билетов
        OnlineCheckout Yandex = new OnlineCheckout(NamePlay, 20, 30, CostPlay, "https://afisha.yandex.ru/");  //создание олайн кассы
        PhysicalCheckout Mariinsky = new PhysicalCheckout(NamePlay, 20, 30, CostPlay);  //создание физической кассы

        Yandex.ShowPlays(); //выводим названия пьесс
        Console.Write("Введите название пьессы: ");
        string ReadingLine = Console.ReadLine();
        Yandex.ShowFreePlace(ReadingLine); //показываем свободные места
        Console.Write("Введите ряд: ");
        uint row = Convert.ToUInt32(Console.ReadLine());
        Console.Write("Введите место: ");
        uint place = Convert.ToUInt32(Console.ReadLine());
        Yandex.BookTicket(ReadingLine, row, place);  //бронируем билеты
        Console.WriteLine("Бронирование прошло успешно.");
        Yandex.ShowFreePlace(ReadingLine);  //показываем изменения
        Console.ReadKey();

    }
}
