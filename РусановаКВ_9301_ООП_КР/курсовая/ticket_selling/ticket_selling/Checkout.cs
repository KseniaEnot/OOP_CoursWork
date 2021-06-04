using System;
using System.Collections.Generic;

abstract class Checkout : IBank
{
    private List<Plays> playsList;  //списко пьесс

    public Checkout(string[] _playsName, uint _rowSize, uint _placeSize, float[] _cost)
    {
        playsList = new List<Plays>();
        for (int i = 0; i < _playsName.Length; i++)
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            playsList.Add(new Plays(_playsName[i], _rowSize, _placeSize, _cost[i], start.AddDays(gen.Next(range))));
        }
    }
    public void ShowPlays()  //метод покапзываеющий пьессы
    {
        for (int i = 0; i < playsList.Count; i++)
        {
            Console.WriteLine($"{playsList[i].PlayName}");
        }
    }
    public void BuyTicket(string NamePlayToBuy, uint RowToBuy, uint PlaceToBuy)  //покупка билетов
    {
        Plays Buy = fingPlayByName(NamePlayToBuy);
        Buy.GiveTicket(RowToBuy, PlaceToBuy, ticketType.paid);
        IBank.AddToBank(Buy.cost);  //добавляем стоимость билета в банк
    }
    private protected Plays fingPlayByName(string NamePlay)  //поиск пьессы по имени
    {
        for (int i = 0; i < playsList.Count; i++)  
        {
            if (playsList[i].PlayName == NamePlay)
            {
                return playsList[i];
            }
        }
        throw new ArgumentException("Play not found");
    }
    public void ShowFreePlace(string NamePlay)  //метод покапзываеющий свободные места
    {
        Plays Showing = fingPlayByName(NamePlay);
        Console.WriteLine($"Стоимость битлета = {Showing.cost}");
        uint row = 0;
        for (int i = 0; i < Showing.TicketsList.Count; i++)
        {
            if (row != Showing.TicketsList[i].row)
            {
                Console.WriteLine();
                Console.Write($"{Showing.TicketsList[i].row}: ");
                row = Showing.TicketsList[i].row;
            }
            if (Showing.TicketsList[i].type == ticketType.free)
            {
                Console.Write($"{Showing.TicketsList[i].place} ");
            }
        }
    }
}
