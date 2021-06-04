using System;

enum ticketType  //типы мест
{
    free,  //свободное место
    booked,  //забронированное место
    paid  //оплаченное место
}
struct Tickets
{

    public ticketType type;  //тип билета
    public readonly uint row;  //ряд
    public readonly uint place;  //месито
    public Tickets(uint _row, uint _place)
    {
        this.place = _place;
        this.row = _row;
        this.type = ticketType.free;
    }
}
