using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Booking
{
    struct ChechAndBook
    {
        public bool CheckIn; //false -- бронь, true -- заселён
        public uint CheckInTime { get; set; }   //дата заселения
        public uint CheckOutTime { get; set; }   //дата выселения

        public ChechAndBook(bool _CheckIn, uint _CheckInTime, uint _CheckOutTime)
        {
            CheckIn = _CheckIn;
            CheckInTime = _CheckInTime;
            CheckOutTime = _CheckOutTime;
        }
    }
    class LuxuryRoom : Room
    {
        public LuxuryRoom() : base()
        {
            Cost = 120;  //назначение стоимости номера
            NumberPersons = 6;  //назначение количества человек в номере
        }
    }

    class HalfLuxuryRoom : Room
    {
        public HalfLuxuryRoom() : base()
        {
            Cost = 100;  //назначение стоимости номера
            NumberPersons = 5;  //назначение количества человек в номере
        }
    }

    class SingleRoom : Room
    {
        public SingleRoom() : base()
        {
            Cost = 40;  //назначение стоимости номера
            NumberPersons = 1;  //назначение количества человек в номере
        }
    }

    class DoubleRoom : Room
    {
        public DoubleRoom() : base()
        {
            Cost = 60;  //назначение стоимости номера
            NumberPersons = 2;  //назначение количества человек в номере
        }
    }
}
