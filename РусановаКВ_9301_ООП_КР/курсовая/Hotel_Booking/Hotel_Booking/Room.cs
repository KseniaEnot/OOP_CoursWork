using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_Booking
{
    abstract class Room
    {
        public List<ChechAndBook> CheckList;
        public uint Cost { get; protected set; }   // стоимость
        public uint NumberPersons { get; protected set; }  // количество человек в номере
        

        public Room()
        {
            CheckList = new List<ChechAndBook>();
            Cost = 0;
            NumberPersons = 0;
        }

    }
}