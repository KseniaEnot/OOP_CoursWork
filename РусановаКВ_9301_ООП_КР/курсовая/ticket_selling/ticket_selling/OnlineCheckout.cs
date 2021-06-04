using System;
using System.Collections.Generic;

class OnlineCheckout : Checkout
{
    public readonly string Link;  //ссылка на сайт онлайн кассы
    public OnlineCheckout(string[] _playsName, uint _rowSize, uint _placeSize, float[] _cost, string _Link) : base(_playsName, _rowSize, _placeSize, _cost)
    {
        Link = _Link;
    }
    public void BookTicket(string NamePlayToBuy, uint RowToBuy, uint PlaceToBuy)  //бронирование билетов
    {
        fingPlayByName(NamePlayToBuy).GiveTicket(RowToBuy, PlaceToBuy, ticketType.booked);
    }
}
