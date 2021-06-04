using System;
using System.Collections.Generic;

class Plays
{
    public uint rowSize;  //количество рядов
    public uint placeSize;  //количество мест
    public readonly float cost;  //стоимость билета
    public readonly List<Tickets> TicketsList;  //список билетов
    public string PlayName;  //название пьессы
    public DateTime startTime;  //время начала пьессы

    public Plays(string _PlayName, uint _rowSize, uint _placeSize, float _cost, DateTime _startTime)
    {
        rowSize = _rowSize;
        placeSize = _placeSize;
        TicketsList = new List<Tickets>();
        cost = _cost;
        for (uint i = 0; i < rowSize; i++)  //заплднение списка билетов
        {
            for (uint j = 0; j < placeSize; j++)
            {
                TicketsList.Add(new Tickets(i + 1, j + 1));
            }
        }
        PlayName = _PlayName;
        startTime = _startTime;
    }
    public void GiveTicket(uint RowToBuy, uint PlaceToBuy, ticketType whatDo)  //выдача билета
    {
        for (int i = 0; i < TicketsList.Count; i++)
        {
            if ((TicketsList[i].row == RowToBuy) && (TicketsList[i].place == PlaceToBuy) && (TicketsList[i].type == ticketType.free))
            {
                Tickets changeTic = TicketsList[i];
                changeTic.type = whatDo;
                TicketsList[i] = changeTic;
                return;
            }
        }
        throw new ArgumentException("Ticket not found");  //не удалось найти билет
    }
}
